using MaterialDesignThemes.Wpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ElectronicRaffle.CommonUi.Controls
{
    public class ApplicationTaggingViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand CancelCommand { get; } = new RelayCommand(Cancel);

        private static void Cancel(object obj)
        {
            var command = (ICommand)DialogHost.CloseDialogCommand;
            command.Execute(obj);
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
