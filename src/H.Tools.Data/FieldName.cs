using System;

namespace H.Tools.Data;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class FieldName(string name) : Attribute
{
    public string Name => name;
}
