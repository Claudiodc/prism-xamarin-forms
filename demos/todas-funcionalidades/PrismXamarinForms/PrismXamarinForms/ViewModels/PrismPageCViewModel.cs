using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PrismXamarinForms.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismXamarinForms.ViewModels
{
    public class PrismPageCViewModel : BindableBase
    {
        private string _item;
        public string Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        public DelegateCommand IncluirItemNaListaCommand { get; set; }

        private readonly IEventAggregator _eventAggregator;
        private readonly IPageDialogService _pageDialogService;
        public PrismPageCViewModel(IPageDialogService pageDialogService, IEventAggregator eventAggregator)
        {
            this._pageDialogService = pageDialogService;
            this._eventAggregator = eventAggregator;

            this.IncluirItemNaListaCommand = new DelegateCommand(IncluirItemNaLista, PodeIncluirItemNaLista)
                                                 .ObservesProperty(() => this.Item);
        }

        private async void IncluirItemNaLista()
        {
            //Dispara uma evento (mensagem) informando que um item deve ser adicionado a uma lista
            this._eventAggregator.GetEvent<NovoItemAdicionadoNaListaEvent>().Publish(this.Item);
            this.Item = string.Empty;
            await this._pageDialogService.DisplayAlertAsync("Prism", "Um item foi adicionado na lista da aba EventToCommandBehavior e no menu Master!", "Ok");
        }

        private bool PodeIncluirItemNaLista() => !string.IsNullOrEmpty(this.Item);
    }
}
