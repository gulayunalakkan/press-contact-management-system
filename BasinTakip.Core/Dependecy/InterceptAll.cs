using BasinTakip.Core.Data;
using BasinTakip.Core.Entities.Base;
using BasinTakip.Entities.Base;
using BasinTakip.Logging;
using Castle.DynamicProxy;
using JohnsonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasinTakip.Core.Dependecy
{
    public class InterceptAll : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            AddDataHistory(invocation);

            if (!HandleError(invocation))
            {
                invocation.Proceed();
            }
        }

        private static bool HandleError(IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();
            var handleErrorAttribute = method.GetCustomAttributes(true)
                .FirstOrDefault(p => p.GetType() == typeof(HandleErrorAttribute)) as HandleErrorAttribute;
            var hasAttribute = handleErrorAttribute != null;

            if (hasAttribute)
            {
                try
                {
                    invocation.Proceed();
                }
                catch (Exception ex)
                {
                    var parametersAsDictionary = GetMethodParameters(invocation);
                    var parametersAsSerialized = JohnsonManager.Json.Serialize(parametersAsDictionary);

                    LogManager.Current.Error(ex, parametersAsSerialized);
                }
            }

            return hasAttribute;
        }

        private static bool AddDataHistory(IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();
            var dataHistoryAttribute = method.GetCustomAttributes(true)
                .FirstOrDefault(p => p.GetType() == typeof(DataHistoryAttribute)) as DataHistoryAttribute;
            var hasAttribute = dataHistoryAttribute != null;

            if (hasAttribute)
            {
                var entity = invocation.Arguments[0];

                using (IocManager.BeginScope())
                {
                    var repository = IocManager.Resolve<IDataHistoryRepository>();
                    var history = new DataHistory();

                    history.EntityType = entity.GetType().Name;
                    history.EntityId = JohnsonManager.Reflection.GetPropertyValue(entity, "Id").ToString();
                    history.Data = JohnsonManager.Json.Serialize(entity);
                    history.CreatedAt = DateTime.Now;

                    if (HttpContext.Current != null)
                    {
                        history.CreatedMemberId = HttpContext.Current.User?.Identity?.Name;
                        history.UserAgent = HttpContext.Current.Request.UserAgent;
                        history.UserHostAddress = HttpContext.Current.Request.UserHostAddress;
                    }

                    repository.Save(history);
                }
            }

            return hasAttribute;
        }

        private static Dictionary<string, object> GetMethodParameters(IInvocation invocation)
        {
            var method = invocation.GetConcreteMethod();
            var parametersAsDictionary = new Dictionary<string, object>();
            int index = 0;
            foreach (var item in method.GetParameters())
            {
                parametersAsDictionary.Add(item.Name, invocation.Arguments[index]);
                index++;
            }

            return parametersAsDictionary;
        }
    }
}
