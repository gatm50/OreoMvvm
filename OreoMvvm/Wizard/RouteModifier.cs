using System;
using System.Collections.Generic;

namespace OreoMvvm.Wizard
{
    public class RouteModifier
    {
        public List<Type> ExcludeViewTypes { get; set; }
        public List<Type> IncludeViewTypes { get; set; }

        public void AddToExcludeViews( List<Type> viewTypes )
        {
            if ( ExcludeViewTypes == null )
                ExcludeViewTypes = viewTypes;
            else
                ExcludeViewTypes.AddRange( viewTypes );
        }

        public void AddToIncludeViews( List<Type> viewTypes )
        {
            if ( IncludeViewTypes == null )
                IncludeViewTypes = viewTypes;
            else
                IncludeViewTypes.AddRange( viewTypes );
        }
    }
}
