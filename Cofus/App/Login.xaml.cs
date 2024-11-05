using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;

namespace App
{
    public sealed partial class Login : Window
    {
        private ICoffeeShopDAO _dao;

        public Login()
        {
            this.InitializeComponent();
            _dao = new MockCoffeeShopDAO();
        }

        private void SwitchToSignUp(object sender, RoutedEventArgs e)
        {
            SignInPanel.Visibility = Visibility.Collapsed;
            SignUpPanel.Visibility = Visibility.Visible;
        }

        private void SwitchToSignIn(object sender, RoutedEventArgs e)
        {
            SignInPanel.Visibility = Visibility.Visible;
            SignUpPanel.Visibility = Visibility.Collapsed;
        }

        private bool CheckLogin(string user, string password)
        {
            var existingUser = _dao.GetUserByUsername(user);
            return existingUser != null && existingUser.Password == password;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string user = usernameTextBox.Text;
            string password = passwordBox.Password;

            if (CheckLogin(user, password))
            {
                if (rememberCheckBox.IsChecked == true)
                {
                    var passwordInBytes = Encoding.UTF8.GetBytes(password);
                    var entropyInBytes = new byte[20];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(entropyInBytes);
                    }
                    var encryptedPassword = ProtectedData.Protect(passwordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                    var encryptedPasswordInBase64 = Convert.ToBase64String(encryptedPassword);
                    var entropyInBase64 = Convert.ToBase64String(entropyInBytes);

                    var localSettings = ApplicationData.Current.LocalSettings;
                    localSettings.Values["user"] = user;
                    localSettings.Values["password"] = encryptedPasswordInBase64;
                    localSettings.Values["entropy"] = entropyInBase64;
                }

                var screen = new InventoryManagement();
                this.Content = screen;
            }
            else
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Incorrect username or password entered\n",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }

        private async void Signup_Click(object sender, RoutedEventArgs e)
        {
            string user = usernameTextBox.Text;
            string password = passwordBox.Password;

            if (_dao.GetUserByUsername(user) == null)
            {
                _dao.AddUser(new User { Username = user, Password = password });
                var screen = new HomePage();
                this.Content = screen;
            }
            else
            {
                await new ContentDialog()
                {
                    XamlRoot = this.Content.XamlRoot,
                    Content = "Username already exists\n",
                    CloseButtonText = "OK"
                }.ShowAsync();
            }
        }
    }
}