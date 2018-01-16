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
    public class DpVarlikDurumuDal : DpEntityRepositoryBase<VarlikDurumu>, IVarlikDurumuDal
    {
        public List<VarlikDurumu> GetList()
        {
            return GetListQuery($"select * from VarlikDurumu where Silindi=0", new { });
        }

        public VarlikDurumu Get(int Id)
        {
            return GetQuery("select * from VarlikDurumu where VarlikDurumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikDurumu varlikdurumu)
        {
            return AddQuery("insert into VarlikDurumu(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", varlikdurumu);
        }

        public int Update(VarlikDurumu varlikdurumu)
        {
            return UpdateQuery("update VarlikDurumu set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where VarlikDurumuID=@VarlikDurumuID", varlikdurumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikDurumu where VarlikDurumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikDurumu set Silindi = 1 where VarlikDurumuID=@Id", new { Id });
        }
    }
}
