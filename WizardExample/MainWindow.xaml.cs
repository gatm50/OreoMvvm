using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OreoMvvm.Wizard.ViewModels;
using System.ComponentModel;
using OreoMvvm.Wizard.Views;
using OreoMvvm.Wizard;
using WizardExample.Model;
using WizardExample.ViewModel;
using WizardExample.View;

namespace WizardExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IWizardViewModel WizardVM
        {
            get { return (IWizardViewModel)GetValue(WizardVMProperty); }
            set { SetValue(WizardVMProperty, value); }
        }
        public static readonly DependencyProperty WizardVMProperty = DependencyProperty.Register("WizardVM", typeof(IWizardViewModel), typeof(MainWindow));

        private readonly WizardView CoffeeWizardView;
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            CoffeeWizardView = GetPlainWizardView();

            wizardHost.Children.Clear();
            wizardHost.Children.Add(CoffeeWizardView);
            this.FirePropsChanged(CoffeeWizardView.DataContext as IWizardViewModel);
        }

        private void FirePropsChanged(IWizardViewModel wizardViewModel)
        {
            WizardVM = wizardViewModel;
            OnPropertyChanged("WizardVM");
        }

        private static WizardView GetPlainWizardView()
        {
            /// 1)
            /// Create a WizardViewModel passing the type of the object the wizard will model.
            /// The type it's modeling must have parameterless constructor; WizardViewModel will create it.
            var wizModel = new WizardViewModel<GenericModel>();

            /// 2)
            /// Create / provide the steps for the wizard.  See comments in the CreateSteps method.
            wizModel.ProvideSteps(CreateCoffeeSteps(wizModel.BusinessObject));

            /// 3)
            /// Create the actual wizard view / control.  Set it's DataContext to the WizardViewModel object created above.
            return new WizardView() { Height = 400, Width = 600, DataContext = wizModel };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genericModel">This is the instance created by the WizardViewModel</param>
        /// <returns></returns>
        private static List<CompleteStep<GenericModel>> CreateCoffeeSteps(GenericModel genericModel)
        {
            /// 2.1) Create a view model for each step.
            ///     Each of these descend from WizardStepViewModelBase
            var step1ViewModel = new WelcomeStepViewModel(genericModel);

            /// This ViewModel contains a RouteOptionGroupViewModel (a group of options that may alter the workflow of the wizard).
            /// See TypeSizeStepViewModel.CreateAvailableDrinkSizes.
            var step2ViewModel = new SecondStepViewModel(genericModel);
            var step3ViewModel = new FinishStepViewModel(genericModel);

            /// 2.2) Create a list of steps.
            ///     We pass the same type param (CupOfCoffee) that we passed to WizardViewModel in Button_Click above.
            return new List<CompleteStep<GenericModel>>() 
            {
                /// Each step contains a ViewModel and a View type (the type representing the actual Xaml to be shown).
                new CompleteStep<GenericModel>() { ViewModel = step1ViewModel, ViewType = typeof(WelcomeView), Visited = true },
                new CompleteStep<GenericModel>() { ViewModel = step2ViewModel, ViewType = typeof(SecondView) },
                new CompleteStep<GenericModel>() { ViewModel = step3ViewModel, ViewType = typeof(FinishView) },
            };
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
