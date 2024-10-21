using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.System.Threading;

namespace App
{
    public sealed partial class MainWindow : Window
    {
        private string _currentTime;

        public MainWindow()
        {
            this.InitializeComponent();

            // Khởi tạo bộ đếm thời gian để cập nhật CurrentTime
            ThreadPoolTimer timer = ThreadPoolTimer.CreatePeriodicTimer(Timer_Tick, TimeSpan.FromSeconds(1));
        }

        private void Timer_Tick(ThreadPoolTimer timer)
        {
            // Cập nhật CurrentTime trên thread UI chính
            DispatcherQueue.TryEnqueue(() =>
            {
                // Hiển thị ngày/tháng/năm và giờ/phút/giây
                _currentTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                timeTextBlock.Text = _currentTime; // Cập nhật thời gian lên TextBlock
            });
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var screen = new Login();
            screen.Activate();
            this.Close();
        }
    }
}
