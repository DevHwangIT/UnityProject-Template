using System;
using UnityEngine;


[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyInEditorAttribute : PropertyAttribute
{
    public readonly bool runtimeOnly;

    public ReadOnlyInEditorAttribute(bool runtimeOnly = false)
    {
        this.runtimeOnly = runtimeOnly;
    }
}