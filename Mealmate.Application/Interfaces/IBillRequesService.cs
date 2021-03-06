﻿using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IBillRequestService
    {
        Task<IEnumerable<BillRequestModel>> Get();
        Task<IEnumerable<BillRequestModel>> Get(int branchId, int billRequestStateId);

        Task<BillRequestModel> GetById(int id);
        Task<BillRequestModel> Create(BillRequestCreateModel model);
        Task Update(int id, BillRequestUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<BillRequestModel>> Search(int branchId, PageSearchArgs args);
    }
}
