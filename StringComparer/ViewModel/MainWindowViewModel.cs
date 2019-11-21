// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="The Watson Laboratory">
//   Copyright © 2019 The Watson Laboratory
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StringComparer.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using StringComparer.Annotations;

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string tb1 = "Your string here...";
        private string tb2 = "Your string here...";
        private ICommand compareCommand;
        private string compareResult = "Not Set";
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CompareCommand
        {
            get
            {
                return this.compareCommand ??= new CompareCommand(p => this.CanCompare, p => this.Compare());
            }
        }

        public bool CanCompare => true;

        private void Compare()
        {
            this.CompareResult = this.Tb1 == this.Tb2 ? "Success" : "Failure";
        }

        public string CompareResult
        {
            get => this.compareResult;
            set
            {
                this.compareResult = value;
                this.OnPropertyChanged(nameof(this.CompareResult));
            }
        }

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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class CompareCommand : ICommand
    {
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;

        public CompareCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns>
        /// <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}