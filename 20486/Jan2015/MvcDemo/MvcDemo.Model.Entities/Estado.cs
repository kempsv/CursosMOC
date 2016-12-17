using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    public class Estado
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public IList<Cidade> Cidades { get; set; }
    }
}
