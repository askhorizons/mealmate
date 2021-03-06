﻿using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Mealmate.Infrastructure.Repository.Base;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Repository
{
    public class BillRequestRepository : Repository<BillRequest>, IBillRequestRepository
    {
        public BillRequestRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<BillRequest>> SearchAsync(int branchId, PageSearchArgs args)
        {
            var query = Table
                            .Include(u => u.Customer)
                            .Include(p => p.Table)
                            .ThenInclude(l => l.Location)
                            .ThenInclude(b => b.Branch)
                            .ThenInclude(r => r.Restaurant)
                            .Where(p => p.Table.Location.BranchId == branchId)
                            .OrderByDescending(p => p.RequestTime);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<BillRequest, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<BillRequest, object>>>(sortingOption, c => c.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<BillRequest, object>>>(sortingOption, c => c.BillRequestState.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<BillRequest, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, c => c.Id));
            }

            var filterList = new List<Tuple<FilteringOption, Expression<Func<BillRequest, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<BillRequest, bool>>>(filteringOption, c => c.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<BillRequest, bool>>>(filteringOption, c => c.BillRequestState.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var tempPagedList = new PagedList<BillRequest>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<BillRequest>>(tempPagedList);
        }
    }
}
