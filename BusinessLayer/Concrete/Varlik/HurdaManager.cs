using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class HurdaManager : IHurdaService
    {
        IHurdaDal _hurdaDal;

        public HurdaManager(IHurdaDal hurdaDal)
        {
            _hurdaDal = hurdaDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, HurdaRead, HurdaLtd")]
        public List<Hurda> GetList()
        {
            return _hurdaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, HurdaRead, HurdaLtd")]
        public Hurda GetById(int Id)
        {
            return _hurdaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, HurdaCreate")]
        public int Add(Hurda hurda)
        {
            return _hurdaDal.Add(hurda);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, HurdaUpdate")]
        public int Update(Hurda hurda)
        {
            return _hurdaDal.Update(hurda);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, HurdaDelete")]
        public int Delete(int Id)
        {
            return _hurdaDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, HurdaDelete")]
        public int DeleteSoft(int Id)
        {
            return _hurdaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, HurdaRead, HurdaLtd")]
        public List<Hurda> GetListPagination(PagingParams pagingParams)
        {
            return _hurdaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _hurdaDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Hurda> listHurda)
        {
            return _hurdaDal.AddListWithTransactionBySablon(listHurda);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Hurda> ExcelDataProcess(DataTable dataTable)
        {
            List<Hurda> listHurda = new List<Hurda>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listHurda.Add(new Hurda()
                {
                    BarkodKod = row[0].ToString(),
                    VarlikID = row[1] != DBNull.Value ? Convert.ToInt32(row[1].ToString()) : 0,
                    OzurKod = row[2].ToString(),
                    OzurAd = row[3].ToString(),
                    OzurTip = row[4].ToString(),
                    Tarih = row[5] != DBNull.Value ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    Miktar = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    Toplam = row[7] != DBNull.Value ? Convert.ToInt32(row[7].ToString()) : 0,
                    Aciklama = row[8].ToString(),
                });
            }

            return listHurda;
        }
    }
}
