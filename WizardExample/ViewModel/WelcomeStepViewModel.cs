using OreoMvvm.Wizard.ViewModels;
using WizardExample.Model;

namespace WizardExample.ViewModel
{
    class WelcomeStepViewModel : WizardStepViewModelBase<GenericModel>
    {
        public WelcomeStepViewModel(GenericModel model)
            : base(model)
        {
        }

        public override string DisplayName
        {
            get { return "Welcome"; }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
