using System;
using Machina.StateMachine;

namespace Machina.Tests.SimpleVendingMachine
{
    public enum VendingState
    {
        Waiting,
        Paid
    }

    public class VendingMachine 
    {
        public virtual void Pay() {}
        public virtual void Select() {}    
        
        public static VendingMachine Create()
        {
            return StateMachineBuilder<VendingMachine, VendingState>.Build
                ((b,m) =>
                    b.CreateTransition(
                            when: VendingState.Waiting,
                            then: vm => vm.Pay(),
                            goesTo: VendingState.Paid)
                     .CreateTransition(
                            when: VendingState.Paid,
                            then: vm => vm.Select(),
                            goesTo: VendingState.Waiting)
                     .Setup(getState: () => m.State,
                            setState: s => m.State = s,
                            onStateChanging: a => m.StateChanging(m, a),
                            onStateChanged: a=> m.StateChanged(m, a)                            
                    )
                );
        }

        public VendingState State { get; protected set; }
        public event EventHandler<StateChangeEventArgs<VendingState>> StateChanging = delegate { };
        public event EventHandler<StateChangeEventArgs<VendingState>> StateChanged = delegate { }; 
    }
}