namespace Dboard
{

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class AutowiredAttribute : Attribute
    {
    }
}
