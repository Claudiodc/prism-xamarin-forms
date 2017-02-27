using Prism.Unity;
using PrismXamarinForms.Views;

namespace PrismXamarinForms
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<PrismNavigationPage>();
            Container.RegisterTypeForNavigation<PrismMasterDetailPage>();
            Container.RegisterTypeForNavigation<PrismTabbedPage>();
            Container.RegisterTypeForNavigation<PrismPageA>();
            Container.RegisterTypeForNavigation<PrismPageB>();
            Container.RegisterTypeForNavigation<PrismPageC>();
        }
    }
}
