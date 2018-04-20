using EntityLayer.Concrete.Malzeme;
using System;

namespace EntityLayer.ComplexTypes.DtoModel.Malzeme
{
    public class MalzemeHareketDto : MalzemeHareket
    {
        public string AmbarAd { get; set; }
        public string Ambar2Ad { get; set; }
        public string HareketTuruAd { get; set; }
        public DateTime FisTarih { get; set; }
        public string FisSaat { get; set; }
        public int DurumID { get; set; }

    }
}
