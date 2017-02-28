using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismXamarinForms.Events;
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

        public PrismPageAViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            //Nesse ponto, a view model se registra para ouvir um evento
            //Toda vez que alguém disparar o evento NovoItemAdicionadoNaListaEvent um item é adicionado na lista de Itens
            eventAggregator.GetEvent<NovoItemAdicionadoNaListaEvent>().Subscribe(item =>
            {
                this.Itens.Add($"Item de evento: {item}");
            });

            this.Itens = new ObservableCollection<string>();
            for (int i = 0; i < 5; i++)
                this.Itens.Add($"Item {i}");

            this.ItemTappedCommand = new DelegateCommand<string>((linha) =>
            {
                navigationService.NavigateAsync($"PrismPageD?texto={linha} selecionado!", useModalNavigation: true);
            });
        }
    }
}
