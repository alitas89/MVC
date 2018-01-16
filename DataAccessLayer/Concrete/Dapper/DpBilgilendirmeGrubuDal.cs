using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBilgilendirmeGrubuDal : DpEntityRepositoryBase<BilgilendirmeGrubu>, IBilgilendirmeGrubuDal
    {
        public List<BilgilendirmeGrubu> GetList()
        {
            return GetListQuery("select * from BilgilendirmeGrubu where Silindi=0", new { });
        }

        public BilgilendirmeGrubu Get(int Id)
        {
            return GetQuery("select * from BilgilendirmeGrubu where BilgilendirmeGrubuID= @Id and Silindi=0", new { Id });
        }

        public int Add(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return AddQuery("insert into BilgilendirmeGrubu(BilgilendirmeTuruID,Kod,Ad,YetkiKodu,Aciklama,Silindi) values (@BilgilendirmeTuruID,@Kod,@Ad,@YetkiKodu,@Aciklama,@Silindi)", bilgilendirmegrubu);
        }

        public int Update(BilgilendirmeGrubu bilgilendirmegrubu)
        {
            return UpdateQuery("update BilgilendirmeGrubu set BilgilendirmeTuruID=@BilgilendirmeTuruID,Kod=@Kod,Ad=@Ad,YetkiKodu=@YetkiKodu,Aciklama=@Aciklama,Silindi=@Silindi where BilgilendirmeGrubuID=@BilgilendirmeGrubuID", bilgilendirmegrubu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BilgilendirmeGrubu where BilgilendirmeGrubuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BilgilendirmeGrubu set Silindi = 1 where BilgilendirmeGrubuID=@Id", new { Id });
        }
    }
}