using Prism.Commands;
using Prism.Mvvm;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismXamarinForms.ViewModels
{
    public class PrismPageBViewModel : BindableBase
    {
        private string _opcaoEscolhida;
        public string OpcaoEscolhida
        {
            get { return _opcaoEscolhida; }
            set { SetProperty(ref _opcaoEscolhida, value); }
        }

        public DelegateCommand DisplayAlertUmBotaoCommand { get; set; }
        public DelegateCommand DisplayAlertDoisBotoesCancelCommand { get; set; }
        public DelegateCommand DisplayActionSheetCommand { get; set; }
        public DelegateCommand DisplayActionSheetDestruicaoCommand { get; set; }

        private readonly IPageDialogService _pageDialogService;
        public PrismPageBViewModel(IPageDialogService pageDialogService)
        {
            this._pageDialogService = pageDialogService;

            this.OpcaoEscolhida = "Nenhuma opção escolhida!";

            this.DisplayAlertUmBotaoCommand = new DelegateCommand(DisplayAlertUmBotao);
            this.DisplayAlertDoisBotoesCancelCommand = new DelegateCommand(DisplayAlertDoisBotoesCancel);
            this.DisplayActionSheetCommand = new DelegateCommand(DisplayActionSheet);
            this.DisplayActionSheetDestruicaoCommand = new DelegateCommand(DisplayActionSheetDestruicao);
        }

        private async void DisplayAlertUmBotao()
        {
            await this._pageDialogService.DisplayAlertAsync("Prism", "Apenas um Botão!", "OK");
            this.OpcaoEscolhida = "Sem ação atrelada a DisplayAlert com um botão";
        }

        private async void DisplayAlertDoisBotoesCancel()
        {
            var ok = await this._pageDialogService.DisplayAlertAsync("Prism", "Você pode confirmar ou cancelar a ação!", "OK", "Cancelar");

            if (ok)
                this.OpcaoEscolhida = "Ação confirmada";
            else
                this.OpcaoEscolhida = "Ação cancelada";
        }

        private async void DisplayActionSheet()
        {
            var opcao = await this._pageDialogService.DisplayActionSheetAsync("Prism", "Cancelar", null, "Opção A", "Opção B", "Opção C");
            this.OpcaoEscolhida = $"A opção selecionada foi: {opcao}";
        }

        private async void DisplayActionSheetDestruicao()
        {
            //Outra forma de gerenciar retorno de mensagens:
            //O legal disso é que podemos passar o ICommand como parâmetro e

            var opcaoA = ActionSheetButton.CreateButton("Opção A", new DelegateCommand(() =>
            {
                this.OpcaoEscolhida = "A opção selecionada foi: Opção A";
            }));

            var opcaoB = ActionSheetButton.CreateButton("Opção B", new DelegateCommand(() =>
            {
                this.OpcaoEscolhida = "A opção selecionada foi: Opção B";
            }));

            var opcaoC = ActionSheetButton.CreateButton("Opção B", new DelegateCommand(() =>
            {
                this.OpcaoEscolhida = "A opção selecionada foi: Opção C";
            }));

            var cancelar = ActionSheetButton.CreateCancelButton("Cancelar", new DelegateCommand(() =>
            {
                this.OpcaoEscolhida = "A opção selecionada foi: Cancelar";
            }));

            var excluir = ActionSheetButton.CreateDestroyButton("Excluir!!", new DelegateCommand(() =>
            {
                this.OpcaoEscolhida = "A opção selecionada foi: Excluir!!";
            }));

            await this._pageDialogService.DisplayActionSheetAsync("Prism", opcaoA, opcaoB, opcaoC, cancelar, excluir);
        }
    }
}