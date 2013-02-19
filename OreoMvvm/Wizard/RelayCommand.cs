using System;
using System.Diagnostics;
using System.Windows.Input;

namespace OreoMvvm.Wizard
{
    /// <summary>
    /// Commands whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the CanExecute
    /// method is 'true'.
    /// </summary>
    internal class RelayCommand : ICommand
    {

        readonly Action _execute;
        readonly Func<bool> _canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand( Action execute )
            : this( execute, null )
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand( Action execute, Func<bool> canExecute )
        {
            if ( execute == null )
                throw new ArgumentNullException( "execute" );

            _execute = execute;
            _canExecute = canExecute;
        }

        //#region ICommand Members
        [DebuggerStepThrough]
        public bool CanExecute( object parameter )
        {
            return _canExecute == null ? true : _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if ( _canExecute != null )
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if ( _canExecute != null )
                    CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute( object parameter )
        {
            _execute();
        }

    }

}
