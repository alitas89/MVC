using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpMarkaDal : DpEntityRepositoryBase<Marka>, IMarkaDal
    {
        public List<Marka> GetList()
        {
            return GetListQuery($"select * from Marka where Silindi=0", new { });
        }

        public Marka Get(int Id)
        {
            return GetQuery("select * from Marka where MarkaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Marka marka)
        {
            return AddQuery("insert Marka(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", marka);
        }

        public int Update(Marka marka)
        {
            return UpdateQuery("update Marka set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where MarkaID=@MarkaID", marka);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Marka where MarkaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Marka set Silindi = 1 where MarkaID=@Id", new { Id });
        }
    }
}