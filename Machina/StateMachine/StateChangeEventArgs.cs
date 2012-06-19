using System;

namespace Machina.StateMachine
{
    public class StateChangeEventArgs<TEnum> : EventArgs
    {
        public TEnum OldState { get; set; }
        public TEnum NewState { get; set; }
    }
}