using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Statu : IEntity
    {
        public int StatuID { get; set; }
        public int StatuTipiID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int VarlikDurumuID { get; set; }
        public int KaynakSinifi1ID { get; set; }
        public int KaynakSinifi2ID { get; set; }
        public int KaynakSinifi3ID { get; set; }
        public string Aciklama { get; set; }
        public bool BeklemeIptalNedeni { get; set; }
        public bool TalepVarsayilani { get; set; }
        public bool TalepOnay { get; set; }
        public bool TalepRed { get; set; }
        public bool EmirVarsayilani { get; set; }
        public bool IsEmriAcik { get; set; }
        public bool IsEmriKapali { get; set; }
        public bool IsEmriIptal { get; set; }
        public bool EkipmanCalismiyor { get; set; }
        public bool PlanlanmisIsEmri { get; set; }
        public bool IsEmrineBaslandi { get; set; }
        public bool IsEmriTamamlandi { get; set; }
        public bool IsTeslimEdildi { get; set; }
        public bool SorumluDegisti { get; set; }
        public bool BakimErtelendi { get; set; }
        public bool BakimDevamEdiyor { get; set; }
        public bool BildirimIslemleriniYoksay { get; set; }
        public bool KismiSatinalmaTalebiOlusturuldu { get; set; }
        public bool SatinalmaTalebiOlusturuldu { get; set; }
        public bool SatinalmaTeklifVerildi { get; set; }
        public bool SatinalmaTeklifDegerlendirildi { get; set; }
        public bool SatinalmaSiparisVerildi { get; set; }
        public bool MalzemelerinSatinalmaFiyatBelirlendi { get; set; }
        public bool SatinalmaAmbarGirisiYapildi { get; set; }
        public bool EpostaGonder { get; set; }
        public bool SMSGonder { get; set; }
        public bool HerKayitAsamasindaUygula { get; set; }
        public bool KaydiKilitle { get; set; }
        public bool Silindi { get; set; }
    }
}
