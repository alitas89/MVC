using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class AmbarManager : IAmbarService
    {
        IAmbarDal _ambarDal;

        public AmbarManager(IAmbarDal ambarDal)
        {
            _ambarDal = ambarDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, AmbarRead, AmbarLtd")]
        public List<Ambar> GetList()
        {
            return _ambarDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, AmbarRead, AmbarLtd")]
        public Ambar GetById(int Id)
        {
            return _ambarDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, AmbarCreate")]
        public int Add(Ambar ambar)
        {
            return _ambarDal.Add(ambar);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, AmbarUpdate")]
        public int Update(Ambar ambar)
        {
            return _ambarDal.Update(ambar);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, AmbarDelete")]
        public int Delete(int Id)
        {
            return _ambarDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, AmbarDelete")]
        public int DeleteSoft(int Id)
        {
            return _ambarDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, AmbarRead, AmbarLtd")]
        public List<Ambar> GetListPagination(PagingParams pagingParams)
        {
            return _ambarDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _ambarDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, AmbarRead, AmbarLtd")]
        public List<AmbarDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _ambarDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _ambarDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Ambar> listAmbar)
        {
            return _ambarDal.AddListWithTransactionBySablon(listAmbar);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Ambar> ExcelDataProcess(DataTable dataTable)
        {
            List<Ambar> listAmbar = new List<Ambar>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listAmbar.Add(new Ambar()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    KisimID = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    Aciklama = row[3].ToString(),
                    IsEmriMalzemeFiyatKatsayi = row[4] .ToString() != "" ? Convert.ToDecimal(row[4].ToString()) : 0,
                    SanalAmbarID = row[5] .ToString() != "" ? Convert.ToInt32(row[5].ToString()) : 0,
                    HurdaAmbarID = row[6] .ToString() != "" ? Convert.ToInt32(row[6].ToString()) : 0,
                    SanalAmbar = row[7] .ToString() != "" ? Convert.ToBoolean(row[7].ToString()) : false,
                    VarsayilanDeger = row[8] .ToString() != "" ? Convert.ToBoolean(row[8].ToString()) : false,
                    Semt = row[9].ToString(),
                    Sehir = row[10].ToString(),
                    Ulke = row[11].ToString(),
                    Adres = row[12].ToString(),
                });
            }


            return listAmbar;
        }

    }
}
