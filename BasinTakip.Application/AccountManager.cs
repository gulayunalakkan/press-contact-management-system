using BasinTakip.Domain.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasinTakip.Domain.Entities.Common;
using System.Configuration;
using System.DirectoryServices.AccountManagement;

namespace BasinTakip.Application
{
    public class AccountManager : IAccountManager
    {
        public LoginResponseType Login(string userName, string password)
        {
            string adName = ConfigurationManager.AppSettings["AD-Name"];
            string adPortalGroupName = ConfigurationManager.AppSettings["AD-PortalGroupName"];

            PrincipalContext context = null;

#if DEBUG
            // authenticates against your local machine - for development time
            context = new PrincipalContext(ContextType.Machine);
#else
            // authenticates against your Domain AD
            context = new PrincipalContext(ContextType.Domain, adName);
#endif

            bool isAuthenticated = context.ValidateCredentials(userName, password);

            if (isAuthenticated)
            {
                var userPrincipal = UserPrincipal.FindByIdentity(context, userName);
                var groupList = userPrincipal.GetGroups();
                var isUserInPortalGroup = groupList.Any(p => p.Name == adPortalGroupName);

                if (isUserInPortalGroup)
                {
                    return LoginResponseType.Succeed;
                }
                else
                {
                    return LoginResponseType.GroupAuthorizationFailed;
                }
            }
            else
            {
                return LoginResponseType.UserInformationFailed;
            }
        }
        public string GetLoginName(string userName)
        {
            string adName = ConfigurationManager.AppSettings["AD-Name"];
            string adPortalGroupName = ConfigurationManager.AppSettings["AD-PortalGroupName"];

            PrincipalContext context = null;

#if DEBUG
            // authenticates against your local machine - for development time
            context = new PrincipalContext(ContextType.Machine);
#else
            // authenticates against your Domain AD
            context = new PrincipalContext(ContextType.Domain, adName);
#endif
            var userPrincipal = UserPrincipal.FindByIdentity(context, userName);

            return userPrincipal.DisplayName;
        }
    }
}
