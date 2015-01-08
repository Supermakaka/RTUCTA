using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Infrastructure;

using BCrypt.Net;

namespace BusinessLogic.Services
{
    public class CompanyService : BaseService<Company>, ICompanyService
    {
        public CompanyService(IDataContext dataContext) 
            : base(dataContext)
        {
        }

        public IEnumerable<Company> GetAllCompany()
        {
            return dataContext.Companies.OfType<Company>();
        }
        
    }

    public interface ICompanyService : IService<Company>
    {
        IEnumerable<Company> GetAllCompany();
    }
}
