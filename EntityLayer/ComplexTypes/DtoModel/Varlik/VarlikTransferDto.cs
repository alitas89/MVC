using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Varlik;

namespace EntityLayer.ComplexTypes.DtoModel.Varlik
{
    public class VarlikTransferDto: VarlikTransfer
    {
        public string VarlikAd { get; set; }
        public string EskiKisimAd { get; set; }
        public string EskiSahipVarlikAd { get; set; }
        public string YeniSahipVarlikAd { get; set; }
        public string YeniKisimAd { get; set; }
    }
}
