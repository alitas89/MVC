using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using System.Data;

namespace BusinessLayer.Concrete.Personel
{
    public class MesaiManager : IMesaiService
    {
        IMesaiDal _mesaiDal;

        public MesaiManager(IMesaiDal mesaiDal)
        {
            _mesaiDal = mesaiDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, MesaiRead, MesaiLtd")]
        public List<Mesai> GetList()
        {
            return _mesaiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, MesaiRead, MesaiLtd")]
        public Mesai GetById(int Id)
        {
            return _mesaiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, MesaiCreate")]
        public int Add(Mesai mesai)
        {
            return _mesaiDal.Add(mesai);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, MesaiUpdate")]
        public int Update(Mesai mesai)
        {
            return _mesaiDal.Update(mesai);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, MesaiDelete")]
        public int Delete(int Id)
        {
            return _mesaiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, MesaiDelete")]
        public int DeleteSoft(int Id)
        {
            return _mesaiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, MesaiRead, MesaiLtd")]
        public List<Mesai> GetListPagination(PagingParams pagingParams)
        {
            return _mesaiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _mesaiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<MesaiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _mesaiDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _mesaiDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Mesai> listMesai)
        {
            return _mesaiDal.AddListWithTransactionBySablon(listMesai);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Mesai> ExcelDataProcess(DataTable dataTable)
        {
            List<Mesai> listMesai = new List<Mesai>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMesai.Add(new Mesai()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    UcretCarpani = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    MesaiTuruID = row[3] .ToString() != "" ? Convert.ToInt32(row[3].ToString()) : 0,
                });
            }


            return listMesai;
        }

    }
}
