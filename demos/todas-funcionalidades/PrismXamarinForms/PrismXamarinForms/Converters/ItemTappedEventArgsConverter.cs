using System;
using System.Globalization;
using Xamarin.Forms;

namespace PrismXamarinForms.Converters
{

    //Aqui é criado o converter
    //Ao tocar na lista, o objeto que se tem é do tipo ItemTappedEventArgs
    //Esse converter obtém o valor do conteúdo da lista(itemTappedEventArgs.Item) e passa como parâmetro no Command
    //Nesse caso estamos tratando apenas string, poderia ser um objeto completo.
    //Sendo assim em nossa ViewModel temos um DelegateCommand<string>
    public class ItemTappedEventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var itemTappedEventArgs = value as ItemTappedEventArgs;
            if (itemTappedEventArgs == null)
                throw new ArgumentException("Expected value to be of type ItemTappedEventArgs", nameof(value));

            return itemTappedEventArgs.Item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
    }
}

