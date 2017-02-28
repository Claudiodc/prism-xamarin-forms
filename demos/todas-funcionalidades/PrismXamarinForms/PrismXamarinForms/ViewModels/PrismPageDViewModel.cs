using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace PrismXamarinForms.ViewModels
{
    public class PrismPageDViewModel : BindableBase
    {
        public DelegateCommand VoltarCommand { get; set; }

        public PrismPageDViewModel(INavigationService navigationService)
        {
            this.VoltarCommand = new DelegateCommand(async () =>
            {
                await navigationService.GoBackAsync();
            });
        }
    }
}
