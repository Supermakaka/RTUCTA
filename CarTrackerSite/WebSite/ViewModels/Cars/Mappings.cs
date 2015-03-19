using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Cars
{
    using ViewModels.Mappings;

    public class Mappings : IViewModelMapping
    {   
        public void Create(IConfiguration configuration)
        {
            #region From Domain Model to View Model

            configuration.CreateMap<Car, CarViewModel>();

            configuration.CreateMap<Car, CarsListViewModel>()
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.FirstName + s.User.LastName))
                .ForMember(d => d.UserEmail, o => o.MapFrom(s => s.User.Email));
            #endregion
        }
    }
}