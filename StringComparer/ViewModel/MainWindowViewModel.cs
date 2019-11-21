// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="The Watson Laboratory">
//   Copyright © 2019 The Watson Laboratory
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StringComparer.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using StringComparer.Annotations;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string tb1 = "Your string here...";
        private string tb2 = "Your string here...";

        public string Tb1
        {
            get => this.tb1;
            set
            {
                this.tb1 = value;
                this.OnPropertyChanged(nameof(this.Tb1));
            }
        }

        public string Tb2
        {
            get => this.tb2;
            set
            {
                this.tb2 = value;
                this.OnPropertyChanged(nameof(this.Tb2));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}