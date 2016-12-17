using MvcDemo.Framework.Cache;
using MvcDemo.Framework.Wcf.ServiceManager;
using MvcDemo.Model.Entities;
using MvcDemo.Services.AccountManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Providers;
using System.Web.Security;

namespace MvcDemo.Framework.Secutiry
{
    public class WcfDefaultMembershipProvider : 
        DefaultMembershipProvider
    {
        private IAccountManagementService _service = null;

        public WcfDefaultMembershipProvider() 
        {
            _service = ServiceLocator.GetInstance<IAccountManagementService>();
        }
        public override bool ValidateUser(string username, string password)
        {
            UserAccount userAccount = _service.ValidateUser(username, password);

            if (null != userAccount) 
            {
                var roles = userAccount.Roles.Select(m => m.RoleName).ToArray();
                CacheManager.GetOrAdd<IList<Permission>>(userAccount.Id.ToString(), 
                                                  () => userAccount.Permissions.ToList());

                CustomPrincipalSerialize serializeModel = new CustomPrincipalSerialize();
                serializeModel.UserId = userAccount.Id;
                serializeModel.FirstName = userAccount.FirstName;
                serializeModel.LastName = userAccount.LastName;
                serializeModel.roles = roles;
                
                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                         1,
                        userAccount.Email,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(15),
                         false,
                         userData);

                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, 
                                                    encTicket);
                HttpContext.Current.Response.Cookies.Add(faCookie);
                return true;    
            }
            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            try
            {
                Role roleDefault = _service.GetRoleByName("User");
                UserAccount user = new UserAccount
                {
                    UserName = username,
                    Email = email,
                    FirstName = username,
                    Password = password,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow,
                    Roles = new List<Role>() { roleDefault }
                };
                _service.Add(user);

                status = MembershipCreateStatus.Success;
                                        // Mesmo nome definido no WebConfig
                return new MembershipUser("WcfDefaultMembershipProvider", 
                                          username,
                                          providerUserKey,
                                          email,
                                          passwordQuestion,
                                          null, isApproved,
                                          false,
                                          DateTime.Now,
                                          DateTime.Now,
                                          DateTime.Now,
                                          DateTime.Now,
                                          DateTime.Now);
            }
            catch (Exception)
            {
                status = MembershipCreateStatus.UserRejected;
                return null;
            }
        }
    }
}
