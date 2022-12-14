using EntityLayer.Concrete.Varlik;

namespace EntityLayer.ComplexTypes.DtoModel.Varlik
{
    public class ZimmetTransferDto : ZimmetTransfer
    {
        public string UstVarlikAd { get; set; }
        public string YeniKisimAd { get; set; }
        public string ZimmetAlanAd { get; set; }
        public string ZimmetVerenAd { get; set; }
    }
}