using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrismXamarinForms.ViewModels
{
    //Para receber parâmetros de navegação, implemantamos a interface INavigatingAware
    //Essa interface tem um método chamado OnNavigatingTo(NavigationParameters parameters)
    //Quando uma página atrelada a essa view model receber uma navegação, esse método é invocado.
    //A partir dele, podemos trabalhar os valores dos parâmetros
    public class PrismTabbedPageViewModel : BindableBase, INavigatingAware
    {
        public void OnNavigatingTo(NavigationParameters parameters)
        {
            //NavigationParameters nada mais é do que um Dictionary<string, object>
            if (parameters.ContainsKey("parametro"))
                this.Titulo = parameters["parametro"].ToString();
        }

        //Ao receber a navegação, um parâmetro é informado com o nome parametro.
        //O valor desse parâmetro é passado para Titulo, que por sua vez está atrelado ao Title da Tabbed Page
        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { SetProperty(ref _titulo, value); }
        }

        public PrismTabbedPageViewModel()
        {

        }
    }
}