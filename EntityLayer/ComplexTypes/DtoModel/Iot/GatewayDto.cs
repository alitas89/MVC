using EntityLayer.Concrete.Iot;
using System;

namespace EntityLayer.ComplexTypes.DtoModel.Iot
{
    public class GatewayDto : Gateway
    {
        public DateTime EnSonVeriTarih { get; set; }
        public string EnerjiDurumu { get; set; }
        public string Sicaklik { get; set; }
        public int OkunanSayac { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int VarlikID { get; set; }
    }
}
