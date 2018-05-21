using System;

namespace EntityLayer.ComplexTypes.DtoModel.Sistem
{
    public class IsEmriBakimSonucBildirimTemp
    {
        public int IsEmriNoID { get; set; }
        public int IsEmriID { get; set; }
        public int BakimDurumuID { get; set; }
        public DateTime BitisTarih { get; set; }
        public string BitisSaat { get; set; }
        public string BakimDurumuAd { get; set; }
    }
}
