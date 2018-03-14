using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
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

        public List<RiskTipi> GetListPagination(PagingParams pagingParams)
        {
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM RiskTipi where Silindi=0 {filterQuery} {orderQuery}
        OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM RiskTipi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}