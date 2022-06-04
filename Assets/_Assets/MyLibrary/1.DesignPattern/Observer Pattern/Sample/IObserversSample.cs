
namespace MyLibrary.DesignPattern.Sample
{
    public interface IHpObserver : IObserver { void HpChangeNotify(int hp); }

    public interface IMpObserver : IObserver { void MpChangeNotify(int mp); }
    public interface IStaminaObserver : IObserver { void StaminaChangeNotify(int stamina); }
}
