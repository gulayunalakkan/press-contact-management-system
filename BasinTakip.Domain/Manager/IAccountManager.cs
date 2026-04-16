using BasinTakip.Core.Business;
using BasinTakip.Core.Dependecy;
using BasinTakip.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasinTakip.Domain.Manager
{
    public interface IAccountManager: IManager
    {
        [HandleError]
        LoginResponseType Login(string userName, string password);
        [HandleError]
        string GetLoginName(string userName);
    }
}
