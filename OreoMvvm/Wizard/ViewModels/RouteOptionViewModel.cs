
namespace OreoMvvm.Wizard.ViewModels
{
    /// <summary>
    /// WizardViewModel reads these to determine overall workflow.  It doesn't care about the type param in RouteOptionViewModel below, or anything
    /// else but these two properties.
    /// UPDATE::: Wizard flow is now determined by the RouteModifier object returned from the OnNext method of each step view model.
    /// The main remaining use of this guy is when you have a step that simply asks a yes/no question.  See BinaryDecisionHelper.
    /// </summary>
    public interface IRouteOption
    {
        bool IsSelected { get; set; }
    }

    public class RouteOptionViewModel<TValue> : OptionViewModel<TValue>, IRouteOption
    {
        public RouteOptionViewModel(TValue value) : base(value) { }
        public RouteOptionViewModel(TValue value, int sortValue) : base(value, sortValue) { }
    }
}
