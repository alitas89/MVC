using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class Kaynak : IEntity
    {
        public int KaynakID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int KisimID { get; set; }
        public int SarfyeriID { get; set; }
        public int IsletmeID { get; set; }
        public int VarlikID { get; set; }
        public int EkipID { get; set; }
        public int KaynakSinifID { get; set; }
        public int VardiyaID { get; set; }
        public int StatuID { get; set; }
        public int AmbarID { get; set; }
        public int KaynakPozisyonuID { get; set; }
        public int DurusIsTipiID { get; set; }
        public int KaynakTipiID { get; set; }
        public int KaynakDurumuID { get; set; }
        public int KaynakTuruID { get; set; }
        public string Email { get; set; }
        public string TelefonNo { get; set; }
        public bool Silindi { get; set; }
    }

}
