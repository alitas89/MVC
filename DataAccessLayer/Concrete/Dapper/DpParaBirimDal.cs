using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpParaBirimDal : DpEntityRepositoryBase<ParaBirim>, IParaBirimDal
    {
        public List<ParaBirim> GetList()
        {
            return GetListQuery("select * from ParaBirim where Silindi=0", new { });
        }

        public ParaBirim Get(int Id)
        {
            return GetQuery("select * from ParaBirim where ParaBirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(ParaBirim parabirim)
        {
            return AddQuery("insert ParaBirim(ParaBirimAd) values (@ParaBirimAd)", parabirim);
        }

        public int Update(ParaBirim parabirim)
        {
            return UpdateQuery("update ParaBirim set ParaBirimAd=@ParaBirimAd where ParaBirimID=@ParaBirimID", parabirim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ParaBirim where ParaBirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ParaBirim set Silindi = 1 where ParaBirimID=@Id", new { Id });
        }
    }
}