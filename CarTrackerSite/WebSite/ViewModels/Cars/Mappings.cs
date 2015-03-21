using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Cars
{
    using ViewModels.Mappings;
    using WebSite.Core;

    public class Mappings : IViewModelMapping
    {   
        public void Create(IConfiguration configuration)
        {
            #region From Domain Model to View Model

            configuration.CreateMap<Car, CarViewModel>()
                .IgnoreAllNonExisting();

            configuration.CreateMap<Car, CarsListViewModel>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.FirstName + s.User.LastName))
                .ForMember(d => d.UserEmail, o => o.MapFrom(s => s.User.Email))
                .IgnoreAllNonExisting();

            configuration.CreateMap<Car, CarViewModel>()
                .IgnoreAllNonExisting();

            #endregion

            #region From ViewModel To Domain Model

            configuration.CreateMap<CarViewModel, Car>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => HttpContextStorage.CurrentUser.Id))
                .IgnoreAllNonExisting();

            #endregion
        }
    }
}