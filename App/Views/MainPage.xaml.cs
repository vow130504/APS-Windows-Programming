using App.Views;
using App.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void GoToSecondPage_Click(object sender, RoutedEventArgs e)
        {
            // Điều hướng đến SecondPage
            this.Frame.Navigate(typeof(Revenue));
        }
    }
}