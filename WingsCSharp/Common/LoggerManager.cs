using System.Collections;
using System.Collections.Generic;
using System;

namespace GenOcean.Common
{
    /// <summary>
    /// 日志管理类
    /// </summary>
    public class LoggerManager: ManagerBase
    {
        public Action<string> LogActionCallback = null;

        /// <summary>
        /// 处理最终日志打印的逻辑
        /// </summary>
        /// <param name="info"></param>
        public virtual void LogInfo(string info)
        {
            try
            {
                if (LogActionCallback != null && LogActionCallback.Target != null)
                {
                    LogActionCallback(info);
                }
            }
            catch (Exception err)
            {
                Console.WriteLine($"{GetType().Name}.LogInfoInstance:{err.Message}");
                LogActionCallback = null;
            }
        }

        public virtual void RegisterCallback(Action<string> cb, bool isReplaceAll = false)
        {
            if (isReplaceAll)
            {
                LogActionCallback = cb;
            }
            else
            {
                LogActionCallback += cb;
            }
        }
    }

    /// <summary>
    /// 日志管理
    /// </summary>
    public class SingleLoggerManager<T>:SingletonManagerBase<T> where T: LoggerManager,new() 
    {
        /// <summary>
        /// 注册一个日志回调
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="isReplaceAll"></param>
        public static void RegisterCallback(Action<string> cb,bool isReplaceAll = false)
        {
            Instance.RegisterCallback(cb, isReplaceAll);
        }

        /// <summary>
        /// 输出普通日志
        /// </summary>
        /// <param name="info"></param>
        public static void LogInfo(string info)
        {
            Instance.LogInfo(info);
        }

        /// <summary>
        /// 输出普通日志
        /// </summary>
        /// <param name="info"></param>
        public static void LogError(string info)
        {
            Instance.LogInfo(info);
        }
    }


    public class SingleBaseLogger : SingleLoggerManager<LoggerManager>
    {
    }

}
