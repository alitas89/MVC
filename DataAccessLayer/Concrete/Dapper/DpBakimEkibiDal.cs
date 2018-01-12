using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBakimEkibiDal : DpEntityRepositoryBase<BakimEkibi>, IBakimEkibiDal
    {
        public List<BakimEkibi> GetList()
        {
            return GetListQuery("select * from BakimEkibi where Silindi=0", new { });
        }

        public BakimEkibi Get(int Id)
        {
            return GetQuery("select * from BakimEkibi where BakimEkibiID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimEkibi bakimekibi)
        {
            return AddQuery("insert BakimEkibi(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", bakimekibi);
        }

        public int Update(BakimEkibi bakimekibi)
        {
            return UpdateQuery("update BakimEkibi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where BakimEkibiID=@BakimEkibiID", bakimekibi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimEkibi where BakimEkibiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimEkibi set Silindi = 1 where BakimEkibiID=@Id", new { Id });
        }
    }
}