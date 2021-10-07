using Enrollment.XPlatform.Services;
using Enrollment.XPlatform.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Enrollment.XPlatform
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
