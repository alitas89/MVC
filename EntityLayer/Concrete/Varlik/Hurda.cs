using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Varlik
{
    public class Hurda : IEntity
    {
        public int HurdaID { get; set; }
        public string BarkodKod { get; set; }
        public int VarlikID { get; set; }
        public string OzurKod { get; set; }
        public string OzurAd { get; set; }
        public string OzurTip { get; set; }
        public DateTime Tarih { get; set; }
        public int Miktar { get; set; }
        public int Toplam { get; set; }
        public string Aciklama { get; set; }
    }
}
