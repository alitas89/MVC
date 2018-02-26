using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Varlik
{
    public class AkaryakitAlimFis : IEntity
    {
        public int AkaryakitAlimFisID { get; set; }
        public int AracID { get; set; }
        public int YakitID { get; set; }
        public int AmbarID { get; set; }
        public decimal Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public decimal Iskonto { get; set; }
        public int MasrafYeriID { get; set; }
        public int YakitAlanKisiID { get; set; }
        public int SaticiID { get; set; }
        public DateTime YakitAlimTarih { get; set; }
        public string YakitAlimSaat { get; set; }
        public decimal AracKm { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
        public string FisNo { get; set; }
        public decimal ToplamAkaryakitTutari { get; set; }
    }
}
