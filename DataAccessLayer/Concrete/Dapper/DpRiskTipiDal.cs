using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpRiskTipiDal : DpEntityRepositoryBase<RiskTipi>, IRiskTipiDal
    {
        public List<RiskTipi> GetList()
        {
            return GetListQuery("select * from RiskTipi where Silindi=0", new { });
        }

        public RiskTipi Get(int Id)
        {
            return GetQuery("select * from RiskTipi where RiskTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(RiskTipi risktipi)
        {
            return AddQuery("insert into RiskTipi(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", risktipi);
        }

        public int Update(RiskTipi risktipi)
        {
            return UpdateQuery("update RiskTipi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where RiskTipiID=@RiskTipiID", risktipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from RiskTipi where RiskTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update RiskTipi set Silindi = 1 where RiskTipiID=@Id", new { Id });
        }
    }
}