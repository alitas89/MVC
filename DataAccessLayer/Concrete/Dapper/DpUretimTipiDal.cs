using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpUretimTipiDal : DpEntityRepositoryBase<UretimTipi>, IUretimTipiDal
    {
        public List<UretimTipi> GetList()
        {
            return GetListQuery("select * from UretimTipi where Silindi=0", new { });
        }

        public UretimTipi Get(int Id)
        {
            return GetQuery("select * from UretimTipi where UretimTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(UretimTipi uretimtipi)
        {
            return AddQuery("insert into UretimTipi(UretimTipiAd) values (@UretimTipiAd)", uretimtipi);
        }

        public int Update(UretimTipi uretimtipi)
        {
            return UpdateQuery("update UretimTipi set UretimTipiAd=@UretimTipiAd where UretimTipiID=@UretimTipiID", uretimtipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from UretimTipi where UretimTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update UretimTipi set Silindi = 1 where UretimTipiID=@Id", new { Id });
        }
    }
}
