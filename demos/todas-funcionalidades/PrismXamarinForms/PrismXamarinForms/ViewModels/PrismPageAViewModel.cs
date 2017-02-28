using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PrismXamarinForms.ViewModels
{
    public class PrismPageAViewModel : BindableBase
    {
        public ObservableCollection<string> Itens { get; set; }

        public DelegateCommand<string> ItemTappedCommand { get; set; }

        public PrismPageAViewModel(INavigationService navigationService)
        {
            this.Itens = new ObservableCollection<string>();
            for (int i = 0; i < 10; i++)
                this.Itens.Add($"Item {i}");

            this.ItemTappedCommand = new DelegateCommand<string>((linha) =>
            {
                navigationService.NavigateAsync($"PrismPageD?texto={linha} selecionado!", useModalNavigation: true);
            });
        }
    }
}
