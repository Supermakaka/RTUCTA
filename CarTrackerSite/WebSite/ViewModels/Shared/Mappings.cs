using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Shared
{
    using ViewModels.Shared;
    using ViewModels.Mappings;

    public class Mappings : IViewModelMapping
    {
        public void Create(IConfiguration configuration)
        {

            #region From Domain Modal to View Modal

            configuration.CreateMap<Company, CompanyListViewModel>();
            configuration.CreateMap<Company, CreateEditCompanyViewModel>();

            configuration.CreateMap<Car, DeleteApproveViewModel>()
                .ForMember(d => d.ID, o => o.MapFrom(c => c.Id))
                .ForMember(d => d.Name, o => o.MapFrom(c => c.CarNumber));
            #endregion          
        }
    }
}