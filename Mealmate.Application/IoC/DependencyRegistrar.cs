﻿using Mealmate.Application.Interfaces;
using Mealmate.Application.Services;
using Mealmate.Infrastructure.IoC;
using Mealmate.Infrastructure.Misc;
using Autofac;
using AutoMapper;
using System.Collections.Generic;
using Mealmate.Application.Mapper;

namespace Mealmate.Application.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(context => new MapperConfiguration(configuration =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    configuration.AddProfile(profile);
                }
            }))
              .AsSelf()
              .InstancePerRequest();

            builder.Register(context => context.Resolve<MapperConfiguration>()
                .CreateMapper(context.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            // services

            builder.RegisterType<UserAllergenService>().As<IUserAllergenService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDietaryService>().As<IUserDietaryService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRestaurantService>().As<IUserRestaurantService>().InstancePerLifetimeScope();
            builder.RegisterType<UserBranchService>().As<IUserBranchService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.RegisterType<RestaurantService>().As<IRestaurantService>().InstancePerLifetimeScope();
            builder.RegisterType<BranchService>().As<IBranchService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();

            builder.RegisterType<MenuItemOptionService>().As<IMenuItemOptionService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemService>().As<IMenuItemService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemAllergenService>().As<IMenuItemAllergenService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemDietaryService>().As<IMenuItemDietaryService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemOptionService>().As<IMenuItemOptionService>().InstancePerLifetimeScope();

            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<OptionItemService>().As<IOptionItemService>().InstancePerLifetimeScope();
            builder.RegisterType<QRCodeService>().As<IQRCodeService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();
            builder.RegisterType<TableService>().As<ITableService>().InstancePerLifetimeScope();


            builder.RegisterType<AllergenService>().As<IAllergenService>().InstancePerLifetimeScope();
            builder.RegisterType<DietaryService>().As<IDietaryService>().InstancePerLifetimeScope();
            builder.RegisterType<CuisineTypeService>().As<ICuisineTypeService>().InstancePerLifetimeScope();

            builder.RegisterType<OptionItemService>().As<IOptionItemService>().InstancePerLifetimeScope();
            builder.RegisterType<OptionItemAllergenService>().As<IOptionItemAllergenService>().InstancePerLifetimeScope();
            builder.RegisterType<OptionItemDietaryService>().As<IOptionItemDietaryService>().InstancePerLifetimeScope();

            builder.RegisterType<OrderStateService>().As<IOrderStateService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderItemDetailService>().As<IOrderItemDetailService>().InstancePerLifetimeScope();

            builder.RegisterType<RestroomRequestService>().As<IRestroomRequestService>().InstancePerLifetimeScope();
            builder.RegisterType<ContactRequestService>().As<IContactRequestService>().InstancePerLifetimeScope();
            builder.RegisterType<BillRequestService>().As<IBillRequestService>().InstancePerLifetimeScope();

            builder.RegisterType<RestroomRequestStateService>().As<IRestroomRequestStateService>().InstancePerLifetimeScope();
            builder.RegisterType<ContactRequestStateService>().As<IContactRequestStateService>().InstancePerLifetimeScope();
            builder.RegisterType<BillRequestStateService>().As<IBillRequestStateService>().InstancePerLifetimeScope();

            builder.RegisterType<MealMateMapper>().As<Profile>();

        }

        public int Order => 2;
    }
}
