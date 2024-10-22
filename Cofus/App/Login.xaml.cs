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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace App
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Window
    {
        public Login()
        {
            this.InitializeComponent();
        } 
        private bool CheckLogin(string user, string password)
        {
            return user == "user123" && password == "password123";
        }

        private void Window_Activated(object sender, WindowActivatedEventArgs args)
        {
            Title = "App is ready";

            try
            {
                var localSettings = ApplicationData.Current.LocalSettings;
                if (localSettings.Values.ContainsKey("user"))
                {
                    usernameTextBox.Text = localSettings.Values["user"].ToString();
                    var encryptedPasswordInBase64 = localSettings.Values["password"].ToString();
                    var entropyInBase64 = localSettings.Values["entropy"].ToString();

                    var encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordInBase64);
                    var entropyInBytes = Convert.FromBase64String(entropyInBase64);
                    var passwordInBytes = ProtectedData.Unprotect(encryptedPasswordInBytes, entropyInBytes, DataProtectionScope.CurrentUser);
                    passwordBox.Password = Encoding.UTF8.GetString(passwordInBytes);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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

                var screen = new HomePage();
                this.Content = screen; // Gán nội dung của cửa sổ hiện tại là trang mới

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

                var screen = new HomePage();
                this.Content = screen; // Gán nội dung của cửa sổ hiện tại là trang mới

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
    }
}