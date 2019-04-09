using System;

namespace NullGuard
{
    /// <summary>
    /// Prevents the injection of null checking.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.Property)]
    public class AllowNullAttribute : Attribute
    {
    }
}