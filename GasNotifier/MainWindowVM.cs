using Microsoft.Win32;
using ReactiveUI;
using System;
using System.Windows;
using System.Windows.Input;

namespace GasNotifier
{
    public class MainWindowVM: ReactiveObject
    {
        public MainWindowVM()
        {
            SetBtnCommand = new RelayCommand(o => SetBtnClick(o));
            ResetBtnCommand = new RelayCommand(o => ResetBtnClick(o));
            CloseBtnCommand = new RelayCommand(o => CloseBtnClick(o));
            MinimizeBtnCommand = new RelayCommand(o => MinimizeBtnClick(o));

            
            RegistryKey key = Registry.CurrentUser.OpenSubKey("GasFeeNotifier", true);
            if(key == null)
            {
                key = Registry.CurrentUser.CreateSubKey("GasFeeNotifier");
                key.SetValue("BaseFee", (int)limitedValue);
            }
            var baseFee = key.GetValue("BaseFee").ToString();

            if(baseFee != "None" && Int32.Parse(baseFee) > 0)
            {
                EnableEdit = Visibility.Hidden;
                ToggleEdit = Visibility.Visible;
                LimitedValue = Int32.Parse(baseFee);
                LimitValue = $"Currently set: {(int)limitedValue} Gwei.";

            }
        }

        private float limitedValue = 0;
        public float LimitedValue
        {
            get => limitedValue;
            set => this.RaiseAndSetIfChanged(ref limitedValue, value);
        }
        private Visibility enableEdit = Visibility.Visible;
        public Visibility EnableEdit
        {
            get => enableEdit;
            set 
            {
                this.RaiseAndSetIfChanged(ref enableEdit, value);
            }

        }

        private Visibility toggleEdit = Visibility.Hidden;
        public Visibility ToggleEdit
        {
            get => toggleEdit;
            set => this.RaiseAndSetIfChanged(ref toggleEdit, value);
        }

        private string limitValue = "";
        public string LimitValue
        {
            get => limitValue;
            set => this.RaiseAndSetIfChanged(ref limitValue, value);
        }

        public ICommand SetBtnCommand { get; set; }
        private void SetBtnClick(object sender)
        {
            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey("GasFeeNotifier");
            key.SetValue("BaseFee", (int)limitedValue);
            key.Close();

            EnableEdit = Visibility.Hidden;
            ToggleEdit = Visibility.Visible;
            LimitValue = $"Currently set: {(int)limitedValue} Gwei.";
        }

        public ICommand ResetBtnCommand { get; set; }
        private void ResetBtnClick(object sender)
        {
            RegistryKey key;
            key = Registry.CurrentUser.CreateSubKey("GasFeeNotifier");
            key.SetValue("BaseFee", 0);
            key.Close();

            EnableEdit = Visibility.Visible;
            ToggleEdit = Visibility.Hidden;
            LimitedValue = 0;
        }

        public ICommand CloseBtnCommand { get; set; }
        private void CloseBtnClick(object sender)
        {
            Environment.Exit(Environment.ExitCode);
        }

        public ICommand MinimizeBtnCommand { get; set; }
        private void MinimizeBtnClick(object sender)
        {
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter ?? "<N/A>");
        }

    }
}
