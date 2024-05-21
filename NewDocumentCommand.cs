// NewDocumentCommand.cs
using System;
using System.Windows.Input;

namespace WpfApp1
{
    public class NewDocumentCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true; // Implement your logic here
        }

        public void Execute(object? parameter)
        {
            // Implement your logic here
        }

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged();
        }
    }
}
