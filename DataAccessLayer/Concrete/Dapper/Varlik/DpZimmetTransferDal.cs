using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpZimmetTransferDal : DpEntityRepositoryBase<ZimmetTransfer>, IZimmetTransferDal
    {
        public List<ZimmetTransfer> GetList()
        {
            return GetListQuery("select * from ZimmetTransfer where Silindi=0", new { });
        }

        public ZimmetTransfer Get(int Id)
        {
            return GetQuery("select * from ZimmetTransfer where ZimmetTransferID= @Id and Silindi=0", new { Id });
        }

        public int Add(ZimmetTransfer zimmettransfer)
        {
            return AddQuery("insert into ZimmetTransfer(TransferNo,TeslimTarih,TeslimSaat,ZimmetVerenID,ZimmetAlanID,UstVarlikID,YeniKisimID,Aciklama,Silindi) values (@TransferNo,@TeslimTarih,@TeslimSaat,@ZimmetVerenID,@ZimmetAlanID,@UstVarlikID,@YeniKisimID,@Aciklama,@Silindi)", zimmettransfer);
        }

        public int Update(ZimmetTransfer zimmettransfer)
        {
            return UpdateQuery("update ZimmetTransfer set TransferNo=@TransferNo,TeslimTarih=@TeslimTarih,TeslimSaat=@TeslimSaat,ZimmetVerenID=@ZimmetVerenID,ZimmetAlanID=@ZimmetAlanID,UstVarlikID=@UstVarlikID,YeniKisimID=@YeniKisimID,Aciklama=@Aciklama,Silindi=@Silindi where ZimmetTransferID=@ZimmetTransferID", zimmettransfer);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ZimmetTransfer where ZimmetTransferID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ZimmetTransfer set Silindi = 1 where ZimmetTransferID=@Id", new { Id });
        }

        public List<ZimmetTransfer> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM ZimmetTransfer where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ZimmetTransfer {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<ZimmetTransferDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<ZimmetTransferDto>().GetListDtoQuery($@"SELECT * FROM View_ZimmetTransferDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_ZimmetTransferDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}