using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class ArizaNedeniGrubuManager : IArizaNedeniGrubuService
    {
        IArizaNedeniGrubuDal _arizanedenigrubuDal;

        public ArizaNedeniGrubuManager(IArizaNedeniGrubuDal arizanedenigrubuDal)
        {
            _arizanedenigrubuDal = arizanedenigrubuDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniGrubuRead, ArizaNedeniGrubuLtd")]
        public List<ArizaNedeniGrubu> GetList()
        {
            return _arizanedenigrubuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniGrubuRead, ArizaNedeniGrubuLtd")]
        public ArizaNedeniGrubu GetById(int Id)
        {
            return _arizanedenigrubuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, ArizaNedeniGrubuCreate")]
        public int Add(ArizaNedeniGrubu arizanedenigrubu)
        {
            return _arizanedenigrubuDal.Add(arizanedenigrubu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, ArizaNedeniGrubuUpdate")]
        public int Update(ArizaNedeniGrubu arizanedenigrubu)
        {
            return _arizanedenigrubuDal.Update(arizanedenigrubu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaNedeniGrubuDelete")]
        public int Delete(int Id)
        {
            return _arizanedenigrubuDal.Delete(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaNedeniGrubuDelete")]
        public int DeleteSoft(int Id)
        {
            return _arizanedenigrubuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniGrubuRead, ArizaNedeniGrubuLtd")]
        public List<ArizaNedeniGrubu> GetListPagination(PagingParams pagingParams)
        {
            return _arizanedenigrubuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _arizanedenigrubuDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<ArizaNedeniGrubu> listArizaNedeniGrubu)
        {
            return _arizanedenigrubuDal.AddListWithTransactionBySablon(listArizaNedeniGrubu);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<ArizaNedeniGrubu> ExcelDataProcess(DataTable dataTable)
        {
            List<ArizaNedeniGrubu> listArizaNedeniGrubu = new List<ArizaNedeniGrubu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listArizaNedeniGrubu.Add(new ArizaNedeniGrubu()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }



            return listArizaNedeniGrubu;
        }


    }
}