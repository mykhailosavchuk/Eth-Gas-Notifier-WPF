using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace GasNotifier
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        public NotificationWindow()
        : base()
        {
            this.InitializeComponent();
            this.Closed += this.NotificationWindowClosed;
        }

        private void NotificationWindowClosed(object sender, EventArgs e)
        {

        }

        public void Show(string Message, string strType)
        {
            this.Topmost = true;
            base.Show();

            lbl_message.Content = Message;
            lbl_information.Content = strType;
            this.Closed += this.NotificationWindowClosed;

            var workingArea = Screen.PrimaryScreen.WorkingArea;

            this.alarm_date.Content = DateTime.Now.ToString("HH:mm:ss");
            this.Left = workingArea.Right - this.ActualWidth - 20;
            double top = workingArea.Bottom - this.ActualHeight - 20;


            foreach (Window window in System.Windows.Application.Current.Windows)
            {
                string windowName = window.GetType().Name;

                if (windowName.Equals("NotificationWindow") && window != this)
                {
                    // Adjust any windows that were above this one to drop down
                    if (window.Top < this.Top)
                    {
                        window.Top = window.Top + this.ActualHeight;
                    }
                }
            }

            SystemSounds.Beep.Play();
            this.Top = top;
        }
        private void ImageMouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void DoubleAnimationCompleted(object sender, EventArgs e)
        {
            if (!this.IsMouseOver)
            {
                //Settings.Instance.ni.Dispose();
                this.Close();
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            //Settings.Instance.ni.Dispose();
            this.Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Settings.Instance.ni.Dispose();
            this.Close();
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Settings.Instance.ni.Dispose();
            //this.Close();
        }
    }


    internal static class Constants
    {
        internal static readonly string version = "Task View 1.7.8b";
        internal static readonly string current_version = "1.7.8b";

        internal static readonly string LoaderMainLogFileName = "TaskView.Log.txt";
        internal static readonly string Icon = "TaskView.ico";
        internal static readonly string RestProcess = "RestProcess";
        internal static readonly string Unknown = "Unknown";
        internal static readonly string Default = "Default";
        internal static readonly int AutoPatchPort = 19996;
        internal static readonly string AutoPatchServerIP = "192.168.109.250";


        internal static readonly string TranslateCom = "translate.";
        internal static readonly string Updating = "updating";

        //Audio File Name
        internal static readonly string AudioFileName = "Auo.dll";
        internal static readonly int nAudioSession = 1;

        internal static readonly string AudioSource = "Speakers / Headphones (2-Realtek(R) Audio)";
        internal static readonly int DeviceIndex = 0;
        internal static readonly string Format = "WMA";
        internal static readonly string Bitrate = "8";
        internal static readonly string Samplerate = "8000";
        internal static readonly string Bits = "8";
        internal static readonly string Channels = "1";
        internal static readonly string Mode = "WasapiLoopbackCapture";
        internal static readonly int Latency = 100;
        internal static readonly string AudioFileExtension = ".wma";
        internal static readonly bool StartOnNoise = false;
        internal static readonly string StartOnNoiseVal = "1";
        internal static readonly bool StopOnSilence = false;
        internal static readonly string StopOnSilenceVal = "3";
        internal static readonly string StopOnSilenceSeconds = "1";
        internal static readonly int fAlarmCheckTime = 500;
        internal static readonly int sAlarmCheckTime = 500;
        internal static readonly int nAudioCheckTime = 1500;

        //Slide Time
        internal static readonly int slideTime = 50;

        //File name
        internal static readonly string DbFileName = "Contents.lib";
        internal static readonly string DbServerFileName = "Contents_Server.lib";

        internal static readonly string[] strArray = { "A", "C", "E", "G", "I", "K", "M", "Q", "R", "T" };

        // Filter
        internal static readonly string[] strForbiddenURL = { "youtube", "facebook", "social", "twitter", "linkedin", "sex", "adult", "dating", "vpn", "vps", "proxy" };
        internal static readonly string[] strForbiddenProcess = { "iexplore.exe", "opera.exe", "msedge.exe", "browser.exe", "Ferdi.exe", "Franz.exe", "vlc.exe", "QQPlayer.exe", "BlueStacks.exe", "Nox.exe", "CCleaner64.exe", "WasherSvcc.exe" };

        internal static readonly string strImgExtension = "psl";
        internal static readonly string DownloadFile = "dwControl.dll";
        internal static readonly string ForbiddenFile = "Disable.lib";

        //File Pattern
        internal static readonly string filePattern = "*!!*";

        //URL Hooking
        internal static readonly int urlSessionTime = 3;
        internal static readonly int urlActiveTime = 10;
        internal static readonly string urlFileName = "Browser.dat";


        //Security
        internal static readonly string InitPassword = "youarefool";
        internal static readonly string Md5Key = "A!9HHhi%XjjYY4YP2@Nob009X";

        //Hiden processes
        internal static readonly string HideProcess_IDLE = "Idle.exe";
        internal static readonly string HideProcess_LockApp = "LockApp.exe";
        internal static readonly string HideProcess_APH = "ApplicationFrameHost.exe";

        //Base Path
        internal static readonly string SetupPath = "C:\\Program Files (x86)\\RM\\Client";
        internal static readonly string RegPath = "Ryonbong\\Client";
        internal static readonly string BaseDirectory = "D:\\CaptureData";
        internal static readonly string Version = "1.0";

        //Client base setting 
        internal static readonly int SessionTime = 10;
        internal static readonly int ActiveDuration = 5 * 60;
        internal static readonly int CaptureTime = 60;
        internal static readonly int SlideWidth = 355;
        internal static readonly int SlideHeight = 200;
        internal static readonly int CaptureWidth = 800;
        internal static readonly int CaptureHeight = 450;
        internal static readonly int DelDataDay = 7;

        //Execption
        internal static readonly Byte exExit = 1;
        internal static readonly Byte exRepair = 2;
        internal static readonly Byte exResume = 3;

        //Network protocols
        internal static readonly int Port = 9999;
        internal static readonly int Port1 = 20000;
        internal static readonly string Se_ClientInfo = "RS21";
        internal static readonly string Se_Forbidden = "RS22";
        internal static readonly string Se_Confirm = "RS23";
        internal static readonly string Se_SetInfo = "RS24";
        internal static readonly string Se_Password = "RS25";
        internal static readonly string Se_End = "RS26";
        internal static readonly string Se_AutoVersion = "RS27";
        internal static readonly string Se_FileEnd = "RS28";

        internal static readonly string Se_MsgAudio = "RS31";
        internal static readonly string Se_MsgUSB = "RS32";
        internal static readonly string Se_MsgDownload = "RS33";
        internal static readonly string Se_MsgForbidden = "RS34";
        internal static readonly string Se_MsgDownLoading = "RS35";
        internal static readonly string Se_MsgDanger = "RS36";

        internal static readonly string Se_DataSlide = "RS11";
        internal static readonly string Se_DataCapture = "RS12";
        internal static readonly string Se_DataAudio = "RS13";
        internal static readonly string Se_DataProcess = "RS14";
        internal static readonly string Se_DataUSB = "RS15";
        internal static readonly string Se_DataURL = "RS16";
        internal static readonly string Se_DataDownload = "RS17";
        internal static readonly string Se_DataForbidden = "RS18";
        internal static readonly string Se_DataHuman = "RS19";

        internal static readonly string Se_VidCapture = "RS41";
        internal static readonly string Se_VidAudio = "RS42";
        internal static readonly string Se_VidCMD = "RS43";
        internal static readonly string Se_AudioData = "RS44";

        /// <summary>
        /// ////////////////////////////        /////////////////
        /// </summary>

        internal static readonly string Re_ClientInfo = "RR21";
        internal static readonly string Re_Forbidden = "RR22";
        internal static readonly string Re_Confirm = "RR23";
        internal static readonly string Re_Password = "RR24";
        internal static readonly string Re_SetInfo = "RR25";
        internal static readonly string Re_End = "RR26";

        internal static readonly string Re_MsgAudio = "RR31";
        internal static readonly string Re_MsgUSB = "RR32";
        internal static readonly string Re_MsgDownload = "RR33";
        internal static readonly string Re_MsgForbidden = "RR34";
        internal static readonly string Re_MsgDownLoading = "RR35";
        internal static readonly string Re_MsgDanger = "RR36";

        internal static readonly string Re_DataSlide = "RR11";
        internal static readonly string Re_DataCapture = "RR12";
        internal static readonly string Re_DataAudio = "RR13";
        internal static readonly string Re_DataProcess = "RR14";
        internal static readonly string Re_DataUSB = "RR15";
        internal static readonly string Re_DataURL = "RR16";
        internal static readonly string Re_DataDownload = "RR17";
        internal static readonly string Re_DataForbidden = "RR18";
        internal static readonly string Re_DataHuman = "RR19";


        internal static readonly string Re_VidCapture = "RR41";
        internal static readonly string Re_VidAudio = "RR42";
        internal static readonly string Re_VidCMD = "RR43";
        internal static readonly string Re_AudioData = "RR44";

        internal static readonly string Re_ServerProcessData = "RR51";
        internal static readonly string Re_ServerSlideData = "RR52";
        internal static readonly string Re_ServerCaptureData = "RR53";

        internal static readonly string Re_ServerProcessFile = "RR61";
        internal static readonly string Re_ServerSlideFile = "RR62";
        internal static readonly string Re_ServerCaptureFile = "RR63";

        internal static readonly string Re_ServerTime = "RR00";

    }

}
