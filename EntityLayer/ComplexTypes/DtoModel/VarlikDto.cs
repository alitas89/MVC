using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Varlik;

namespace EntityLayer.ComplexTypes.DtoModel
{
    public class VarlikDto : Varlik
    {
        public string IsEmriTuruAd { get; set; }
        public string BakimEkibiAd { get; set; }
        public string ArizaCozumuAd { get; set; }
        public string ArizaNedeniAd { get; set; }
        public string BakimArizaKoduAd { get; set; }
        public string IsTipiAd { get; set; }
        public string ModelAd { get; set; }
        public string MarkaAd { get; set; }
        public string IsletmeAd { get; set; }
        public string SarfYeriAd { get; set; }
        public string KisimAd { get; set; }
        public string VarlikGrupAd { get; set; }
        public string VarlikTuruAd { get; set; }
        public string VarlikDurumAd { get; set; }
    }
}
