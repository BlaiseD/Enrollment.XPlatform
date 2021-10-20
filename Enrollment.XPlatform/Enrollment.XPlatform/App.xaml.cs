using Enrollment.XPlatform.Services;
using Microsoft.Extensions.DependencyInjection;
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

            //DependencyService.Register<MockDataStore>();
            //MainPage = new AppShell();
        }

        public const string BASE_URL = "https://contosoapibps.azurewebsites.net/";

        #region Properties
        public static IServiceProvider ServiceProvider { get; set; }
        public static ServiceCollection ServiceCollection { get; set; }
        #endregion Properties

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
