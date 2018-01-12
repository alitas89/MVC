using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBilgilendirmeTuruDal : DpEntityRepositoryBase<BilgilendirmeTuru>, IBilgilendirmeTuruDal
    {
        public List<BilgilendirmeTuru> GetList()
        {
            return GetListQuery("select * from BilgilendirmeTuru where Silindi=0", new { });
        }

        public BilgilendirmeTuru Get(int Id)
        {
            return GetQuery("select * from BilgilendirmeTuru where BilgilendirmeTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(BilgilendirmeTuru bilgilendirmeturu)
        {
            return AddQuery("insert BilgilendirmeTuru(BilgilendirmeTuruAd) values (@BilgilendirmeTuruAd)", bilgilendirmeturu);
        }

        public int Update(BilgilendirmeTuru bilgilendirmeturu)
        {
            return UpdateQuery("update BilgilendirmeTuru set BilgilendirmeTuruAd=@BilgilendirmeTuruAd where BilgilendirmeTuruID=@BilgilendirmeTuruID", bilgilendirmeturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BilgilendirmeTuru where BilgilendirmeTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BilgilendirmeTuru set Silindi = 1 where BilgilendirmeTuruID=@Id", new { Id });
        }
    }
}