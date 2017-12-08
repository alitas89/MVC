using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using PostSharp.Aspects;

namespace Core.Aspects.Postsharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceCounterAspect : OnMethodBoundaryAspect
    {
        private int _interval;
        [NonSerialized]
        private Stopwatch _stopwatch;

        private LoggerService _loggerService;

        //Metodun çalışma süresi örneğin 5snyden fazlaysa raporla
        public PerformanceCounterAspect(int interval = 5)
        {
            _interval = interval;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>();
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopwatch.Start();
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _stopwatch.Stop();
            //Eğer belirttiğimiz sürenin üzerinde bir sonuca sahipse bunu belirtmek gerekir
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                PerformanceLog(args);
                //Debug.WriteLine($"Performance: {args.Method.DeclaringType.FullName}.{args.Method.Name} ->> {_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
            base.OnExit(args);
        }

        /// <summary>
        /// Performans logları warning olarak düşer
        /// </summary>
        /// <param name="args"></param>
        public void PerformanceLog(MethodExecutionArgs args)
        {
            _loggerService = (LoggerService)Activator.CreateInstance(typeof(DatabaseLogger));

            if (!_loggerService.IsWarnEnabled)
            {
                return;
            }

            try
            {
                var logParameters = args.Method.GetParameters().Select((t, i) => new LogParameter
                {
                    Name = t.Name,
                    Type = t.ParameterType.Name,
                    Value = args.Arguments.GetArgument(i)
                }).ToList();

                var logDetail = new LogDetail
                {
                    FullName = $"ElapsedTime --> {_stopwatch.Elapsed.TotalSeconds}",
                    MethodName = $"{args.Method.DeclaringType.FullName}.{ args.Method.Name}",
                    Parameters = logParameters
                };

                _loggerService.Warn(logDetail);
            }
            catch (Exception)
            {

            }
        }
    }
}
