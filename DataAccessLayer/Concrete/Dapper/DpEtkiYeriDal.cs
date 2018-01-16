using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpEtkiYeriDal : DpEntityRepositoryBase<EtkiYeri>, IEtkiYeriDal
    {
        public List<EtkiYeri> GetList()
        {
            return GetListQuery("select * from EtkiYeri where Silindi=0", new { });
        }

        public EtkiYeri Get(int Id)
        {
            return GetQuery("select * from EtkiYeri where EtkiYeriID= @Id and Silindi=0", new { Id });
        }

        public int Add(EtkiYeri etkiyeri)
        {
            return AddQuery("insert into EtkiYeri(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", etkiyeri);
        }

        public int Update(EtkiYeri etkiyeri)
        {
            return UpdateQuery("update EtkiYeri set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where EtkiYeriID=@EtkiYeriID", etkiyeri);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from EtkiYeri where EtkiYeriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update EtkiYeri set Silindi = 1 where EtkiYeriID=@Id", new { Id });
        }
    }
}