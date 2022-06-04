using System;
using System.Collections;
using System.Linq;
using System.Reflection;

public abstract class Enumeration : IComparable
{
    public string Name { get; internal set; }
    public int Priority { get; internal set; }
    public override string ToString() => Name;
    protected Enumeration(int priority, string name) => (Priority, Name) = (priority, name);
    public int CompareTo(object other) => Priority.CompareTo(((Enumeration)other).Priority);
}

public partial class UIWindowID : Enumeration
{
    public UIWindowID(int priority, string name) : base(priority, name) { }
    public static UIWindowID[] GetAll
    {
        get
        {
            return typeof(UIWindowID).GetFields(BindingFlags.Public |
                                                BindingFlags.Static |
                                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<UIWindowID>()
                .ToArray();
        }
    }

    //Index is Draw Order
    public static UIWindowID None = new UIWindowID(0, "None");
    public static UIWindowID GameMenu = new UIWindowID(1, "GameMenu");
}