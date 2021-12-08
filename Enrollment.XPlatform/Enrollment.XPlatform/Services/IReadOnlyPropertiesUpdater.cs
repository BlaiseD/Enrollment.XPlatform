using Enrollment.Forms.Configuration.EditForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IReadOnlyPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, object entity, List<FormItemSettingsDescriptor> fieldSettings, string parentField = null);
    }
}
