using System;
using System.Linq.Expressions;
using Castle.DynamicProxy;

namespace Machina.StateMachine
{
    public class StateMachineBuilder<T, TEnum> : Builder where T : class, new()
    {
        private readonly T _machine;
        private readonly StateManager<TEnum> _stateManager;        

        StateMachineBuilder()
        {
            var options = new ProxyGenerationOptions{
                Hook = new MethodSelector()
            };            
            _stateManager = new StateManager<TEnum>();                        
            _machine = _generator.CreateClassProxy<T>(options, _stateManager);             
        }

        public static T Build(Action<StateMachineBuilder<T, TEnum>, T> program) 
        {            
            var builder = new StateMachineBuilder<T, TEnum>();
            program(builder, builder._machine);
            builder._stateManager.Validate();
            return builder._machine;
        }    

        public StateMachineBuilder<T,TEnum> CreateTransition(TEnum when, Expression<Action<T>> then, TEnum goesTo)
        {
            var methodCall = (MethodCallExpression)then.Body;            
            var transition = new Transition<TEnum>(when, methodCall.Method.Name, goesTo);            
            _stateManager.AddTransition(transition);            
            return this;
        }

        public StateMachineBuilder<T,TEnum> Setup(
            Action<TEnum> setState,
            Func<TEnum> getState,
            Action<StateChangeEventArgs<TEnum>> onStateChanging = null,
            Action<StateChangeEventArgs<TEnum>> onStateChanged = null)
        {
            var conduit = new Conduit<TEnum>(getState, setState, onStateChanging, onStateChanged);
            _stateManager.SetupConduit(conduit);
            return this;
        }
    }
}