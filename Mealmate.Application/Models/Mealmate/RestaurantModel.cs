﻿using Mealmate.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class RestaurantModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsActive { get; set; }
        public bool IsOwner { get; set; }
    }
}
