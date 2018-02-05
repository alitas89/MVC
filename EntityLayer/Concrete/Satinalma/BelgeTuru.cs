using Core.EntityLayer;

namespace EntityLayer.Concrete.Satinalma
{
    public class BelgeTuru : IEntity
    {
        public int BelgeTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool SatinalmaVarsayilan { get; set; }
        public bool IsEmriVarsayilan { get; set; }
        public bool MalzemeVarsayilan { get; set; }
        public bool VarlikKodu { get; set; }
        public bool Silindi { get; set; }
    }
}
