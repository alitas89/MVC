
using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class ZimmetTransferManager : IZimmetTransferService
    {
        IZimmetTransferDal _zimmettransferDal;

        public ZimmetTransferManager(IZimmetTransferDal zimmettransferDal)
        {
            _zimmettransferDal = zimmettransferDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public List<ZimmetTransfer> GetList()
        {
            return _zimmettransferDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public ZimmetTransfer GetById(int Id)
        {
            return _zimmettransferDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, ZimmetTransferCreate")]
        public int Add(ZimmetTransfer zimmettransfer)
        {
            return _zimmettransferDal.Add(zimmettransfer);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, ZimmetTransferUpdate")]
        public int Update(ZimmetTransfer zimmettransfer)
        {
            return _zimmettransferDal.Update(zimmettransfer);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, ZimmetTransferDelete")]
        public int Delete(int Id)
        {
            return _zimmettransferDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, ZimmetTransferDelete")]
        public int DeleteSoft(int Id)
        {
            return _zimmettransferDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public List<ZimmetTransfer> GetListPagination(PagingParams pagingParams)
        {
            return _zimmettransferDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _zimmettransferDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public List<ZimmetTransferDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _zimmettransferDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _zimmettransferDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<ZimmetTransfer> listZimmetTransfer)
        {
            return _zimmettransferDal.AddListWithTransactionBySablon(listZimmetTransfer);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<ZimmetTransfer> ExcelDataProcess(DataTable dataTable)
        {
            List<ZimmetTransfer> listZimmetTransfer = new List<ZimmetTransfer>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listZimmetTransfer.Add(new ZimmetTransfer()
                {
                    TransferNo = row[0].ToString(),
                    TeslimTarih = row[1] != DBNull.Value ? Convert.ToDateTime(row[1].ToString()) : DateTime.MaxValue,
                    TeslimSaat = row[2].ToString(),
                    ZimmetVerenID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    ZimmetAlanID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    UstVarlikID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    YeniKisimID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    Aciklama = row[7].ToString(),
                });
            }

            return listZimmetTransfer;
        }

    }
}
