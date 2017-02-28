using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismXamarinForms.ViewModels
{
    //A classe BindableBase expões métodos para facilitar a criação de propriedades Bindables.
    //BindableBase implementa o INotifyPropertyChanged e o método SetProperty atribui valor e lança o evento notificando que a propriedade foi alterada
    public class MainPageViewModel : BindableBase
    {
        private string _parametro;
        public string Parametro
        {
            get { return _parametro; }
            set { SetProperty(ref _parametro, value); }
        }

        private bool _navegacaoAbsoluta;
        public bool NavegacaoAbsoluta
        {
            get { return _navegacaoAbsoluta; }
            set { SetProperty(ref _navegacaoAbsoluta, value); }
        }

        private bool _deepLinking;
        public bool DeepLinking
        {
            get { return _deepLinking; }
            set { SetProperty(ref _deepLinking, value); }
        }

        //DelegaCommand implementa o ICommand adicionando várias funcionalidades
        public DelegateCommand NavegarCommand { get; set; }

        //Aqui é injetada o serviço de Navegação. O mais interessante que ele é uma interface, sendo assim, conseguimos testar essa ViewModel facilmente.
        private readonly INavigationService _navigationService;
        //O nome do parâmetro do INavigationService obrigatóriamente deve se chamar navigationService
        public MainPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            //O ICommand tem dois métodos: Execute e CanExecute.  
            //No nosso caso, o Execute será representado por Navegar, e o CanExecute por PodeNavegar
            //Colocamos uma regra: A ação de navegar só vai ocorrer caso a propriedade Parametro contenha valor.
            //Sendo assim, observamos a mudança de valor da propriedade Parametro com o método ObservesProperty
            //Isso é feito pois essa propriedade influencia diretamente no PodeNavegar (CanExecute).
            //É possível ter vários parâmetros observáveis (note o trecho comentado de código)
            //Toda vez que as propriedades observadas alterarem seu valor, o método PodeNavegar é invocado.
            //Uma coisa fantástica é que, como o NavegarCommand está atrelado (bind) a um botão, a propriedade IsEnabled do botão depende do retorno do método PodeNavegar
            //Sendo assim, se o método PodeNavegar retornar true, o botão ficará. Caso contrário, desabilitado.
            this.NavegarCommand = new DelegateCommand(Navegar, PodeNavegar)
                                                     .ObservesProperty(() => this.Parametro);
            //.ObservesProperty(() => this.OutroParametro);
        }

        private bool PodeNavegar()
        {
            return !string.IsNullOrEmpty(this.Parametro);
        }

        private async void Navegar()
        {
            //A navegação Relativa mantém todas as páginas anteriores abertas, sendo assim, é possível voltar para página anterior 
            var uriKind = UriKind.Relative;

            if (this._navegacaoAbsoluta)
            {
                //A naveção Absoluta remove toda as páginas, sendo assim, caso o botão voltar seja pressionado, o App vai para background! 
                //Equivale a MainPage = new Page();
                uriKind = UriKind.Absolute;
            }

            //Explicação da navegação:

            //- Uma Master Detail Page é formada por duas páginas (Page): Master e Detail. Sendo que a Detail é usada como Menu Lateral, e a Detail página principal.
            //- Numa Tabbed Page, cada aba é uma Page.

            //Abre-se uma página do tipo MasterDetail.
            //Decompondo a URL abaixo temos:
            //PrismMasterDetailPage: Master Detail Page
            //PrismNavigationPage: Navigation Page, usado para efetuar navegações, algo como MainPage = new NavigationPage(new MyPage())
            //PrismTabbedPage: Tabbed Page, página com duas abas (Pages) PrismPageA, PrismPageB e PrismPageC

            //Quando se navega até uma Master Detail, precisamos informar qual será a página usada como Detail.
            //No caso abaixo, a navegação vai abrir uma Master Detail, com uma Tabbed Page navegável no Detail (note o PrismNavigationPage) com a página PrismPageB selecionada

            //É importante notar que, para que as páginas sejam navegáveis, utilizando o botão voltar ou o ícone na barra superior, é preciso que exista uma NavigationPage.
            //Na navegação com Prism, por ser feita via string, deve-se usar uma página existente, nesse caso a PrismNavigationPage.

            var url = $"/PrismMasterDetailPage/PrismNavigationPage/PrismTabbedPage?parametro={this._parametro}/PrismPageB";

            //Outra feature sensacional do Prism, é que podemos empilhar uma navegação, abrindo uma sequencia de páginas...
            //Isso é importante quando termos o Deep Linking.
            //Imagine um cenário onde você recebe um SMS com uma notícia. 
            //Seu App de Notícias tem uma página inicial com categorias, 
            //uma outra página com as chamadas das notícias de uma determinada categoria, 
            //e uma página com a noticia selecionada.
            //Ao tocar no SMS automáticamente seu App abre com a notícia selecionada, sendo que a página da categoria daquela notícia também foi aberta!
            //A url ficaria algo como "/Principal/Navegacao/Categorias?=idCategoria=1/Noticias/Noticia?idNoticia=123456"
            //Sensacional, certo? Podemos simular algo parecido aqui...
            url += "/PrismPageD?texto=Simulando um Deep Linking";

            var uri = new Uri(url, uriKind);

            await this._navigationService.NavigateAsync(uri);
        }
    }
}


