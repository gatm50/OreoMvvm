using System;
using OreoMvvm.Wizard.ViewModels;
using WizardExample.Model;

namespace WizardExample.ViewModel
{
    class SecondStepViewModel : WizardStepViewModelBase<GenericModel>
    {
        public SecondStepViewModel(GenericModel model)
            : base(model)
        {
        }

        public override string DisplayName
        {
            get { return "Second"; }
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}
