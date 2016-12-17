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
    #region Classe
    [Table("Cliente")]
    public class Cliente : AuditableEntity
    {
        [Required]
        [DataMember]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        [DataMember]
        public string Endereco { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+")]
        [DataMember]
        public string Email { get; set; }

        [Required]
        //[Range(0,110)]
        [IdadeChecker]
        [LargerThanValidation(1, ErrorMessage = "Idade Invalida")]
        [DataMember]
        public int Idade { get; set; }
    }
    #endregion

    #region Atributos
    [AttributeUsage(AttributeTargets.Property)]
    public class LargerThanValidationAttribute : ValidationAttribute
    {
        public int MinimumValue { get; set; }
        public LargerThanValidationAttribute(int minimum)
        {
            MinimumValue = minimum;
        }
        public override Boolean IsValid(Object value)
        {
            var valueToCompare = (int)value;
            if (valueToCompare > MinimumValue) { return true; }
            else { return false; }
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IdadeChecker : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int idade = (int)value;

            if (idade > 0 && idade <= 110)
                return true;

            return false;
        }
    }
    #endregion
}
