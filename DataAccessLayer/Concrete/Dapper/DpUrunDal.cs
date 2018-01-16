using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpUrunDal : DpEntityRepositoryBase<Urun>, IUrunDal
    {
        public List<Urun> GetList()
        {
            return GetListQuery($"select * from Urun where Silindi=0", new { });
        }

        public Urun Get(int Id)
        {
            return GetQuery("select * from Urun where UrunID= @Id and Silindi=0", new { Id });
        }

        public int Add(Urun urun)
        {
            return AddQuery("insert into Urun(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", urun);
        }

        public int Update(Urun urun)
        {
            return UpdateQuery("update Urun set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where UrunID=@UrunID", urun);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Urun where UrunID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Urun set Silindi = 1 where UrunID=@Id", new { Id });
        }
    }
}