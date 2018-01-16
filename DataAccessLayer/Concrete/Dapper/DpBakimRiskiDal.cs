using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBakimRiskiDal : DpEntityRepositoryBase<BakimRiski>, IBakimRiskiDal
    {
        public List<BakimRiski> GetList()
        {
            return GetListQuery("select * from BakimRiski where Silindi=0", new { });
        }

        public BakimRiski Get(int Id)
        {
            return GetQuery("select * from BakimRiski where BakimRiskiID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimRiski bakimriski)
        {
            return AddQuery("insert into BakimRiski(RiskTipiID,Kod,Ad,Formulu,StokNo,Telefon,Aciklama1,Aciklama2,Aciklama3,Silindi) values (@RiskTipiID,@Kod,@Ad,@Formulu,@StokNo,@Telefon,@Aciklama1,@Aciklama2,@Aciklama3,@Silindi)", bakimriski);
        }

        public int Update(BakimRiski bakimriski)
        {
            return UpdateQuery("update BakimRiski set RiskTipiID=@RiskTipiID,Kod=@Kod,Ad=@Ad,Formulu=@Formulu,StokNo=@StokNo,Telefon=@Telefon,Aciklama1=@Aciklama1,Aciklama2=@Aciklama2,Aciklama3=@Aciklama3,Silindi=@Silindi where BakimRiskiID=@BakimRiskiID", bakimriski);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimRiski where BakimRiskiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimRiski set Silindi = 1 where BakimRiskiID=@Id", new { Id });
        }
    }
}