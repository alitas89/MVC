using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.Bakim;

namespace EntityLayer.ComplexTypes.DtoModel.Bakim
{
    public class StatuDto : Statu
    {
        public string StatuTipiAd { get; set; }
        public string VarlikDurumuAd { get; set; }
        public string KaynakSinifi1Ad { get; set; }
        public string KaynakSinifi2Ad { get; set; }
        public string KaynakSinifi3Ad { get; set; }
    }
}
