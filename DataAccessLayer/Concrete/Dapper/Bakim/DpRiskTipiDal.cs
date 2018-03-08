using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
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
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

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
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM RiskTipi where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}