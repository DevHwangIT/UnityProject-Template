using System;

namespace MyLibrary.Utility
{
    public class ObservableValue<T>
    {
        private event Action OnChangeCallback;
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                this._value = value;
                if (OnChangeCallback != null)
                    OnChangeCallback.Invoke();
            }
        }

        public ObservableValue()
        {
        }

        public ObservableValue(T value)
        {
            _value = value;
        }

        public void Subscribe(Action callback)
        {
            OnChangeCallback += callback;
        }

        public void UnSubscribe(Action callback)
        {
            OnChangeCallback -= callback;
        }
    }
}