// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="The Watson Laboratory">
//   Copyright © 2019 The Watson Laboratory
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace StringComparer.ViewModel
{
    using System.Windows.Input;

    public class MainWindowViewModel
    {
        public string Tb1 { get; }
        public string Tb2 { get; }

        public ICommand OnTb1MouseUpCommand { get; }
        public ICommand OnTb2MouseUpCommand { get; }
    }
}