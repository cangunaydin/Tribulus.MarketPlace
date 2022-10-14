using System;

namespace Tribulus.Composition;

[AttributeUsage(AttributeTargets.Method )]
public class ViewModelPropertyAttribute : Attribute
{
    private string _propertyName;

    public ViewModelPropertyAttribute(string propertyName)
    {
        _propertyName = propertyName;
    }

    public string PropertyName { get => _propertyName; }

    public static bool IsDefinedOrInherited<T>()
    {
        return IsDefinedOrInherited(typeof(T));
    }

    public static bool IsDefinedOrInherited(Type type)
    {
        if (type.IsDefined(typeof(ViewModelPropertyAttribute), true))
        {
            return true;
        }

        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface.IsDefined(typeof(ViewModelPropertyAttribute), true))
            {
                return true;
            }
        }

        return false;
    }
}

