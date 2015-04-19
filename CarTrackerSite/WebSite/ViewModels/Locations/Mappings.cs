using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Locations
{
    using ViewModels.Mappings;

    public class Mappings : IViewModelMapping
    {


        public void Create(IConfiguration configuration)
        {
            #region ViewModel to DomainModel
            configuration.CreateMap<LocationViewModel, Location>()
                .ForMember(d => d.Throtle, o => o.ResolveUsing<ThrotleResolver>().FromMember(s => s.Throtle))
                .ForMember(d => d.Speed, o => o.ResolveUsing<SpeedResolver>().FromMember(s => s.Speed))
                .ForMember(d => d.FuelTank, o => o.ResolveUsing<FuelTankResolver>().FromMember(s => s.FuelTank))
                .IgnoreAllNonExisting();
        
        
            #endregion

            #region DomainModel to ViewModel

            configuration.CreateMap<Location, LocationViewModel>()
                .IgnoreAllNonExisting();

            #endregion
        }

        

 
    }
}