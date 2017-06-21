﻿using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace domain.Entities
{
    public class PeladaEvent : EntityModel
    {
        public int PeladaId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        public virtual Pelada Pelada { get; set; }
        public List<PeladaEventUser> PeladaEventUsers { get; set; }
    }
}
