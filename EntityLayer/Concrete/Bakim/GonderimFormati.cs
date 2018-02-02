using Core.EntityLayer;

namespace EntityLayer.Concrete.Bakim
{
    public class GonderimFormati : IEntity
    {
        public int GonderimFormatiID { get; set; }
        public int GonderimTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Konu { get; set; }
        public string Format { get; set; }
        public bool Silindi { get; set; }
    }
}
