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
    public class DpKullaniciDal : DpEntityRepositoryBase<Kullanici>, IKullaniciDal
    {
        public List<Kullanici> GetList()
        {
            return GetListQuery($"select * from Kullanici", new { });
        }

        public Kullanici Get(int Id)
        {
            return GetQuery("select * from Kullanici where KullaniciId= @Id", new { Id });
        }

        public int Add(Kullanici kullanici)
        {
            return AddQuery("insert Kullanici(KullaniciAdi,Sifre,Ad,Soyad,Email,Silindi) values (@KullaniciAdi,@Sifre,@Ad,@Soyad,@Email,@Silindi)", kullanici);
        }

        public int Update(Kullanici kullanici)
        {
            return UpdateQuery("update Kullanici set KullaniciAdi=@KullaniciAdi,Sifre=@Sifre,Ad=@Ad,Soyad=@Soyad,Email=@Email,Silindi=@Silindi where KullaniciId=@KullaniciId", kullanici);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kullanici where KullaniciId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kullanici set Silindi = 1 where KullaniciId=@Id", new { Id });
        }

        public Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre)
        {
            return GetQuery("select * from Kullanici where KullaniciAdi= @kullaniciAdi and Sifre = @sifre",
                new { kullaniciAdi, sifre });
        }
    }
}
