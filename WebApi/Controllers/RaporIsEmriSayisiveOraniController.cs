using BusinessLayer.Abstract.Bakim;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class RaporIsEmriSayisiveOraniController : ApiController
    {

        IRaporIsEmriSayisiveOraniService _raporisemrisayisiveoraniService;

        public RaporIsEmriSayisiveOraniController(IRaporIsEmriSayisiveOraniService raporisemrisayisiveoraniService)
        {
            _raporisemrisayisiveoraniService = raporisemrisayisiveoraniService;
        }

        // GET api/<controller>
        public IEnumerable<RaporIsEmriSayisiveOraniTemp> Get()
        {
            return _raporisemrisayisiveoraniService.GetList();
        }
    }
}