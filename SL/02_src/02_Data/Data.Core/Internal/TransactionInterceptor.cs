using Castle.DynamicProxy;
using SL.Data.Abstractions;
using SL.Data.Abstractions.Annotations;
using SL.Utils.Helpers;
using SL.Utils.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SL.Data.Core.Internal
{

    /// <summary>
    /// 特性事务拦截器
    /// </summary>
    public class TransactionInterceptor : IInterceptor
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionInterceptor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Intercept(IInvocation invocation)
        {

            var transactionAttribute = invocation.MethodInvocationTarget.GetCustomAttribute<TransactionAttribute>();
            if (transactionAttribute == null)
            {
                //调用业务方法
                invocation.Proceed();
            }
            else
            {
                try
                {
                    _unitOfWork.BeginTran();
                    invocation.Proceed();
                    // 异步获取异常，先执行
                    if (IsAsyncMethod(invocation.Method))
                    {
                        dynamic result = invocation.ReturnValue;
                        if (result is Task<IResultModel>)
                        {
                            //等待完成
                            Task.WaitAll(result as Task);
                            if (result.Result.Successful)
                            {
                                _unitOfWork.CommitTran();
                            }
                            else
                            {
                                _unitOfWork.RollbackTran();
                            }
                        }
                    }
                    else
                    {
                        _unitOfWork.CommitTran();
                    }
                }
                catch (Exception)
                {
                    ConsoleHelper.WriteErrorLine("Rollback Transaction");
                    _unitOfWork.RollbackTran();
                }
            }
        }


        

        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }
    }
}
