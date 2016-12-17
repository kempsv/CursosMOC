using MvcDemo.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Framework.Secutiry
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string[] roles)
        {
            bool isInRole = false;
            foreach (string role in roles)
            {
                isInRole = IsInRole(role);
                if (isInRole)
                    break;
            }
            return isInRole;
        }
        public bool IsInRole(string role)
        {
            return roles.Any(r => role.Contains(r)) ? true : false;
        }
        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] roles { get; set; }
        public IList<Permission> Permissions { get; set; }

        public bool HasPermission(IList<Permission> permissions)
        {
            bool hasPermission = false;
            foreach (Permission permission in permissions)
            {
                hasPermission = HasPermission(permission.PermissionName, permission.Controller, permission.Action);
                if (hasPermission)
                    break;
            }
            return hasPermission;
        }
        public bool HasPermission(string permission, string controller, string action)
        {
            return Permissions.Any(r => (r.PermissionName.Equals(permission) || r.PermissionName.Equals("*"))
                && r.Controller.Equals(controller) || r.Controller.Equals("*")
                && r.Action.Equals(action) || r.Action.Equals("*")) ? true : false;
        }
    }
}
