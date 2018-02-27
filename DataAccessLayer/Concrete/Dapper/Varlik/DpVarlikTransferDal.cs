using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikTransferDal : DpEntityRepositoryBase<VarlikTransfer>, IVarlikTransferDal
    {
        public List<VarlikTransfer> GetList()
        {
            return GetListQuery("select * from VarlikTransfer where Silindi=0", new { });
        }

        public VarlikTransfer Get(int Id)
        {
            return GetQuery("select * from VarlikTransfer where VarlikTransferID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikTransfer varliktransfer)
        {
            return AddQuery("insert into VarlikTransfer(TransferNo,VarlikID,MevcutKisimID,MevcutSahipVarlikID,YeniSahipVarlikID,YeniKisimID,IslemiYapanID,Tarih,Saat,Aciklama,Silindi) values (@TransferNo,@VarlikID,@MevcutKisimID,@MevcutSahipVarlikID,@YeniSahipVarlikID,@YeniKisimID,@IslemiYapanID,@Tarih,@Saat,@Aciklama,@Silindi)", varliktransfer);
        }

        public int Update(VarlikTransfer varliktransfer)
        {
            return UpdateQuery("update VarlikTransfer set TransferNo=@TransferNo,VarlikID=@VarlikID,MevcutKisimID=@MevcutKisimID,MevcutSahipVarlikID=@MevcutSahipVarlikID,YeniSahipVarlikID=@YeniSahipVarlikID,YeniKisimID=@YeniKisimID,IslemiYapanID=@IslemiYapanID,Tarih=@Tarih,Saat=@Saat,Aciklama=@Aciklama,Silindi=@Silindi where VarlikTransferID=@VarlikTransferID", varliktransfer);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikTransfer where VarlikTransferID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikTransfer set Silindi = 1 where VarlikTransferID=@Id", new { Id });
        }

        public List<VarlikTransfer> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM VarlikTransfer where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikTransfer {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VarlikTransferDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikTransferDto>().GetListDtoQuery($@"SELECT * FROM View_VarlikTransferDto where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_BakimArizaKoduDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}