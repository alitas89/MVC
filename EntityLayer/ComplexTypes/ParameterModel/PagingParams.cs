using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ComplexTypes.ParameterModel
{
    public class PagingParams
    {

        public int? offset { get; set; }
        public int? limit { get; set; }
        public string filter { get; set; }
        public string order { get; set; }
        public string columns { get; set; }
    }
}
