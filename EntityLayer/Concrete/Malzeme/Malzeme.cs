using Core.EntityLayer;

namespace EntityLayer.Concrete.Malzeme
{
    public class Malzeme : IEntity
    {
        public int MalzemeID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int OlcuBirimID { get; set; }
        public string MalzemeGrupID { get; set; }
        public string MalzemeAltGrupID { get; set; }
        public string SeriNo { get; set; }
        public int MarkaID { get; set; }
        public int ModelID { get; set; }
        public bool Silindi { get; set; }
    }
}
