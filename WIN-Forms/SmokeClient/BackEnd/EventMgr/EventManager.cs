using System;
using System.Collections.Generic;

namespace SomkeClient
{
    class EventManager
    {
        private Account _account;
        private List<EventBase> _registeredCallbacks;

        public EventManager(Account account)
        {
            _registeredCallbacks = new List<EventBase>();
            _account = account;
        }

        public Event<TEvent> Subscribe<TEvent>(Action<TEvent> eventFunction) where TEvent : class, IEventMsg
        {
            return new Event<TEvent>(eventFunction, this);
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

        void Handle(IEventMsg evntMsg)
        {
            _registeredCallbacks
                .FindAll(evnt => evnt.EventType.IsInstanceOfType(evntMsg))
                .ForEach(evnt => evnt.Run(evntMsg));
        }
    }
}
