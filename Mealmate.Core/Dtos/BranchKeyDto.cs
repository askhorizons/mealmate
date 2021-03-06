﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Dtos
{
    public class BranchKeyDto
    {
        public string Branch { get; set; }
        public int BranchId { get; set; }
        public string Restaurant { get; set; }
        public byte[] Photo { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
