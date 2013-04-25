using OreoMvvm.ViewModel;
using OreoMvvm.Wizard.Helper;

namespace OreoMvvm.Wizard.ViewModels
{
    /// <summary>
    /// Abstract base class for all steps shown in the wizard.
    /// </summary>
    public abstract class WizardStepViewModelBase<WizardBusinessObject> : BaseViewModel
    {
        private readonly WizardBusinessObject _businessObject;
        private bool _isCurrentStep;
        private readonly BinaryDecisionHelper _binaryDecisionHelper;

        public WizardBusinessObject BusinessObject
        {
            get { return _businessObject; }
        }

        public RouteOptionGroupViewModel<bool> BinaryDecisionGroup
        {
            get
            {
                return _binaryDecisionHelper.BinaryDecisionGroup;
            }
        }

        public abstract string DisplayName { get; }

        public bool IsCurrentStep
        {
            get { return _isCurrentStep; }
            set
            {
                if (value == _isCurrentStep)
                {
                    return;
                }
                _isCurrentStep = value;
                this.NotifyPropertyChanged(() => this.IsCurrentStep);
            }
        }

        protected WizardStepViewModelBase(WizardBusinessObject businessObject)
        {
            _businessObject = businessObject;
            _binaryDecisionHelper = new BinaryDecisionHelper();
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>An object that may modify the route</returns>
        public virtual RouteModifier OnNext()
        {
            // Must be virtual (as opposed to abstract) so descendants aren't forced to implement.
            return null;
        }

        /// <summary>
        /// For when yous need to set up some values that can't be directly bound to UI elements.
        /// </summary>
        public virtual void BeforeShow()
        {
        }

        /// <summary>
        /// Run an Async Operation before go to the next step
        /// </summary>
        /// <returns></returns>
        public virtual bool RunOnNextAsyncOperations()
        {
            return false;
        }

        /// <summary>
        /// Returns true if the user has filled in this step properly
        /// and the wizard should allow the user to progress to the 
        /// next step in the workflow.
        /// </summary>
        public abstract bool IsValid();

        public object ParentViewModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="displayName">Can't be empty string or the radio buttons won't work as a group</param>
        /// <param name="defaultSelection">Pass value to have a selection set when step comes into view</param>
        public void ConfigureBinaryDecision(string displayName = "_", bool? defaultSelection = null)
        {
            _binaryDecisionHelper.ConfigureBinaryDecision(displayName, defaultSelection);
        }

        public bool GetValueOfBinaryDecision()
        {
            return _binaryDecisionHelper.GetValueOfBinaryDecision();
        }

        public bool BinaryDecisionHasBeenMade()
        {
            return _binaryDecisionHelper.BinaryDecisionHasBeenMade();
        }
    }
}
