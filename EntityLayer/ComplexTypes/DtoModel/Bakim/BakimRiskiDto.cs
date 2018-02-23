using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class BakimRiskiDto : BakimRiski
    {
        public string RiskTipiAd { get; set; }
    }
}
