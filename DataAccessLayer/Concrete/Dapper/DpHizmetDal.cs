using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpHizmetDal : DpEntityRepositoryBase<Hizmet>, IHizmetDal
    {
        public List<Hizmet> GetList()
        {
            return GetListQuery("select * from Hizmet where Silindi=0", new { });
        }

        public Hizmet Get(int Id)
        {
            return GetQuery("select * from Hizmet where HizmetID= @Id and Silindi=0", new { Id });
        }

        public int Add(Hizmet hizmet)
        {
            return AddQuery("insert into Hizmet(Kod,Ad,BirimFiyat,ParaBirimID,Aciklama,Silindi) values (@Kod,@Ad,@BirimFiyat,@ParaBirimID,@Aciklama,@Silindi)", hizmet);
        }

        public int Update(Hizmet hizmet)
        {
            return UpdateQuery("update Hizmet set Kod=@Kod,Ad=@Ad,BirimFiyat=@BirimFiyat,ParaBirimID=@ParaBirimID,Aciklama=@Aciklama,Silindi=@Silindi where HizmetID=@HizmetID", hizmet);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Hizmet where HizmetID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Hizmet set Silindi = 1 where HizmetID=@Id", new { Id });
        }
    }
}