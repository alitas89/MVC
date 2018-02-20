using Core.EntityLayer;
using System;

namespace EntityLayer.Concrete.Varlik
{
    public class Varlik : IEntity
    {
        public int VarlikID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int VarlikDurumID { get; set; }
        public int VarlikTuruID { get; set; }
        public int VarlikGrupID { get; set; }
        public int BagliVarlikKod { get; set; }
        public int KisimID { get; set; }
        public int SarfYeriID { get; set; }
        public int IsletmeID { get; set; }
        public int MarkaID { get; set; }
        public int ModelID { get; set; }
        public string SeriNo { get; set; }
        public string BarkodKod { get; set; }
        public DateTime GarantiBitisTarih { get; set; }
        public DateTime SonKullanimTarih { get; set; }
        public string Aciklama { get; set; }
        public int ZimmetliPersonelID { get; set; }
        public int VarsayilanIsTipiID { get; set; }
        public int VarsayilanBakimArizaID { get; set; }
        public int VarsayilanArizaNedenID { get; set; }
        public int VarsayilanArizaCozumID { get; set; }
        public int EkipID { get; set; }
        public int IsEmriTurID { get; set; }
        public bool Silindi { get; set; }
    }
}
