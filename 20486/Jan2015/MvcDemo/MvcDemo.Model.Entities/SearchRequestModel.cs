using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    public class SearchRequestModel
    {
        public string Name { get; set; }
        public IEnumerable<int> Numbers { get; set; }
    }

    
}
