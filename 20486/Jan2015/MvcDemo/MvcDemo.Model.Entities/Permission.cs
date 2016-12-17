using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    [DataContract]
    public class Permission
    {
        [DataMember]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PermissionId { get; set; }

        [Required]
        [DataMember]
        public string PermissionName { get; set; }

        [Required]
        [DataMember]
        public string Controller { get; set; }

        [Required]
        [DataMember]
        public string Action { get; set; }

        [IgnoreDataMember]
        public virtual IList<UserAccount> UserAccounts { get; set; }
    }
}
