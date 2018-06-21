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
        public double SonEndeks { get; set; }
        public string EnerjiVarYok { get; set; }
        public string GatewaySicaklik { get; set; }
    }
}
