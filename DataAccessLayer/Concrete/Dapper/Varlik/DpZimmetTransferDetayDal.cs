﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpZimmetTransferDetayDal : DpEntityRepositoryBase<ZimmetTransferDetay>, IZimmetTransferDetayDal
    {
        public List<ZimmetTransferDetay> GetList()
        {
            return GetListQuery("select * from ZimmetTransferDetay where Silindi=0", new { });
        }

        public ZimmetTransferDetay Get(int Id)
        {
            return GetQuery("select * from ZimmetTransferDetay where ZimmetTransferDetayID= @Id and Silindi=0", new { Id });
        }

        public int Add(ZimmetTransferDetay zimmettransferdetay)
        {
            return AddQuery("insert into ZimmetTransferDetay(VarlikID,ZimmetTransferID) values (@VarlikID,@ZimmetTransferID)", zimmettransferdetay);
        }

        public int Update(ZimmetTransferDetay zimmettransferdetay)
        {
            return UpdateQuery("update ZimmetTransferDetay set VarlikID=@VarlikID,ZimmetTransferID=@ZimmetTransferID where ZimmetTransferDetayID=@ZimmetTransferDetayID", zimmettransferdetay);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ZimmetTransferDetay where ZimmetTransferDetayID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ZimmetTransferDetay set Silindi = 1 where ZimmetTransferDetayID=@Id", new { Id });
        }

        public List<ZimmetTransferDetay> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM ZimmetTransferDetay where Silindi=0 {filterQuery} {orderQuery}
                        OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ZimmetTransferDetay {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}