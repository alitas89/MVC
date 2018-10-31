using Core.EntityLayer;

namespace EntityLayer.Concrete.Iot
{
    public class Gateway : IEntity
    {
        public string SeriNo { get; set; }
        public string GsmNo { get; set; }        
        public string Aciklama { get; set; }
        public string Mahalle { get; set; }
        public string Ilce { get; set; }
        public string ModemNo { get; set; }
        public string LokasyonNo { get; set; }
        public string Adres { get; set; }
        public string EtiketNo { get; set; }
        public string Path { get; set; }
    }
}
