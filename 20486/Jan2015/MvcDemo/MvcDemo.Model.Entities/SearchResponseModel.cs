using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcDemo.Model.Entities
{
    public class SearchResponseModel
    {
        public string Name { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public IList<int> Numbers { get; set; }
    }
}
