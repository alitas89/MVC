using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpOncelikDal : DpEntityRepositoryBase<Oncelik>, IOncelikDal
    {
        public List<Oncelik> GetList()
        {
            return GetListQuery("select * from Oncelik where Silindi=0", new { });
        }

        public Oncelik Get(int Id)
        {
            return GetQuery("select * from Oncelik where OncelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(Oncelik oncelik)
        {
            return AddQuery("insert into Oncelik(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", oncelik);
        }

        public int Update(Oncelik oncelik)
        {
            return UpdateQuery("update Oncelik set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where OncelikID=@OncelikID", oncelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Oncelik where OncelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Oncelik set Silindi = 1 where OncelikID=@Id", new { Id });
        }
    }
}