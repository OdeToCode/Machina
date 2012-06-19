using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;

namespace Machina.StateMachine
{
    internal class StateManager<TEnum> : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var transition = FindTransition(invocation);            
            invocation.Proceed();
            DoStateChange(transition);
        }

        private void DoStateChange(Transition<TEnum> transition)
        {
            var args = new StateChangeEventArgs<TEnum>() {OldState = _conduit.GetState(), NewState = transition.GoesTo};
            _conduit.OnStateChanging(args);
            _conduit.SetState(transition.GoesTo);
            _conduit.OnStateChanged(args);
        }

        public void SetupConduit(Conduit<TEnum> conduit)
        {
            _conduit = conduit;
        }

        public void AddTransition(Transition<TEnum> transition)
        {
            _transitions.Add(transition);
        }

        public void Validate()
        {
            if(_conduit == null)
            {
                throw new InvalidOperationException("You did not call the Setup method on StateMachineBuilder when building the state machine.");
            }
            if(!_transitions.Any())
            {
                throw new InvalidOperationException("You did not call Transition on the StateMachineBuilder when building the state machine.");
            }
        }

        private Transition<TEnum> FindTransition(IInvocation invocation)
        {
            var transition =  _transitions.FirstOrDefault(t => t.Then == invocation.Method.Name && t.When.Equals(_conduit.GetState()));
            if(transition == null)
            {
                var message = String.Format("There was no transition found when method {0} is called in state {1}",
                                            invocation.Method.Name, _conduit.GetState());
                throw new InvalidOperationException(message);
            }
            return transition;
        }

        Conduit<TEnum> _conduit;
        readonly List<Transition<TEnum>> _transitions = new List<Transition<TEnum>>();
    }
}