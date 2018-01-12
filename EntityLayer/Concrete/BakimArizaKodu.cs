using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class BakimArizaKodu : IEntity
    {
        public int BakimArizaKoduID { get; set; }
        public string Kod { get; set; }
        public bool GenelKod { get; set; }
        public string Ad { get; set; }
        public int IsTipiID { get; set; }
        public int BakimOnceligiID { get; set; }
        public int TalimatKoduID { get; set; }
        public int RiskKoduID { get; set; }
        public int BakimPeriyodu { get; set; }
        public int BirimID { get; set; }
        public int BakimSuresi { get; set; }
        public int BakimPuanı { get; set; }
        public string Etiket { get; set; }
        public bool SurecPerformansinaDahil { get; set; }
        public string Aciklama { get; set; }
        public int UretimTipiID { get; set; }
        public bool Silindi { get; set; }
    }
}
