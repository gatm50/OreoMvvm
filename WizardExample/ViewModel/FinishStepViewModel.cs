using OreoMvvm.Wizard.ViewModels;
using WizardExample.Model;

namespace WizardExample.ViewModel
{
    class FinishStepViewModel : WizardStepViewModelBase<GenericModel>
    {
        public FinishStepViewModel(GenericModel model)
            : base(model)
        {
        }

        public override string DisplayName
        {
            get { return "Finish"; }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
