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
    public class Role
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int RoleId { get; set; }

        [Required]
        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string Description { get; set; }

        [IgnoreDataMember]
        public virtual IList<UserAccount> UserAccounts { get; set; }
    }
}
