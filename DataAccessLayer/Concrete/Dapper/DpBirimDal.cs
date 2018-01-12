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
    public class DpBirimDal : DpEntityRepositoryBase<Birim>, IBirimDal
    {
        public List<Birim> GetList()
        {
            return GetListQuery("select * from Birim where Silindi=0", new { });
        }

        public Birim Get(int Id)
        {
            return GetQuery("select * from Birim where BirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(Birim birim)
        {
            return AddQuery("insert Birim(BirimAd) values (@BirimAd)", birim);
        }

        public int Update(Birim birim)
        {
            return UpdateQuery("update Birim set BirimAd=@BirimAd where BirimID=@BirimID", birim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Birim where BirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Birim set Silindi = 1 where BirimID=@Id", new { Id });
        }
    }
}
