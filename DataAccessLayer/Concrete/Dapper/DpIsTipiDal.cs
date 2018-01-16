using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpIsTipiDal : DpEntityRepositoryBase<IsTipi>, IIsTipiDal
    {
        public List<IsTipi> GetList()
        {
            return GetListQuery("select * from IsTipi where Silindi=0", new { });
        }

        public IsTipi Get(int Id)
        {
            return GetQuery("select * from IsTipi where IsTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsTipi ıstipi)
        {
            return AddQuery("insert IsTipi(Kod,Ad,BakimOnceligiID,IsEmriTuruID,Aciklama,Silindi) values (@Kod,@Ad,@BakimOnceligiID,@IsEmriTuruID,@Aciklama,@Silindi)", ıstipi);
        }

        public int Update(IsTipi ıstipi)
        {
            return UpdateQuery("update IsTipi set Kod=@Kod,Ad=@Ad,BakimOnceligiID=@BakimOnceligiID,IsEmriTuruID=@IsEmriTuruID,Aciklama=@Aciklama,Silindi=@Silindi where IsTipiID=@IsTipiID", ıstipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsTipi where IsTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsTipi set Silindi = 1 where IsTipiID=@Id", new { Id });
        }
    }
}