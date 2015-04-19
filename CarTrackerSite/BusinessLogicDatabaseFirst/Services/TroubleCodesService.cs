using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TroubleCodesService : BaseService<TroubleCode>, ITroubleCodesService
    {
        public TroubleCodesService(DataContext dataContext)
            : base(dataContext)
        {

        }

        #region Public

        #endregion
    }

    public interface ITroubleCodesService : IService<TroubleCode>
    {

    }
}
