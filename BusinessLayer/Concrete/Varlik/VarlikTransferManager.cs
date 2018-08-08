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
    public class VarlikTransferManager : IVarlikTransferService
    {
        IVarlikTransferDal _varliktransferDal;

        public VarlikTransferManager(IVarlikTransferDal varliktransferDal)
        {
            _varliktransferDal = varliktransferDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTransferRead, VarlikTransferLtd")]
        public List<VarlikTransfer> GetList()
        {
            return _varliktransferDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTransferRead, VarlikTransferLtd")]
        public VarlikTransfer GetById(int Id)
        {
            return _varliktransferDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikTransferCreate")]
        public int Add(VarlikTransfer varliktransfer)
        {
            //Varlık kısım ve bağlıVarlıkKod güncellenmelidir.
            int updateResult = _varliktransferDal.UpdateVarlikKisimBagliVarlikKod(varliktransfer.VarlikID, varliktransfer.YeniKisimID, varliktransfer.YeniSahipVarlikID);
            //Ekleme yapılır
            return updateResult > 0 ? _varliktransferDal.Add(varliktransfer) : 0;
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikTransferUpdate")]
        public int Update(VarlikTransfer varliktransfer)
        {
            //Varlık kısım ve bağlıVarlıkKod güncellenmelidir.
            int updateResult = _varliktransferDal.UpdateVarlikKisimBagliVarlikKod(varliktransfer.VarlikID, varliktransfer.YeniKisimID, varliktransfer.YeniSahipVarlikID);
            //Ekleme yapılır
            return updateResult > 0 ? _varliktransferDal.Update(varliktransfer) : 0;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikTransferDelete")]
        public int Delete(int Id)
        {
            return _varliktransferDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikTransferDelete")]
        public int DeleteSoft(int Id)
        {
            return _varliktransferDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTransferRead, VarlikTransferLtd")]
        public List<VarlikTransfer> GetListPagination(PagingParams pagingParams)
        {
            return _varliktransferDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varliktransferDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTransferRead, VarlikTransferLtd")]
        public List<VarlikTransferDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varliktransferDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _varliktransferDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<VarlikTransfer> listVarlikTransfer)
        {
            return _varliktransferDal.AddListWithTransactionBySablon(listVarlikTransfer);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<VarlikTransfer> ExcelDataProcess(DataTable dataTable)
        {
            List<VarlikTransfer> listVarlikTransfer = new List<VarlikTransfer>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlikTransfer.Add(new VarlikTransfer()
                {
                    TransferNo = row[0] .ToString() != "" ? Convert.ToInt32(row[0].ToString()) : 0,
                    VarlikID = row[1] .ToString() != "" ? Convert.ToInt32(row[1].ToString()) : 0,
                    EskiKisimID = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    EskiSahipVarlikID = row[3] .ToString() != "" ? Convert.ToInt32(row[3].ToString()) : 0,
                    YeniSahipVarlikID = row[4] .ToString() != "" ? Convert.ToInt32(row[4].ToString()) : 0,
                    YeniKisimID = row[5] .ToString() != "" ? Convert.ToInt32(row[5].ToString()) : 0,
                    IslemiYapanID = row[6] .ToString() != "" ? Convert.ToInt32(row[6].ToString()) : 0,
                    Tarih = row[7] .ToString() != "" ? Convert.ToDateTime(row[7].ToString()) : DateTime.MaxValue,
                    Saat = row[8].ToString(),
                    Aciklama = row[9].ToString(),
                });
            }

            return listVarlikTransfer;
        }
    }
}