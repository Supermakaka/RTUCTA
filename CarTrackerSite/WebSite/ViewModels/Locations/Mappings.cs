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
        public void Create(IConfiguration configuration) {

            configuration.CreateMap<Location, LocationViewModel>();

        }
    }
}