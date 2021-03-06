using System;
using System.Collections.Generic;
using System.Net.Cache;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class Branch : Entity
    {
        //public int BranchId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public DateTimeOffset Created { get; set; }
        public TimeSpan ServiceTimeFrom { get; set; }
        public TimeSpan ServiceTimeTo { get; set; }
        public bool IsActive { get; set; }

        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<OptionItem> OptionItems { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }

        public Branch()
        {
            Locations = new HashSet<Location>();
            Menus = new HashSet<Menu>();
            OptionItems = new HashSet<OptionItem>();
            UserBranches = new HashSet<UserBranch>();
        }
    }
}