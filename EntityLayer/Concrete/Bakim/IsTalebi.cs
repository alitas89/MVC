using Core.EntityLayer;
using System;

namespace EntityLayer.Concrete.Bakim
{
    public class IsTalebi : IEntity
    {
        public int IsTalebiID { get; set; }
        public int TalepYil { get; set; }
        public int IsEmriTuruID { get; set; }
        public int BakimOncelikID { get; set; }
        public int VarlikID { get; set; }
        public int KisimID { get; set; }
        public DateTime ArizaOlusmaTarih { get; set; }
        public string ArizaOlusmaSaat { get; set; }
        public DateTime BildirilisTarih { get; set; }
        public string BildirilisSaat { get; set; }
        public int TalepEdenID { get; set; }
        public int IsTipiID { get; set; }
        public int BakimArizaID { get; set; }
        public string Aciklama { get; set; }
        public int OnaylayanID { get; set; }
        public string OnaylayanAciklama { get; set; }
        public int SorumluID { get; set; }
        public int EkipID { get; set; }
        public DateTime OnayTarih { get; set; }
        public string OnaySaat { get; set; }
        public int StatuID { get; set; }
        public bool Silindi { get; set; }
    }
}
