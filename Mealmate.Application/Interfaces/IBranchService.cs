﻿using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchModel>> Get(int restaurantId);
        Task<BranchModel> GetById(int id);
        Task<BranchModel> Create(BranchCreateModel model);
        Task Update(BranchUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<BranchModel>> Search(PageSearchArgs args);
        Task<IPagedList<BranchModel>> Search(int restaurantId, int isActive, PageSearchArgs args);
    }
}
