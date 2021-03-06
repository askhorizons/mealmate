﻿using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IRestroomRequestService
    {
        Task<IEnumerable<RestroomRequestModel>> Get();
        Task<IEnumerable<RestroomRequestModel>> Get(int branchId, int restroomRequestStateId);

        Task<RestroomRequestModel> GetById(int id);
        Task<RestroomRequestModel> Create(RestroomRequestCreateModel model);
        Task Update(int id, RestroomRequestUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<RestroomRequestModel>> Search(int branchId, PageSearchArgs args);
    }
}
