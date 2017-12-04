﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;

namespace Core.Aspects.Postsharp.CacheAspects
{
    [Serializable]
    public class CacheRemoveAspect : OnMethodBoundaryAspect
    {
        private string _pattern;
        private Type _cacheType;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(Type cacheType)
        {
            _cacheType = cacheType;
        }

        public CacheRemoveAspect(string pattern, Type cacheType)
        {
            _pattern = pattern;
            _cacheType = cacheType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (typeof(ICacheManager).IsAssignableFrom(_cacheType) == false)
            {
                throw new Exception("Wrong Cache Manager");
            }

            //Memory cachemanager örneği oluştur
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }

        //Birşey eklendiğinde, silindiğinde veya güncellendiğinde cache'i silmek gerekir.
        //Bu yapı namespace altındaki ilgili methodla bağlantılı olan cacheleri siler.
        public override void OnSuccess(MethodExecutionArgs args)
        {
            //pattern yoksa tamamı silinmelidir (*)
            _cacheManager.RemoveByPattern(string.IsNullOrEmpty(_pattern) ? string.Format($@"{args.Method.ReflectedType.Namespace}.{args.Method.ReflectedType.Name}.*") : _pattern);
        }
    }
}
