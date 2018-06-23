using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Iot
{
    public class DpGatewayDal : DpEntityRepositoryBase<Gateway>, IGatewayDal
    {
        public int Add(Gateway gateway)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public int DeleteSoft(int Id)
        {
            throw new NotImplementedException();
        }

        public Gateway Get(int Id)
        {
            throw new NotImplementedException();
        }

        public int GetCount(string filter = "")
        {
            throw new NotImplementedException();
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_GatewayDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<Gateway> GetList()
        {
            throw new NotImplementedException();
        }

        public List<GatewayDto> GetListDto()
        {
            return new DpDtoRepositoryBase<GatewayDto>().GetListDtoQuery("SELECT * FROM View_GatewayDto", new { });

        }

        public List<Gateway> GetListPagination(PagingParams pagingParams)
        {
            throw new NotImplementedException();
        }

        public List<GatewayDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<GatewayDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_GatewayDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int Update(Gateway obj)
        {
            throw new NotImplementedException();
        }
    }
}
