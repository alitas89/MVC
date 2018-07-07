using System;
using EntityLayer.Concrete.Iot;

namespace EntityLayer.ComplexTypes.DtoModel.Iot
{
    public class AlarmKosulDto : AlarmKosul
    {
        public string OznitelikAd { get; set; }
        public string KosulTipAd { get; set; }
    }
}