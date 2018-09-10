using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{

    public class DpRaporVarlikByArizaNedeniDal : DpEntityRepositoryBase<EntityLayer.Concrete.Varlik.Varlik>,
            IRaporVarlikByArizaNedeniDal
    {
        //VarlikGrupID ye göre gelen Varlıklar için pagination
        public List<VarlikDto> GetListPaginationDtoByArizaNedeniID(int ArizaNedeniID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikDto where Silindi=0 and VarlikID IN(
                                                                            Select  VarlikID from IsEmri Where Silindi = 0 and ArizaNedeniID=@ArizaNedeniID
                                                                            Group By VarlikID ORDER BY COUNT(IsEmriID) DESC OFFSET 0 ROWS)
                                                                             {filterQuery} {orderQuery}
                                                                 OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit, ArizaNedeniID });
        }

        //VarlikGrupID ye göre gelen Varlıkların count
        public int GetCountDtoByArizaNedeniID(int ArizaNedeniID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikDto where Silindi=0 and VarlikID IN(
                                                                            Select  VarlikID from IsEmri Where Silindi = 0 and ArizaNedeniID=@ArizaNedeniID
                                                                            Group By VarlikID ORDER BY COUNT(IsEmriID) DESC OFFSET 0 ROWS) {filterQuery} ", new { ArizaNedeniID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }


    }
}
