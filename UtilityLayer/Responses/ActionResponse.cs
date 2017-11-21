using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLayer.Responses
{
    public class ActionResponse
    {
        public int Count { get; set; }
        public object Data { get; set; }

        public ActionResponse(int count)
        {
            if (count==0)
            {
                //İşlem başarısız

            }
            else
            {
                //İşlem başarılı

            }
        }
    }
}
