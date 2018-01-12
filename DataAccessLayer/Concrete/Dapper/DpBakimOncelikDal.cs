using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBakimOncelikDal : DpEntityRepositoryBase<BakimOncelik>, IBakimOncelikDal
    {
        public List<BakimOncelik> GetList()
        {
            return GetListQuery("select * from BakimOncelik where Silindi=0", new { });
        }

        public BakimOncelik Get(int Id)
        {
            return GetQuery("select * from BakimOncelik where BakimOncelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimOncelik bakimoncelik)
        {
            return AddQuery("insert BakimOncelik(Kod,Ad,TamamlanmaZamani,BirimID,Aciklama,TeminSureleriID,IsEmriVarsayilani,IsTalepVarsayilani,PeriyodikBakimVarsayilani,Silindi) values (@Kod,@Ad,@TamamlanmaZamani,@BirimID,@Aciklama,@TeminSureleriID,@IsEmriVarsayilani,@IsTalepVarsayilani,@PeriyodikBakimVarsayilani,@Silindi)", bakimoncelik);
        }

        public int Update(BakimOncelik bakimoncelik)
        {
            return UpdateQuery("update BakimOncelik set Kod=@Kod,Ad=@Ad,TamamlanmaZamani=@TamamlanmaZamani,BirimID=@BirimID,Aciklama=@Aciklama,TeminSureleriID=@TeminSureleriID,IsEmriVarsayilani=@IsEmriVarsayilani,IsTalepVarsayilani=@IsTalepVarsayilani,PeriyodikBakimVarsayilani=@PeriyodikBakimVarsayilani,Silindi=@Silindi where BakimOncelikID=@BakimOncelikID", bakimoncelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimOncelik where BakimOncelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimOncelik set Silindi = 1 where BakimOncelikID=@Id", new { Id });
        }
    }
}