// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="The Watson Laboratory">
//   Copyright © 2019 The Watson Laboratory
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StringComparer.ViewModel
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
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

        /// <summary>
        /// Compare the two strings, Tb1 and Tb2, and set the compare result string to indicate the result of the comparison.
        /// </summary>
        /// <remarks>If the compare succeeds then the compare result will be a simple success message. If the compare fails, the compare result should show the substrings where the difference occurred with a 10-precision character padding.</remarks>
        private void Compare()
        {
            this.CompareResult = this.Tb1 == this.Tb2 ? "Success" : this.Difference;
        }

        private string Difference
        {
            get
            {
                var tb1Array = this.tb1.ToCharArray();
                var tb2Array = this.tb2.ToCharArray();
                var longerStringLength = this.tb1.Length >= this.tb2.Length ? this.tb1.Length : this.tb2.Length;
                var shorterStringLength = this.tb1.Length >= this.tb2.Length ? this.tb2.Length : this.tb1.Length;
                var index = 0;
                var differenceFound = false;
                var result = "Couldn't find a difference between the two strings.";

                if (this.tb1.Length != this.tb2.Length)
                {
                    while (index < shorterStringLength && !differenceFound)
                    {
                        if (tb1Array[index] != tb2Array[index])
                        {
                            result = index.ToString();
                            differenceFound = true;
                        }

                        index++;
                    }

                    if (!differenceFound)
                    {
                        result = index.ToString();
                    }
                }
                else
                {
                    while (!differenceFound && index < longerStringLength)
                    {
                        if (tb1Array[index] != tb2Array[index])
                        {
                            result = index.ToString();
                            differenceFound = true;
                        }

                        index++;
                    }
                }

                //if (differenceFound)
                //{
                //    index--;
                //    var tb1Prepadding = this.tb1.Substring(index - 11, 10);
                //    var tb1Postpadding = this.tb1.Substring(index + 1, 10);
                //    var tb2Prepadding = this.tb2.Substring(index - 11, 10);
                //    var tb2Postpadding = this.tb2.Substring(index + 1, 10);
                //    var tb1Result = string.Concat(tb1Prepadding, this.tb1.Substring(index, 1), tb1Postpadding);
                //    var tb2Result = string.Concat(tb2Prepadding, this.tb2.Substring(index, 1), tb2Postpadding);
                //    result = string.Concat(tb1Result, "\n", tb2Result);
                //}

                return result;
            }
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