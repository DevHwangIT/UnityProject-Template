
namespace MyLibrary.DesignPattern.Sample
{
    public class ObserverControllerSample : Singleton<ObserverControllerSample>
    {
        private ObserverSubject[] _observerSubjectList = {new HpObserverSubject(), new MpObserverSubject(), new StaminaObserverSubject()};

        public T GetObserverSubject<T>() where T : ObserverSubject
        {
            foreach (var observerSubject in _observerSubjectList)
            {
                if (observerSubject.GetType() == typeof(T))
                    return (T) observerSubject;
            }
            return null;
        }
    }
}
