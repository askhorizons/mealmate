﻿using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class AllergenUpdateModel
    {
        [Required]
        public string Name { get; set; }

        public byte[] Photo { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}