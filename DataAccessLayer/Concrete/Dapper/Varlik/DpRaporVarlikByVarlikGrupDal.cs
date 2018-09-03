using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpRaporVarlikByVarlikGrupDal : DpEntityRepositoryBase<EntityLayer.Concrete.Varlik.Varlik>,
        IRaporVarlikByVarlikGrupDal
    {
        //VarlikGrupID ye göre gelen Varlıklar için pagination
        public List<VarlikDto> GetListPaginationDtoByVarlikGrupID(int VarlikGrupID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikDto where Silindi=0 and VarlikGrupID=@VarlikGrupID {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit, VarlikGrupID });
        }

        //VarlikGrupID ye göre gelen Varlıkların count
        public int GetCountDtoByVarlikGrupID(int VarlikGrupID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikDto where Silindi=0 and VarlikGrupID= @VarlikGrupID {filterQuery} ", new { VarlikGrupID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

     
    }
}
