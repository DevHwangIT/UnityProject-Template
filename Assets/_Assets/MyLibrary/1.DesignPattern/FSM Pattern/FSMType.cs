using System;

namespace MyLibrary.DesignPattern
{
    public abstract class FSMType<T>
    {
        public Action OnChanged;
        public abstract void Enter(T t);
        public abstract void Update(T t);
        public abstract void Exit(T t);
    }
}

