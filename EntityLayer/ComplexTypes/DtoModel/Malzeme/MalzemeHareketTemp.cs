using System;

namespace EntityLayer.ComplexTypes.DtoModel.Malzeme
{
    public class MalzemeHareketTemp
    {
        public int MalzemeHareketID { get; set; }
        public int MalzemeHareketFisNo { get; set; }
        public DateTime FisTarih { get; set; }
        public string FisSaat { get; set; }
        public int AmbarID { get; set; }
        public int Ambar2ID { get; set; }
        public int MalzemeHareketTuruID { get; set; }
        public string Aciklama { get; set; }
        public string arrMalzeme { get; set; }
    }
}
