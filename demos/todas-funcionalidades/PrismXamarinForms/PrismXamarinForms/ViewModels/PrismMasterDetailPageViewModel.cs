using Prism.Events;
using Prism.Mvvm;
using PrismXamarinForms.Events;

namespace PrismXamarinForms.ViewModels
{
    public class PrismMasterDetailPageViewModel : BindableBase
    {
        private string _mensagemVindaDeUmEvento;
        public string MensagemVindaDeUmEvento
        {
            get { return _mensagemVindaDeUmEvento; }
            set { SetProperty(ref _mensagemVindaDeUmEvento, value); }
        }

        public PrismMasterDetailPageViewModel(IEventAggregator eventAggregator)
        {
            //Nesse ponto, a view model se registra para ouvir um evento
            //Toda vez que alguém disparar o evento NovoItemAdicionadoNaListaEvent um item é adicionado na lista de Itens
            eventAggregator.GetEvent<NovoItemAdicionadoNaListaEvent>().Subscribe(item =>
            {
                this.MensagemVindaDeUmEvento = $"Item de evento: {item}";
            });
        }
    }
}
