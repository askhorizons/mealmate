﻿using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuItemDetailUpdateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int MenuId { get; set; }
        [Required]
        public int CuisineTypeId { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public List<MenuItemDetailCreateAllergenModel> Allergens { get; set; }
        public List<MenuItemDetailCreateDietaryModel> Dietaries { get; set; }

        public MenuItemDetailUpdateModel()
        {
            Allergens = new List<MenuItemDetailCreateAllergenModel>();
            Dietaries = new List<MenuItemDetailCreateDietaryModel>();
        }
    }

    public class MenuItemDetailUpdateAllergenModel
    {
        public int MenuItemAllergenId { get; set; } = 0;

        [Required]
        public int AllergenId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class MenuItemDetailUpdateDietaryModel
    {
        public int MenuItemDietaryId { get; set; } = 0;

        [Required]
        public int DietaryId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}