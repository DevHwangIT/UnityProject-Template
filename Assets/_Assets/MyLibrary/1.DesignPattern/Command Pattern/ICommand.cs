using System.Collections;

namespace MyLibrary.DesignPattern
{
    public interface ICommand
    {
        IEnumerator Undo();
        IEnumerator Execute();
    }
}