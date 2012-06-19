using System;
using Machina.StateMachine;
using Xunit;

namespace Machina.Tests.ErrorConditions
{
    public enum NoState
    {  
        None
    }

    public class NoTransitionMachine
    {
        public static NoTransitionMachine Create()
        {
            return StateMachineBuilder<NoTransitionMachine, NoState>.Build
                ((b, m) => b.Setup(s => { }, ()=> NoState.None));
        }
    }

    public class NoSetupMachine
    {
        public virtual void Foo(){}

        public static NoSetupMachine Create()
        {
            return StateMachineBuilder<NoSetupMachine, NoState>.Build
                ((b, m) =>
                    b.CreateTransition(
                            when: NoState.None,
                            then: nsm => nsm.Foo(),
                            goesTo: NoState.None));
        }
    }

    public class IncompleteSetup
    {
        [Fact]
        public void Machine_With_No_Setup_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => NoSetupMachine.Create());
        }

        [Fact]
        public void Machine_With_No_Transitions_Throws()
        {
            Assert.Throws<InvalidOperationException>(() => NoTransitionMachine.Create());
        }
    }
}