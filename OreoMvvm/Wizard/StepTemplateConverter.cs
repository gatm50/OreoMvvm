using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace OreoMvvm.Wizard
{
    /// <summary>
    /// Used by the main wizard view (WizardView.xaml).
    /// When a step changes, Convert is called and passed the current CompleteStep object.  This then passed back the view for that step as a DataTemplate.
    /// </summary>
    internal class StepTemplateConverter : MarkupExtension, IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            var viewType = ( (IProvideViewType)value ).ViewType;
            return new DataTemplate( /*type passed in here does not have anything to do with DataContext*/ ) { VisualTree = new FrameworkElementFactory( viewType ) };
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            throw new InvalidOperationException( string.Format( "{0} can only be used OneWay.", this.GetType().Name ) );
        }

        public override object ProvideValue( IServiceProvider serviceProvider )
        {
            return this;
        }
    }
}
