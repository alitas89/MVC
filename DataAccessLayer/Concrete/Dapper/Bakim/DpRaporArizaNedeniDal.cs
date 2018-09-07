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
    public class DpRaporArizaNedeniDal : DpEntityRepositoryBase<ArizaNedeni>,
         IRaporArizaNedeniDal
    {
        public List<ArizaNedeni> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<ArizaNedeni>().GetListDtoQuery($@"SELECT {columnsQuery} From ArizaNedeni Where Silindi = 0  and ArizaNedeniID IN (
                                                                            Select  ArizaNedeniID from IsEmri Where Silindi = 0
                                                                            Group By ArizaNedeniID ORDER BY COUNT(IsEmriID) DESC OFFSET 0 ROWS)
                                                                            {filterQuery} {orderQuery}
                                                                            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                                                                            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"Select Count(*) From ArizaNedeni Where Silindi = 0  and ArizaNedeniID IN (
                                            Select  ArizaNedeniID from IsEmri Where Silindi = 0 Group By ArizaNedeniID
                                                ORDER BY COUNT(IsEmriID) DESC OFFSET 0 ROWS){filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
