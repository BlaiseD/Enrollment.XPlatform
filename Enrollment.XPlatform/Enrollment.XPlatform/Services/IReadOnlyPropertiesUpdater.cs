using Enrollment.Forms.Configuration.DetailForm;
using Enrollment.XPlatform.ViewModels.ReadOnlys;
using System.Collections.Generic;

namespace Enrollment.XPlatform.Services
{
    public interface IReadOnlyPropertiesUpdater
    {
        void UpdateProperties(IEnumerable<IReadOnly> properties, object entity, List<DetailItemSettingsDescriptor> fieldSettings, string parentField = null);
    }
}
