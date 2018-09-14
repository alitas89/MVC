using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpRaporArizaNedeniByVarlikDal : DpEntityRepositoryBase<ArizaNedeni>,
             IRaporArizaNedeniByVarlikDal
    {
        public List<ArizaNedeni> GetListPaginationDtoByVarlikID(int VarlikID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<ArizaNedeni>().GetListDtoQuery($@"SELECT {columnsQuery} FROM ArizaNedeni where Silindi=0 and ArizaNedeniID IN (
                                                                            Select  ArizaNedeniID from IsEmri Where Silindi = 0 and VarlikID = @VarlikID
                                                                            Group By ArizaNedeniID ORDER BY COUNT(IsEmriID) DESC OFFSET 0 ROWS)
                                                                             {filterQuery} {orderQuery}
                                                                 OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit, VarlikID });
        }

        public int GetCountDtoByVarlikID(int VarlikID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ArizaNedeni where Silindi=0 and ArizaNedeniID IN (
                                                                            Select  ArizaNedeniID from IsEmri Where Silindi = 0 and VarlikID = @VarlikID
                                                                            Group By ArizaNedeniID ORDER BY COUNT(IsEmriID) DESC OFFSET 0 ROWS) {filterQuery} ", new { VarlikID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
