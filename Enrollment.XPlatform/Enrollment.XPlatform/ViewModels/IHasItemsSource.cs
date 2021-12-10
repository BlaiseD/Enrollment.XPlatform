using System;
using System.Collections.Generic;
using System.Text;

namespace Enrollment.XPlatform.ViewModels
{
    public interface IHasItemsSource
    {
        void Reload(object entity);
    }
}
