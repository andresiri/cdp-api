﻿using System.Collections.Generic;
using FluentValidation.Results;

namespace domain.Entities
{
    public class Pelada : EntityModel
    {
        public string Name { get; set; }
        public int? ArenaDefaultId { get; set; }
        public int CreatedByUserId { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual List<PeladaUser> PeladaUsers { get; set; }
        public virtual List<PeladaEvent> PeladaEvents { get; set; }
        public virtual Arena ArenaDefault { get; set; }
    }
}