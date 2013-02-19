using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace OreoMvvm.Wizard
{
    public class StepManager<WizardBusinessObject>
    {
        private LinkedListNode<CompleteStep<WizardBusinessObject>> _currentLinkedListStep;
        private bool _reconfiguringRoute;
        private List<CompleteStep<WizardBusinessObject>> _steps;
        private LinkedList<CompleteStep<WizardBusinessObject>> _linkedSteps;

        public LinkedListNode<CompleteStep<WizardBusinessObject>> CurrentLinkedListStep
        {
            get
            {
                return _currentLinkedListStep;
            }
            set
            {
                _currentLinkedListStep = value;
                if ( ( _linkedSteps.First == _currentLinkedListStep ) && !_reconfiguringRoute )
                    this.ResetRoute();
            }
        }

        public List<CompleteStep<WizardBusinessObject>> Steps { get { return _steps; } }

        public LinkedListNode<CompleteStep<WizardBusinessObject>> FirstStep
        {
            get
            {
                return _linkedSteps == null ? null : _linkedSteps.First;
            }
        }

        public void ProvideSteps( List<CompleteStep<WizardBusinessObject>> steps )
        {
            _steps = steps;
            _linkedSteps = new LinkedList<CompleteStep<WizardBusinessObject>>( _steps );
            CurrentLinkedListStep = _linkedSteps.First;
        }

        public void ReworkListBasedOn( RouteModifier routeModifier )
        {
            if ( routeModifier == null )
                return;

            _reconfiguringRoute = true;
            this.ReorganizeLinkedList( routeModifier );
            this.ResetListRelevancy();
            _reconfiguringRoute = false;
        }

        /// <summary>
        /// Each step in the wizard may modify the route, but it's assumed that if the user goes back to step one, the route initializes back to the way it
        /// was when it was created.
        /// </summary>
        private void ResetRoute()
        {
            var allStepViewTypes = _linkedSteps.ToList().ConvertAll( s => s.ViewType );
            this.ReworkListBasedOn( new RouteModifier() { IncludeViewTypes = allStepViewTypes } );
        }

        /// <summary>
        /// At this point, if a step is in the linked list, it's relevant; if not, it's not.
        /// </summary>
        private void ResetListRelevancy()
        {
            _steps.ForEach( s => s.Relevant = false );
            var linkedStep = _linkedSteps.First;
            while ( linkedStep != null )
            {
                linkedStep.Value.Relevant = true;
                linkedStep = linkedStep.Next;
            }
        }

        /// <summary>
        /// Re-create the linked list to reflect the new "workflow."
        /// </summary>
        /// <param name="nextStep"></param>
        private void ReorganizeLinkedList( RouteModifier rm )
        {
            var cacheCurrentStep = CurrentLinkedListStep.Value;
            var newSubList = CreateNewStepList( rm );

            /// Re-create linked list.
            _linkedSteps = new LinkedList<CompleteStep<WizardBusinessObject>>( newSubList );
            this.ResetCurrentLinkedListStepTo( cacheCurrentStep );
        }

        private List<CompleteStep<WizardBusinessObject>> CreateNewStepList( RouteModifier routeModifier )
        {
            var result = new List<CompleteStep<WizardBusinessObject>>( _linkedSteps );

            this.EnsureNotModifyingCurrentStep( routeModifier );

            if ( routeModifier.ExcludeViewTypes != null )
                routeModifier.ExcludeViewTypes.ForEach( t => result.RemoveAll( step => step.ViewType.Equals( t ) ) );
            
            if ( routeModifier.IncludeViewTypes != null )
                AddBack( result, routeModifier.IncludeViewTypes );

            return result;
        }

        private void EnsureNotModifyingCurrentStep( RouteModifier routeModifier )
        {
            Func<Type, bool> currentStepCondition = t => t == CurrentLinkedListStep.Value.ViewType;
            
            if ( routeModifier.ExcludeViewTypes != null )
                Contract.Ensures( routeModifier.ExcludeViewTypes.FirstOrDefault( currentStepCondition ) == null );
            
            if ( routeModifier.IncludeViewTypes != null )
                Contract.Ensures( routeModifier.IncludeViewTypes.FirstOrDefault( currentStepCondition ) == null );
        }

        /// <summary>
        /// OMG, if the user chooses an option that changes the route through the wizard, then goes back and chooses a different option,
        /// we need to add the appropriate step(s) back into the workflow.
        /// </summary>
        /// <param name="workingStepList"></param>
        /// <param name="viewTypes"></param>
        private void AddBack( List<CompleteStep<WizardBusinessObject>> workingStepList, List<Type> viewTypes )
        {
            foreach ( var vt in viewTypes )
            {
                // Find the step to add back in the main list of steps.
                var stepToAddBack = _steps.Where( s => s.ViewType == vt ).FirstOrDefault();
                if ( !workingStepList.Contains( stepToAddBack ) )
                {
                    // Re-insert the step into our working list (which will become the wizard's new linked list).
                    if ( stepToAddBack != null )
                    {
                        int indexOfStepToAddBack = _steps.IndexOf( stepToAddBack );
                        // If it belongs at the head of the list, add it there.
                        if ( indexOfStepToAddBack == 0 )
                        {
                            workingStepList.Insert( 0, stepToAddBack );
                            continue;
                        }
                        else
                        {
                            /// Otherwise we have to find the previous step in the main list, find that step in our working list and add in
                            /// the step after that step.
                            var stepReinserted = false;
                            var countOfStepsToPreviousFoundStep = 1;
                            while ( !stepReinserted )
                            {
                                var previousStep = _steps[indexOfStepToAddBack - countOfStepsToPreviousFoundStep];
                                for ( int i = 0; i < workingStepList.Count; i++ )
                                {
                                    if ( workingStepList[i].ViewType == previousStep.ViewType )
                                    {
                                        workingStepList.Insert( i + 1, stepToAddBack );
                                        stepReinserted = true;
                                    }
                                }
                                // The previous step wasn't found; continue to the next previous step.
                                countOfStepsToPreviousFoundStep++;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Must maintain the current step reference (this re-creating of the linked list happens when the user makes a selection on
        /// the current step).
        /// After recreating the list, our CurrentLinkedListStep reference would be referring to an item in the old linked list.
        /// </summary>
        /// <param name="cacheCurrentStep"></param>
        private void ResetCurrentLinkedListStepTo( CompleteStep<WizardBusinessObject> cacheCurrentStep )
        {
            CurrentLinkedListStep = _linkedSteps.First;
            while ( CurrentLinkedListStep.Value != cacheCurrentStep )
            {
                if ( CurrentLinkedListStep.Next == null )
                    throw new Exception( "Error resetting current step after reorganizing steps." );

                CurrentLinkedListStep = CurrentLinkedListStep.Next;
            }
        }

    }

}
