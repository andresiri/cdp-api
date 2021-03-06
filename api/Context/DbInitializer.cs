﻿using System.Collections.Generic;
using api.Context.Transaction;
using domain.Entities;
using domain.Enum;
using domain.Lib;
using Microsoft.EntityFrameworkCore;

namespace api.Context
{
    public class DbInitializer
    {        
        public static void Initialize(CdpContext context)
        {            
            var resetData = true;

            context.Database.EnsureCreated();

            if (resetData)
            {
                context.Database.ExecuteSqlCommand("DELETE FROM peladaEvent");
                context.Database.ExecuteSqlCommand("ALTER TABLE peladaEvent AUTO_INCREMENT = 1");

                context.Database.ExecuteSqlCommand("DELETE FROM peladaUser");
                context.Database.ExecuteSqlCommand("ALTER TABLE peladaUser AUTO_INCREMENT = 1");
                
                context.Database.ExecuteSqlCommand("DELETE FROM peladaTeam");
                context.Database.ExecuteSqlCommand("ALTER TABLE peladaTeam AUTO_INCREMENT = 1");

                context.Database.ExecuteSqlCommand("DELETE FROM pelada");
                context.Database.ExecuteSqlCommand("ALTER TABLE pelada AUTO_INCREMENT = 1");

                context.Database.ExecuteSqlCommand("DELETE FROM arena");
                context.Database.ExecuteSqlCommand("ALTER TABLE arena AUTO_INCREMENT = 1");

                context.Database.ExecuteSqlCommand("DELETE FROM user");
                context.Database.ExecuteSqlCommand("ALTER TABLE user AUTO_INCREMENT = 1");

                context.SaveChanges();
            }

            var unitOfWork = new UnitOfWork(context);

            var users = InsertUsers(unitOfWork);
            var peladas = InsertPeladas(unitOfWork, users);

            InsertPeladaTeams(unitOfWork, peladas);
            InsertPeladaUsers(unitOfWork, users, peladas);
            InsertArenas(unitOfWork);          

            unitOfWork.Save();           
        }

        public static List<User> InsertUsers(UnitOfWork unitOfWork)
        {
            var users = new User[]
            {
                new User
                {
                    Email = "andremirannda@gmail.com",
                    FirstName = "Andre",
                    LastName = "Miranda",
                    Number = 8,
                    Password = Password.Hash("andre", "andremirannda@gmail.com"),
                    Position = PositionEnum.MeioCampo
                },
                new User
                {
                    Email = "heliofeliciano@gmail.com",
                    FirstName = "Helio",
                    LastName = "Feliciano",
                    Number = 7,
                    Password = Password.Hash("helio", "heliofeliciano@gmail.com"),
                    Position = PositionEnum.Atacante
                },
                new User
                {
                    Email = "lessa@gmail.com",
                    FirstName = "Mauricio",
                    LastName = "Lessa",
                    Number = 5,
                    Password = Password.Hash("lessa", "lessa@gmail.com"),
                    Position = PositionEnum.MeioCampo
                },
                new User
                {
                    Email = "duble@gmail.com",
                    FirstName = "Filipe",
                    LastName = "Egito",
                    Nickname = "Dublê",
                    Number = 1,
                    Password = Password.Hash("duble", "duble@gmail.com"),
                    Position = PositionEnum.Goleiro
                }
            };

            var newUsers = new List<User>();

            foreach (var user in users)
            {
                newUsers.Add(unitOfWork.UserRepository.Create(user));
            }

            return newUsers;
        }

        public static void InsertArenas(UnitOfWork unitOfWork) 
        {                        
            var arenas = new Arena[]
            {
                new Arena {Name = "Arena Capim Macio", Description = "Rua Arena Capim Macio"},
                new Arena {Name = "Arena Nova Descoberta", Description = "Rua Arena Nova Descoberta"}
            };

            foreach(var arena in arenas) 
            {
                unitOfWork.ArenaRepository.Create(arena);
            }

            var xico = unitOfWork.ArenaRepository.GetAll();
        }

        public static List<Pelada> InsertPeladas(UnitOfWork unitOfWork, List<User> users)
        {            
            var peladas = new Pelada[]
            {
                new Pelada {Name = "Pelada grotesca", Day = WeekdayEnum.TUESDAY, CreatedByUserId = users[0].Id},
                new Pelada {Name = "Pelada da Massa", Day = WeekdayEnum.MONDAY, CreatedByUserId = users[0].Id},
                new Pelada {Name = "Pelada do floresta encantada", Day = WeekdayEnum.FRIDAY, CreatedByUserId = users[1].Id}
            };

            var newPeladas = new List<Pelada>();

            foreach (var pelada in peladas)
            {
                newPeladas.Add(unitOfWork.PeladaRepository.Create(pelada));
            }

            return newPeladas;
        }

        public static void InsertPeladaUsers(UnitOfWork unitOfWork, List<User> users, List<Pelada> peladas)
        {            
            var peladaUsers = new PeladaUser[]
            {
                new PeladaUser {UserId = users[0].Id, PeladaId = peladas[0].Id, IsMonthly = true, IsAdministrator = true},
                new PeladaUser {UserId = users[1].Id, PeladaId = peladas[0].Id, IsMonthly = true, IsAdministrator = true},
                new PeladaUser {UserId = users[2].Id, PeladaId = peladas[0].Id},
                new PeladaUser {UserId = users[3].Id, PeladaId = peladas[0].Id},

                new PeladaUser {UserId = users[0].Id, PeladaId = peladas[1].Id},
                new PeladaUser {UserId = users[1].Id, PeladaId = peladas[1].Id}
            };

            foreach (var peladaUser in peladaUsers)
            {
                unitOfWork.PeladaUserRepository.Create(peladaUser);
            }
        }

        public static void InsertPeladaTeams(UnitOfWork unitOfWork, List<Pelada> peladas)
        {
            var peladaTeams = new PeladaTeam[]
            {
                new PeladaTeam() {PeladaId = peladas[0].Id, Name = "Time 1"},
                new PeladaTeam() {PeladaId = peladas[0].Id, Name = "Time 2"},
                new PeladaTeam() {PeladaId = peladas[0].Id, Name = "Time 3"}
            };
            
            foreach (var peladaTeam in peladaTeams)
            {
                unitOfWork.PeladaTeamRepository.Create(peladaTeam);
            }
        }
    }
}