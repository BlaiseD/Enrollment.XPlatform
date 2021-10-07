using Enrollment.XPlatform.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Enrollment.XPlatform.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}