using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpStatuTipiDal : DpEntityRepositoryBase<StatuTipi>, IStatuTipiDal
    {
        public List<StatuTipi> GetList()
        {
            return GetListQuery("select * from StatuTipi where Silindi=0", new { });
        }

        public StatuTipi Get(int Id)
        {
            return GetQuery("select * from StatuTipi where StatuTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(StatuTipi statutipi)
        {
            return AddQuery("insert StatuTipi(StatuTipiAd) values (@StatuTipiAd)", statutipi);
        }

        public int Update(StatuTipi statutipi)
        {
            return UpdateQuery("update StatuTipi set StatuTipiAd=@StatuTipiAd where StatuTipiID=@StatuTipiID", statutipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from StatuTipi where StatuTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update StatuTipi set Silindi = 1 where StatuTipiID=@Id", new { Id });
        }
    }
}