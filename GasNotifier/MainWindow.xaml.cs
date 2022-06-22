using Flurl.Http;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GasNotifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Timers.Timer aTimer;
        private NotifyIcon trayIcon;
        private bool IsAlarmIsChecked = false;
        private bool IsSoundIsChecked = false;
        private int preValue = 0;
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                FetchFee();

                aTimer = new System.Timers.Timer(8000);
                aTimer.Elapsed += startWork;
                aTimer.AutoReset = true;
                aTimer.Enabled = true;

                System.IO.Stream st;
                System.Reflection.Assembly a = System.Reflection.Assembly.GetExecutingAssembly();
                st = a.GetManifestResourceStream("GasNotifier.logo.ico");

                this.trayIcon = new NotifyIcon();
                this.trayIcon.Icon = new System.Drawing.Icon(st);
                this.trayIcon.Text = "Gas Notifier";
                this.trayIcon.Click += new EventHandler(m_notifyIcon_Click);

            }catch(Exception evt)
            {
            }
        }

        void m_notifyIcon_Click(object sender, EventArgs e)
        {
            Show();
        }

        async void FetchFee()
        {
            var result = await "https://api.etherscan.io/api?module=gastracker&action=gasoracle&apikey=E1XF8CRJU6EB1XCTN7EKEKUFAW772Y3UXQ"
                    .GetJsonAsync<JObject>();
            GasFeeResult fees = JsonConvert.DeserializeObject<GasFeeResult>(result["result"].ToString());
            this.currentValue.Text = fees.suggestBaseFee.ToString();
        }
        async void startWork(Object source, ElapsedEventArgs e)
        {
            try
            {
                var result = await "https://api.etherscan.io/api?module=gastracker&action=gasoracle&apikey=E1XF8CRJU6EB1XCTN7EKEKUFAW772Y3UXQ"
                     .GetJsonAsync<JObject>();
                GasFeeResult fees = JsonConvert.DeserializeObject<GasFeeResult>(result["result"].ToString());
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    this.currentValue.Text = fees.suggestBaseFee.ToString();
                });

                RegistryKey key = Registry.CurrentUser.OpenSubKey("GasFeeNotifier", true);
                var baseFee = key.GetValue("BaseFee").ToString();

                if (baseFee != "None")
                {
                    if(preValue != (int)fees.suggestBaseFee)
                    {
                        preValue = (int)fees.suggestBaseFee;
                        if (Int32.Parse(baseFee) > fees.suggestBaseFee)
                        {
                            if(IsAlarmIsChecked)
                            {
                                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                                {
                                    var notify = new NotificationWindow();
                                    notify.Show("Hello, Good news", $"Base gas fee is now {(int)fees.suggestBaseFee} Gwei.");
                                });
                            }
                            if (IsSoundIsChecked)
                                SystemSounds.Beep.Play();
                        }
                    }
                }

            }catch(Exception evt)
            {
                this.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    var notify = new NotificationWindow();
                    notify.Show("Your app has something wrong.", "Please check your environment.");
                });
            }
        }

        private void MinimizeBtnComand(object sender, RoutedEventArgs e)
        {
            Hide();

            this.trayIcon.Visible = true;
        }

        private void onAlarmChecked(object sender, RoutedEventArgs e)
        {
            IsAlarmIsChecked = true;
        }

        private void onAlarmUnChecked(object sender, RoutedEventArgs e)
        {
            IsAlarmIsChecked = false;
        }

        private void onSoundChecked(object sender, RoutedEventArgs e)
        {
            IsSoundIsChecked = true;
        }

        private void onSoundUnChecked(object sender, RoutedEventArgs e)
        {
            IsSoundIsChecked = false;
        }

        private void onMoveWindow(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
