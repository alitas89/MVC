using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Core.Utilities.Mappings
{
    public class AutoMapperHelper
    {
        public static List<T> MapToSameTypeList<T>(List<T> list)
        {
            Mapper.Initialize(c => { c.CreateMap<T, T>(); });

            List<T> result = Mapper.Map<List<T>, List<T>>(list);

            return result;
        }

        public static T MapToSameType<T>(T obj)
        {
            Mapper.Initialize(c => { c.CreateMap<T, T>(); });

            T result = Mapper.Map<T, T>(obj);
            return result;
        }

        public static List<TB> MapToDiffTypeList<TA, TB>(List<TA> list)
        {
            Mapper.Initialize(c => { c.CreateMap<TA, TB>(); });

            List<TB> result = Mapper.Map<List<TA>, List<TB>>(list);
            return result;
        }
    }
}
