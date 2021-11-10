namespace Enrollment.XPlatform.ViewModels.Validatables
{
    public interface IHasItemsSourceValidatable : IValidatable
    {
        void Reload(object entity);
    }
}
