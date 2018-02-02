using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class ArizaNedeni : IEntity
    {
        public int ArizaNedeniID { get; set; }
        public string Kod { get; set; }
        public bool GenelKod { get; set; }
        public string Ad { get; set; }
        public bool UretimiDurdurur { get; set; }
        public int NedenAnaliziZorunluOlmali { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
