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
    public class MarkaManager : IMarkaService
    {
        IMarkaDal _markaDal;

        public MarkaManager(IMarkaDal markaDal)
        {
            _markaDal = markaDal;
        }

        public List<Marka> GetList()
        {
            return _markaDal.GetList();
        }
        public Marka GetById(int Id)
        {
            return _markaDal.Get(Id);
        }
        public int Add(Marka marka)
        {
            return _markaDal.Add(marka);
        }
        public int Update(Marka marka)
        {
            return _markaDal.Update(marka);
        }
        public int Delete(int Id)
        {
            return _markaDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _markaDal.DeleteSoft(Id);
        }
    }
}
