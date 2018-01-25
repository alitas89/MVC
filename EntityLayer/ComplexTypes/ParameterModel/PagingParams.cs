using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ComplexTypes.ParameterModel
{
    public class PagingParams
    {
        public int? offset;
        public int? limit;
        public string filterCol = "";
        public string filterVal = "";
    }
}
