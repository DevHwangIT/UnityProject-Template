using System.Collections.Generic;

namespace MyLibrary.DesignPattern
{
    public class ObserverSubject
    {
        protected List<IObserver> Observers = new List<IObserver>();
        
        public virtual void Subscribe(IObserver observer)
        {
            Observers.Add(observer);
        }

        public virtual void UnSubscribe(IObserver observer)
        {
            Observers.Remove(observer);
        }
        
        public virtual void Notify()
        {
            foreach (var observer in Observers)
            {
                observer.Notify();
            }
        }
    }
}
