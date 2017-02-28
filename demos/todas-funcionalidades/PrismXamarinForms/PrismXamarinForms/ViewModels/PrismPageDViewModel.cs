using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismXamarinForms.ViewModels
{
    public class PrismPageDViewModel : BindableBase, INavigatingAware
    {
        private string _texto;
        public string Texto
        {
            get { return _texto; }
            set { SetProperty(ref _texto, value); }
        }

        public DelegateCommand VoltarCommand { get; set; }

        public PrismPageDViewModel(INavigationService navigationService)
        {
            this.VoltarCommand = new DelegateCommand(async () =>
            {
                await navigationService.GoBackAsync();
            });
        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //NavigationParameters nada mais é do que um Dictionary<string, object>
            if (parameters.ContainsKey("texto"))
                this.Texto = parameters["texto"].ToString();
        }
    }
}
