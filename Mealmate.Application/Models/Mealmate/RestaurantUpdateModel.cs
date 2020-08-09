﻿using Mealmate.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class RestaurantUpdateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public byte[] Photo { get; set; }
    }
}
