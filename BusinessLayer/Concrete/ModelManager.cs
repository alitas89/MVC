using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ModelManager : IModelService
    {
        IModelDal _modelDal;

        public ModelManager(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        public List<Model> GetList()
        {
            return _modelDal.GetList();
        }
        public Model GetById(int Id)
        {
            return _modelDal.Get(Id);
        }
        public int Add(Model model)
        {
            return _modelDal.Add(model);
        }
        public int Update(Model model)
        {
            return _modelDal.Update(model);
        }
        public int Delete(int Id)
        {
            return _modelDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _modelDal.DeleteSoft(Id);
        }
    }
}
