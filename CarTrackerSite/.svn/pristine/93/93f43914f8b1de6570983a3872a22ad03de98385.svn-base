using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ninject;
using Ninject.Modules;
using AutoMapper;

namespace WebSite.Core.NinjectModules
{
    public class AutoMapperModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().ToMethod(context => Mapper.Configuration);
            Bind<IMappingEngine>().ToMethod(context => Mapper.Engine);

            SetupMappings(Kernel.Get<IConfiguration>());

            Mapper.AssertConfigurationIsValid();    
        }

        private void SetupMappings(IConfiguration configuration)
        {
            var mappings = typeof(ViewModels.Mappings.IViewModelMapping).Assembly.GetExportedTypes()
            .Where(x => !x.IsAbstract && typeof(ViewModels.Mappings.IViewModelMapping).IsAssignableFrom(x))
            .Select(Activator.CreateInstance)
            .Cast<ViewModels.Mappings.IViewModelMapping>();

            foreach (ViewModels.Mappings.IViewModelMapping mapping in mappings)
                mapping.Create(configuration);
        }
    }
}