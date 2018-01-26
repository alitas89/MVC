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
        public string filterCol { get; set; }
        public string filterVal { get; set; }
        public string order { get; set; }
    }
}
