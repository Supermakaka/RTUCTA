using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AutoMapper;

using BusinessLogic.Models;

namespace WebSite.ViewModels.Admin
{
    using ViewModels.Shared;
    using ViewModels.Mappings;

    public class Mappings : IViewModelMapping
    {
        public void Create(IConfiguration configuration)
        {
            configuration.CreateMap<User, EditUserViewModel>()
                .ForMember(d => d.NewPassword, o => o.Ignore())
                .ForMember(d => d.Companies, o => o.Ignore())
                .ForMember(d => d.CompanyId, o => o.MapFrom(s => s.Company.Id));

            configuration.CreateMap<User, UserListViewModel>()
                .ForMember(d => d.CompanyName, o => o.MapFrom(s => s.Company.Name))
                .ForMember(d => d.CreateDate, o => o.ResolveUsing<DateToFormattedStringResolver>().FromMember(s => s.CreateDate)) 
            ;
        }
    }
}