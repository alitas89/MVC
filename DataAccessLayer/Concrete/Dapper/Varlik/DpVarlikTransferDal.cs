using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
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
            return AddQuery("insert into VarlikTransfer(TransferNo,VarlikID,EskiKisimID,EskiSahipVarlikID,YeniSahipVarlikID,YeniKisimID,IslemiYapanID,Tarih,Saat,Aciklama,Silindi) values (@TransferNo,@VarlikID,@EskiKisimID,@EskiSahipVarlikID,@YeniSahipVarlikID,@YeniKisimID,@IslemiYapanID,@Tarih,@Saat,@Aciklama,@Silindi)", varliktransfer);
        }

        public int Update(VarlikTransfer varliktransfer)
        {
            return UpdateQuery("update VarlikTransfer set TransferNo=@TransferNo,VarlikID=@VarlikID,EskiKisimID=@EskiKisimID,EskiSahipVarlikID=@EskiSahipVarlikID,YeniSahipVarlikID=@YeniSahipVarlikID,YeniKisimID=@YeniKisimID,IslemiYapanID=@IslemiYapanID,Tarih=@Tarih,Saat=@Saat,Aciklama=@Aciklama,Silindi=@Silindi where VarlikTransferID=@VarlikTransferID", varliktransfer);
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

            return GetListQuery($@"SELECT {columnsQuery} FROM VarlikTransfer where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikTransfer where Silindi=0 {filter}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VarlikTransferDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikTransferDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikTransferDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikTransferDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int UpdateVarlikKisimBagliVarlikKod(int VarlikID, int KisimID, int? BagliVarlikKod=null)
        {
            if (BagliVarlikKod==null)
            {
                return UpdateQuery("update Varlik set KisimID=@KisimID where VarlikID=@VarlikID", new { KisimID, VarlikID });
            }
            return UpdateQuery("update Varlik set KisimID=@KisimID, BagliVarlikKod=@BagliVarlikKod where VarlikID=@VarlikID", new { KisimID, BagliVarlikKod, VarlikID });
        }
    }
}
