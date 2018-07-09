using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.ComplexTypes.DtoModel.Iot
{
    public class GatewayDto : Gateway
    {
        public DateTime EnSonVeriTarih { get; set; }
        public string EnerjiDurumu { get; set; }
        public string Sicaklik { get; set; }
        public int OkunanSayac { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
