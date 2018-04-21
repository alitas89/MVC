using System.Collections.Generic;
using System.Linq;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using MoreLinq;
using Newtonsoft.Json;

namespace BusinessLayer.Concrete.Sistem
{
    public class MenuManager : IMenuService
    {
        IMenuDal _menuDal;

        public MenuManager(IMenuDal menuDal)
        {
            _menuDal = menuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public List<Menu> GetList()
        {
            return _menuDal.GetList();
        }

        [SecuredOperation(Roles = "Authorized")]
        public Menu GetById(int Id)
        {
            return _menuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public int Add(Menu menu)
        {
            return _menuDal.Add(menu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public int Update(Menu menu)
        {
            return _menuDal.Update(menu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public int Delete(int Id)
        {
            return _menuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public int DeleteSoft(int Id)
        {
            return _menuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Authorized")]
        public List<Menu> GetListPagination(PagingParams pagingParams)
        {
            return _menuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _menuDal.GetCount(filter);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public string GetMenuByKod(string arrKodJson)
        {
            string[] arrKod = JsonConvert.DeserializeObject<string[]>(arrKodJson);
            return FindMenu(arrKod);
        }

        public string GetMenuByRoles(List<string> listRoles)
        {
            string[] arrKod = listRoles.ToArray();
            return FindMenu(arrKod);
        }

        List<Menu> listFindMenu = new List<Menu>();
        public string FindMenu(string[] arrKod)
        {
            List<Menu> listMenu = _menuDal.GetList();

            foreach (var kod in arrKod)
            {
                if (kod == "Authorized" || kod == "Admin")
                {
                    continue;
                }

                //her birinin parentIdsi bulunur ve çağırılır
                var itemMenu = listMenu.FirstOrDefault(x => x.Kod == kod);

                if (itemMenu != null)
                {
                    //eklenir
                    RecursiveParentMenu(itemMenu);
                    RecursiveChildrenMenu(itemMenu);
                }
            }

            //Tüm menüler bulunmuştur.
            return JsonConvert.SerializeObject(listFindMenu.DistinctBy(a => a.MenuID).OrderBy(b => b.ParentID).ToList());
        }

        /// <summary>
        /// Parent menüler bulunup eklenir. Sürekli parentları bulup eklemeye yönelik çalışma
        /// </summary>
        /// <param name="item"></param>
        public void RecursiveParentMenu(Menu item)
        {
            listFindMenu.Add(item);

            if (item != null && item.ParentID != 0)
            {
                var parentItemMenu = _menuDal.Get(item.ParentID);
                RecursiveParentMenu(parentItemMenu);
            }
        }

        public void RecursiveChildrenMenu(Menu item)
        {
            List<Menu> listMenu = _menuDal.GetList();
            if (item != null)
            {
                //Parent'ı gelen item'e ait olan tüm childlar bulunur.
                var listChildMenu = listMenu.Where(x => x.ParentID == item.MenuID).ToList();
                foreach (var childMenu in listChildMenu)
                {
                    listFindMenu.Add(childMenu);
                    RecursiveChildrenMenu(childMenu);
                }
            }
        }
    }
}