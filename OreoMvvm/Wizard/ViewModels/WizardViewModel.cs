using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using OreoMvvm.ViewModel;

namespace OreoMvvm.Wizard.ViewModels
{
    public interface IWizardViewModel
    {
        ICommand MoveNextCommand { get; }
        ICommand MovePreviousCommand { get; }
        ICommand FinishWizardCommand { get; }
        bool IsOnLastStep { get; }
    }

    public delegate void NextEventHandler( object currentStep );

    /// <summary>
    /// The main ViewModel class for the wizard.  This class contains the various steps shown in the workflow and provides navigation between the steps.
    /// </summary>
    /// <typeparam name="WizardBusinessObject">The object the wizard models.  Must have parameterless constructor because we will create it within.</typeparam>
    public class WizardViewModel<WizardBusinessObject> : BaseViewModel, IWizardViewModel where WizardBusinessObject : IWizardBusinessObject, new()
    {
        private WizardBusinessObject _businessObject;
        private readonly StepManager<WizardBusinessObject> _stepManager;
        private RelayCommand _moveNextCommand;
        private RelayCommand _movePreviousCommand;
        private RelayCommand _finishWizardCommand;
        private RelayCommand _cancelCommand;

        /// <summary>
        /// Referenced only in xaml
        /// </summary>
        public ReadOnlyCollection<CompleteStep<WizardBusinessObject>> Steps
        {
            get
            {
                return new ReadOnlyCollection<CompleteStep<WizardBusinessObject>>( _stepManager.Steps );
            }
        }

        /// <summary>
        /// Returns the step ViewModel that the user is currently viewing.
        /// </summary>
        /// <summary>
        /// Returns the business object the wizard is building.  If this returns null, the user cancelled.
        /// </summary>
        public WizardBusinessObject BusinessObject
        {
            get { return _businessObject; }
            set { _businessObject = value; }
        }

        public LinkedListNode<CompleteStep<WizardBusinessObject>> CurrentLinkedListStep
        {
            get { return _stepManager.CurrentLinkedListStep; }
            private set
            {
                if ( value == _stepManager.CurrentLinkedListStep )
                    return;

                this.ActionsOnCurrentLinkedListStep( value );

                this.NotifyPropertyChanged(() => this.CurrentLinkedListStep);
                this.NotifyPropertyChanged(() => this.IsOnLastStep);
            }
        }

        public WizardViewModel()
        {
            _stepManager = new StepManager<WizardBusinessObject>();
            _businessObject = Activator.CreateInstance<WizardBusinessObject>();
        }

        public void ProvideSteps( List<CompleteStep<WizardBusinessObject>> steps )
        {
            _stepManager.ProvideSteps( steps );
            this.ActionsOnCurrentLinkedListStep( _stepManager.FirstStep );
        }

        void Cancel()
        {
            _businessObject.Cancel();
        }

        /// <summary>
        /// Returns the command which, when executed, cancels the order and causes the Wizard to be removed from the user interface.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if ( _cancelCommand == null )
                    _cancelCommand = new RelayCommand( () => this.Cancel() );

                return _cancelCommand;
            }
        }

        /// <summary>
        /// Returns the command which, when executed, causes the CurrentLinkedListStep
        /// property to reference the previous step in the workflow.
        /// </summary>
        public ICommand MovePreviousCommand
        {
            get
            {
                if ( _movePreviousCommand == null )
                    _movePreviousCommand = new RelayCommand( () => this.MoveToPreviousStep(), () => this.CanMoveToPreviousStep );

                return _movePreviousCommand;
            }
        }

        /// <summary>
        /// Returns the command which, when executed, causes the CurrentLinkedListStep property to reference the next step in the workflow.  If the user
        /// is viewing the last step in the workflow, this causes the Wizard to finish and be removed from the user interface.
        /// </summary>
        public ICommand MoveNextCommand
        {
            get
            {
                if ( _moveNextCommand == null )
                    _moveNextCommand = new RelayCommand( () => this.MoveToNextStep(), () => this.CanMoveToNextStep );

                return _moveNextCommand;
            }
        }

        bool CanMoveToPreviousStep
        {
            get { return this.CurrentLinkedListStep.Previous != null; }
        }

        void MoveToPreviousStep()
        {
            if ( CanMoveToPreviousStep )
            {
                this.CurrentLinkedListStep = CurrentLinkedListStep.Previous;
                //CurrentLinkedListStep.Value.ViewModel.BeforeShow();
            }
        }

        bool CanMoveToNextStep
        {
            get
            {
                var step = this.CurrentLinkedListStep;
                return ( step != null ) && ( step.Value.ViewModel.IsValid() ) && ( step.Next != null );
            }
        }

        /// <summary>
        /// Note that currently, the step OnNext handler is only called when moving next; not when moving previous.
        /// </summary>
        void MoveToNextStep()
        {
            if (CanMoveToNextStep)
            {
                if (CurrentLinkedListStep.Value.ViewModel.RunOnNextAsyncOperations())
                    return;

                this.MoveToNextStepSyncOperations();
            }
        }

        public void MoveToNextStepSyncOperations()
        {
            _stepManager.ReworkListBasedOn(CurrentLinkedListStep.Value.ViewModel.OnNext());
            CurrentLinkedListStep = CurrentLinkedListStep.Next;
            CurrentLinkedListStep.Value.Visited = true;
        }

        private void ActionsOnCurrentLinkedListStep( LinkedListNode<CompleteStep<WizardBusinessObject>> step )
        {
            if ( this.CurrentLinkedListStep != null )
                this.CurrentLinkedListStep.Value.ViewModel.IsCurrentStep = false;

            _stepManager.CurrentLinkedListStep = step;

            if ( step != null )
            {
                step.Value.ViewModel.IsCurrentStep = true;
                step.Value.ViewModel.BeforeShow();
            }
        }

        /// <summary>
        /// Returns true if the user is currently viewing the last step in the workflow.
        /// </summary>
        public bool IsOnLastStep
        {
            get { return this.CurrentLinkedListStep.Next == null; }
        }

        bool CanFinishWizard
        {
            get
            {
                if (this.IsOnLastStep && CurrentLinkedListStep.Value.ViewModel.IsValid())
                    return true;
                else
                    return false;
            }
        }

        public ICommand FinishWizardCommand
        {
            get
            {
                if (_finishWizardCommand == null)
                {
                    _finishWizardCommand = new RelayCommand(() => this.FinishWizard(), () => this.CanFinishWizard);
                }
                return _finishWizardCommand;
            }
        }

        void FinishWizard()
        {
            if (this.CanFinishWizard)
                return;
        }
    }
}
