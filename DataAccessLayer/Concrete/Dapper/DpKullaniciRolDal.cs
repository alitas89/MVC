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
    public class DpKullaniciRolDal : DpEntityRepositoryBase<KullaniciRol>, IKullaniciRolDal
    {
        public List<KullaniciRol> GetList()
        {
            return GetListQuery($"select * from KullaniciRol", new { });
        }

        public KullaniciRol Get(int Id)
        {
            return GetQuery("select * from KullaniciRol where RolId= @Id", new { Id });
        }

        public int Add(KullaniciRol kullanicirol)
        {
            return AddQuery("insert into KullaniciRol(KullaniciRolId,KullaniciId,Silindi) values (@KullaniciRolId,@KullaniciId,@Silindi)", kullanicirol);
        }

        public int Update(KullaniciRol kullanicirol)
        {
            return UpdateQuery("update KullaniciRol set KullaniciRolId=@KullaniciRolId,KullaniciId=@KullaniciId,Silindi=@Silindi where RolId=@RolId", kullanicirol);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KullaniciRol where RolId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KullaniciRol set Silindi = 1 where RolId=@Id", new { Id });
        }
    }
}
