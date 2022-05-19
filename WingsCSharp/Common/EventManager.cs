/*
 * FileName:    EventManager
 * Author:      Wings
 * CreateTime:  2021_12_03
 * 
*/

using System;
using System.Collections;
using System.Collections.Generic;

using  EventCallBack = System.Action<object>;

namespace GenOcean.Common
{

    public interface IRegsiterEvents
    {
        bool IsRegister { get; set; }
        Dictionary<int,int> EventIDs { get; set; }
        void RegsiterEvents();
        void UnregsiterEvents();
        void RegsiterEvent(int eid, EventCallBack cb, bool isSwallow = false, int invokeCount = 1);
    }

    public class BaseEvent
    {
        public int UID = 0;
        public int EventID = 0;
        public int InvokeCount = 1;
        public bool IsSwallow = false;
        public EventCallBack Callback = null;
    }

    public class EventListener
    {
        public int EventID = 0;
        public List<BaseEvent> Events = new List<BaseEvent>();
    }

    public class EventManager:ManagerBase
    {
        #region Protected Fields

        protected Dictionary<int, EventListener> _EventListeners = new Dictionary<int, EventListener>();

        protected object _LockObject = new object();

        #endregion --Protected Fields

        #region Public Fields

        #endregion --Public Fields

        #region Private Fields
        #endregion --Private Fields

        #region Public Methods
        public virtual int RegisterEventCallback(int eid, EventCallBack cb, bool isSwallow = false, int invokeCount = 1)
        {
            BaseEvent newEvent = new BaseEvent();
            newEvent.EventID = eid;
            newEvent.Callback = cb;
            newEvent.IsSwallow = false;
            newEvent.InvokeCount = invokeCount;
            newEvent.UID = newEvent.GetHashCode();

            lock (_LockObject)
            {
                EventListener el = null;
                if (!_EventListeners.TryGetValue(newEvent.EventID, out el))
                {
                    el = new EventListener();
                    el.EventID = newEvent.EventID;
                    _EventListeners.Add(newEvent.EventID, el);
                }
                el.Events.Add(newEvent);

                return newEvent.UID;
            }
        }
        public virtual bool ReleaseEventCallback(int uid, int eid)
        {
            lock (_LockObject)
            {
                EventListener el = null;
                if (_EventListeners.TryGetValue(eid, out el))
                {
                    try
                    {
                        BaseEvent bEvent = el.Events.Find(newEvent => newEvent.UID == uid);
                        el.Events.Remove(bEvent);

                        return true;
                    }
                    catch (Exception err)
                    {
                        SingleLoggerManager.LogInfo($"Can not get {eid} with UID={uid}:{err.Message}");
                        return false;
                    }
                }

                return false;
            }
        }
        public virtual void DispatchEvent(int eid, object edata)
        {
            lock (_LockObject)
            {
                EventListener el = null;
                if (_EventListeners.TryGetValue(eid, out el))
                {
                    int index = el.Events.Count;
                    index--;
                    while (index >= 0)
                    {
                        BaseEvent e = el.Events[index];
                        try
                        {
                            if (e.InvokeCount > 0 || e.InvokeCount < 0)
                            {
                                if (e.InvokeCount > 0)
                                {
                                    e.InvokeCount--;
                                }

                                if (e.Callback != null && e.Callback.Target != null)
                                {
                                    e.Callback(edata);
                                }

                                if (e.InvokeCount == 0)
                                {
                                    el.Events.RemoveAt(index);
                                }
                                index--;
                                if (e.IsSwallow)
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            el.Events.RemoveAt(index);
                            SingleLoggerManager.LogInfo($"{GetType().Name}.InstanceDispatchEvent:{err.Message}");
                        }
                    }
                }
            }
        }
        public virtual void ClearNullEvent()
        {
            lock (_LockObject)
            {
                foreach (var el in _EventListeners.Values)
                {
                    int index = el.Events.Count;
                    index--;
                    while (index >= 0)
                    {
                        try
                        {
                            BaseEvent e = el.Events[index];
                            if (e == null || e.Callback.Target == null)
                            {
                                el.Events.RemoveAt(index);
                            }
                        }
                        catch (Exception err)
                        {
                            el.Events.RemoveAt(index);
                            SingleLoggerManager.LogInfo($"{GetType().Name}.InstanceClearNullEvent:{err.Message}");
                        }

                        index--;
                    }
                }

            }
        }

        #endregion --Public Methods

        #region Private Methods
        #endregion --Public Methods

        #region Protected Methods

        #endregion --Protected Methods
    }

    /// <summary>
    /// 时间管理器单例
    /// </summary>
    public class SingleEventManager : SingletonManagerBase<EventManager>
    {

        /// <summary>
        /// 注册一个监听事件回调
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="cb"></param>
        /// <param name="isSwallow"></param>
        /// <param name="invokeCount">默认只触发一次，设置为 -1 时会无限触发</param>
        public static int RegisterEventCallback(int eid, EventCallBack cb, bool isSwallow = false, int invokeCount = 1)
        {
            return Instance.RegisterEventCallback(eid, cb, isSwallow, invokeCount);
        }

        /// <summary>
        /// 分发一个指定事件
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="edata"></param>
        public static void DispatchEvent(int eid, object edata = null)
        {
            Instance.DispatchEvent(eid, edata);
        }

        /// <summary>
        /// 清除部分有问题的回调
        /// </summary>
        public static void ClearNullEvent()
        {
            Instance.ClearNullEvent();
        }

        /// <summary>
        /// 取消一个事件监听
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="eid"></param>
        public static void ReleaseEventCallback(int uid, int eid)
        {
            Instance.ReleaseEventCallback(uid, eid);
        }

    }
}
