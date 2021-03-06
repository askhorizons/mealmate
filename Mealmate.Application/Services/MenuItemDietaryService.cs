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
    public class MenuItemDietaryService : IMenuItemDietaryService
    {
        private readonly IMenuItemDietaryRepository _menuItemDietaryRepository;
        private readonly IAppLogger<MenuItemDietaryService> _logger;
        private readonly IMapper _mapper;

        public MenuItemDietaryService(
            IMenuItemDietaryRepository menuItemDietaryRepository,
            IAppLogger<MenuItemDietaryService> logger,
            IMapper mapper)
        {
            _menuItemDietaryRepository = menuItemDietaryRepository ?? throw new ArgumentNullException(nameof(menuItemDietaryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<MenuItemDietaryModel> Create(MenuItemDietaryCreateModel model)
        {
            var newmenuItem = new MenuItemDietary
            {
                Created = DateTime.Now,
                DietaryId = model.DietaryId,
                IsActive = model.IsActive,
                MenuItemId = model.MenuItemId
            };
            newmenuItem = await _menuItemDietaryRepository.SaveAsync(newmenuItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newmenuItemmodel = _mapper.Map<MenuItemDietaryModel>(newmenuItem);
            return newmenuItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingMenuItem = await _menuItemDietaryRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            await _menuItemDietaryRepository.DeleteAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<MenuItemDietaryModel>> Get(int menuItemId)
        {
            var result = await _menuItemDietaryRepository.GetAsync(x => x.MenuItemId == menuItemId);
            return _mapper.Map<IEnumerable<MenuItemDietaryModel>>(result);
        }

        public async Task<MenuItemDietaryModel> GetById(int id)
        {
            return _mapper.Map<MenuItemDietaryModel>(await _menuItemDietaryRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, MenuItemDietaryUpdateModel model)
        {
            var existingMenuItem = await _menuItemDietaryRepository.GetByIdAsync(id);
            if (existingMenuItem == null)
            {
                throw new ApplicationException("MenuItem with this id is not exists");
            }

            existingMenuItem.IsActive = model.IsActive;

            await _menuItemDietaryRepository.SaveAsync(existingMenuItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<MenuItemDietaryModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _menuItemDietaryRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuItemDietaryModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuItemDietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<MenuItemDietaryModel>> Search(int branchId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _menuItemDietaryRepository.SearchAsync(branchId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<MenuItemDietaryModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<MenuItemDietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }
    }
}
