using System;
using OreoMvvm.ViewModel;
using OreoMvvm.Wizard.ViewModels;

namespace OreoMvvm.Wizard
{
    /// <summary>
    /// For our StepTemplateConverter
    /// </summary>
    public interface IProvideViewType
    {
        Type ViewType { get; }
    }

    public class CompleteStep<WizardBusinessObject> : BaseViewModel, IProvideViewType
    {

        private bool _relevant = true;
        private bool _visited;

        public bool Relevant
        {
            get
            {
                return _relevant;
            }
            set
            {
                if (_relevant != value)
                {
                    _relevant = value;
                    this.NotifyPropertyChanged(() => this.Relevant);
                }
            }
        }

        public bool Visited
        {
            get
            {
                return _visited;
            }
            set
            {
                if (_visited != value)
                {
                    _visited = value;
                    this.NotifyPropertyChanged(() => this.Visited);
                }
            }
        }

        public WizardStepViewModelBase<WizardBusinessObject> ViewModel { get; set; }

        /// <summary>
        /// The class type of the actual xaml view to be used for this step
        /// </summary>
        public Type ViewType { get; set; }
    }
}
