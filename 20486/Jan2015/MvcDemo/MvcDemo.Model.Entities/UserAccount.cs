using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    [Table("UserAccount")]
    public class UserAccount : AuditableEntity
    {
        [DataMember]
        public String FirstName { get; set; }

        [DataMember]
        public String LastName { get; set; }

        [DataMember]
        public String Email { get; set; }

        [DataMember]
        public Boolean IsActive { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public virtual IList<Role> Roles { get; set; }

        [DataMember]
        public virtual IList<Permission> Permissions { get; set; }
    }
}
