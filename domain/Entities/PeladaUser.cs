﻿using System;
using System.Collections.Generic;
using FluentValidation.Results;
using domain.Entities.Validations;

namespace domain.Entities
{
    public class PeladaUser : EntityModel
    {
        public int PeladaId { get; set; }
        public virtual Pelada Pelada { get; set; }
        public bool IsMonthly { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
