﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class UserRestaurantService : IUserRestaurantService
    {
        private readonly IUserRestaurantRepository _UserRestaurantRepository;
        private readonly IAppLogger<UserRestaurantService> _logger;
        private readonly IMapper _mapper;

        public UserRestaurantService(
            IUserRestaurantRepository UserRestaurantRepository,
            IAppLogger<UserRestaurantService> logger,
            IMapper mapper)
        {
            _UserRestaurantRepository = UserRestaurantRepository ?? throw new ArgumentNullException(nameof(UserRestaurantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<UserRestaurantModel> Create(UserRestaurantCreateModel model)
        {
            var newUser = new UserRestaurant
            {
                OwnerId = model.OwnerId,
                RestaurantId = model.RestaurantId,
                Created = DateTime.Now,
                IsActive = model.IsActive
            };

            newUser = await _UserRestaurantRepository.SaveAsync(newUser);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newUsermodel = _mapper.Map<UserRestaurantModel>(newUser);
            return newUsermodel;
        }

        public async Task Delete(int id)
        {
            var existingUser = await _UserRestaurantRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            await _UserRestaurantRepository.DeleteAsync(existingUser);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<UserRestaurantModel>> Get(int ownerId)
        {
            var result = await _UserRestaurantRepository.GetAsync(x => x.OwnerId == ownerId);
            return _mapper.Map<IEnumerable<UserRestaurantModel>>(result);
        }

        public async Task<UserRestaurantModel> GetById(int id)
        {
            return _mapper.Map<UserRestaurantModel>(await _UserRestaurantRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, UserRestaurantUpdateModel model)
        {
            var existingUser = await _UserRestaurantRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException($"Resource with this id {id} does not exists");
            }

            existingUser = _mapper.Map<UserRestaurant>(model);

            await _UserRestaurantRepository.SaveAsync(existingUser);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<UserRestaurantModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _UserRestaurantRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestaurantModels = _mapper.Map<List<UserRestaurantModel>>(TablePagedList.Items);

            var RestaurantModelPagedList = new PagedList<UserRestaurantModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                RestaurantModels);

            return RestaurantModelPagedList;
        }

        public async Task<IPagedList<UserRestaurantModel>> Search(int userId, PageSearchArgs args)
        {
            var TablePagedList = await _UserRestaurantRepository.SearchAsync(userId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestaurantModels = _mapper.Map<List<UserRestaurantModel>>(TablePagedList.Items);

            var RestaurantModelPagedList = new PagedList<UserRestaurantModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                RestaurantModels);

            return RestaurantModelPagedList;
        }

    }
}