using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    /// <summary>
    /// ScaffoldColumn(false) is used So that ASP.NET MVC 
    /// Scaffolding will NOT generate controls for this in Views. 
    /// We will handle these properties in context SaveChanges method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        [DataMember]
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }

        [DataMember]
        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string CreatedBy { get; set; }

        [DataMember]
        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }

        [DataMember]
        [MaxLength(256)]
        [ScaffoldColumn(false)]
        public string UpdatedBy { get; set; }
    }
}
