using System;

namespace SmokeyLib
{
    public abstract class EventBase
    {
        internal abstract Type EventType { get; }
        internal abstract void Run(object callback);
    }

    public class Event<TEvent> : EventBase, IDisposable where TEvent : class, IEventMsg
    {
        private EventManager _eventManager;
        internal override Type EventType => typeof(TEvent);
        public Action<TEvent> OnRun;

        public Event(Action<TEvent> eventFunction, EventManager eventManager)
        {
            OnRun = eventFunction;
            AttachTo(eventManager);
        }

        protected void AttachTo(EventManager eventManager)
        {
            if(eventManager == null)
                return;

            _eventManager = eventManager;
            _eventManager.Register(this);
        }

        internal override void Run(object callback)
        {
            if (callback is TEvent callbackObj)
                OnRun?.Invoke(callbackObj);
        }

        ~Event()
        {
            Dispose();
        }

        public void Dispose()
        {
            _eventManager?.UnRegister(this);
            GC.SuppressFinalize(this);
        }
    }
}