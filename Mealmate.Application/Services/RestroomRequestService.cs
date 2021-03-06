﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mealmate.Application.Services
{
    public class RestroomRequestService : IRestroomRequestService
    {
        private readonly MealmateContext _context;
        private readonly IRestroomRequestRepository _restroomRequestRepository;
        private readonly IAppLogger<RestroomRequestService> _logger;
        private readonly IMapper _mapper;

        public RestroomRequestService(
            IRestroomRequestRepository restroomRequestRepository,
            IAppLogger<RestroomRequestService> logger,
            IMapper mapper,
            MealmateContext context)
        {
            _context = context;
            _restroomRequestRepository = restroomRequestRepository ?? throw new ArgumentNullException(nameof(restroomRequestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<RestroomRequestModel> Create(RestroomRequestCreateModel model)
        {

            // 1: new request creation
            var new_dietary = new RestroomRequest
            {
                CustomerId = model.CustomerId,
                TableId = model.TableId,
                RequestTime = DateTime.Now,
                RestroomRequestStateId = 1,
                IsDisabled = model.IsDisabled,
                Gender = model.Gender
            };

            new_dietary = await _restroomRequestRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<RestroomRequestModel>(new_dietary);
            return new_dietarymodel;
        }
        #endregion

        #region Read

        public async Task<IEnumerable<RestroomRequestModel>> Get(int branchId, int restroomRequestStateId)
        {
            var result = await _context.RestroomRequests
                            .Include(s => s.RestRoomRequestState)
                            .Include(u => u.Customer)
                            .Include(p => p.Table)
                            .ThenInclude(l => l.Location)
                            .ThenInclude(b => b.Branch)
                            .ThenInclude(r => r.Restaurant)
                            .ToListAsync();
            result = result
                        .Where(p => p.Table.Location.BranchId == branchId &&
                                    p.RestroomRequestStateId == restroomRequestStateId)
                        .OrderByDescending(p => p.RequestTime)
                        .ToList();

            return _mapper.Map<IEnumerable<RestroomRequestModel>>(result);
        }

        public async Task<IEnumerable<RestroomRequestModel>> Get()
        {
            var result = await _restroomRequestRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<RestroomRequestModel>>(result);
        }

        public async Task<RestroomRequestModel> GetById(int id)
        {
            RestroomRequestModel model = null;
            var result = await _restroomRequestRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<RestroomRequestModel>(result);
            return model;
        }
        public async Task<IPagedList<RestroomRequestModel>> Search(int restaurantId, PageSearchArgs args)
        {
            var TablePagedList = await _restroomRequestRepository.SearchAsync(restaurantId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestroomRequestModels = _mapper.Map<List<RestroomRequestModel>>(TablePagedList.Items);

            var RestroomRequestModelPagedList = new PagedList<RestroomRequestModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                RestroomRequestModels);

            return RestroomRequestModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, RestroomRequestUpdateModel model)
        {
            var existingTable = await _restroomRequestRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("RestroomRequest with this id is not exists");
            }

            existingTable.Remarks = model.Remarks;
            existingTable.ResponseTime = DateTime.Now;
            existingTable.RestroomRequestStateId = model.RestroomRequestStateId;

            await _restroomRequestRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingTable = await _restroomRequestRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("RestroomRequest with this id is not exists");
            }

            await _restroomRequestRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        #endregion
    }
}
