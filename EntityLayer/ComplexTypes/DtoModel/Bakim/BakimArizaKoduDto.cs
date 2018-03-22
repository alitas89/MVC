using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class BakimArizaKoduDto: BakimArizaKodu
    {
        public string IsTipiAd { get; set; }
        public string BakimOncelikAd { get; set; }
        public string RiskTipiAd { get; set; }
        public string BirimAd { get; set; }
        public string UretimTipiAd { get; set; }
    }
}
