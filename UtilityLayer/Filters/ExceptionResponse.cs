using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLayer.Filters
{
    public class ExceptionResponse
    {
        public string ErrorMessage { get; set; }
        public string ErrorAction { get; set; }
        public string ErrorController { get; set; }
    }
}
