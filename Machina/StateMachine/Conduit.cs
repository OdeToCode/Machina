using System;
using System.Diagnostics.Contracts;

namespace Machina.StateMachine
{
    internal class Conduit<TEnum>
    {
        private readonly Func<TEnum> _getState;
        private readonly Action<TEnum> _setState;
        private readonly Action<StateChangeEventArgs<TEnum>> _onStateChanging;
        private readonly Action<StateChangeEventArgs<TEnum>> _onStateChanged;

        public Conduit(Func<TEnum> getState,
                                   Action<TEnum> setState,                                   
                                   Action<StateChangeEventArgs<TEnum>> onStateChanging = null,
                                   Action<StateChangeEventArgs<TEnum>> onStateChanged = null)
        {

            Contract.Requires(getState != null);
            Contract.Requires(setState != null);
            _getState = getState;
            _setState = setState;
            _onStateChanging = onStateChanging;
            _onStateChanged = onStateChanged;
        }

        public TEnum GetState()
        {
            return _getState();
        }

        public void SetState(TEnum value)
        {
            _setState(value);
        }

        public void OnStateChanging(StateChangeEventArgs<TEnum> args)
        {
            if(_onStateChanging != null)
            {
                _onStateChanging(args);
            }
        }

        public void OnStateChanged(StateChangeEventArgs<TEnum> args)
        {
            if(_onStateChanged != null)
            {
                _onStateChanged(args);
            }
        }
    }
}