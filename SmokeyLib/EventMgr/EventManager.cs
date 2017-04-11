using System;
using System.Collections.Generic;

namespace SmokeyLib
{
    public class EventManager
    {
        private List<EventBase> _registeredCallbacks;

        public EventManager()
        {
            _registeredCallbacks = new List<EventBase>();
        }

        public Event<TEvent> Subscribe<TEvent>(Action<TEvent> eventFunction) where TEvent : class, IEventMsg
        {
            return new Event<TEvent>(eventFunction, this);
        }

        public void Subscribe<T>()
        {
            throw new NotImplementedException();
        }

        public void UnSubscribe(EventBase evnt)
        {
            UnRegister(evnt);
        }

        internal void Register(EventBase evnt)
        {
            if (_registeredCallbacks.Contains(evnt))
                return;

            _registeredCallbacks.Add(evnt);
        }

        internal void UnRegister(EventBase evnt)
        {
            _registeredCallbacks.Remove(evnt);
        }

        internal void Handle(IEventMsg evntMsg)
        {
            _registeredCallbacks
                .FindAll(evnt => evnt.EventType.IsInstanceOfType(evntMsg))
                .ForEach(evnt => evnt.Run(evntMsg));
        }
    }
}