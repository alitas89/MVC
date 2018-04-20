namespace EntityLayer.ComplexTypes.DtoModel.Malzeme
{
    public class MalzemeDto : Concrete.Malzeme.Malzeme
    {
        public string OlcuBirimAd { get; set; }
        public string MalzemeGrupAd { get; set; }
        public string MalzemeAltGrupAd { get; set; }
        public string MarkaAd { get; set; }
        public string ModelAd { get; set; }
        public double ToplamMiktar { get; set; }
    }
}
