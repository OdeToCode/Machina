using System;
using Machina.StateMachine;
using Xunit;

namespace Machina.Tests.ErrorConditions
{
    public enum Simple
    {
        One,
        Two
    }

    public class SimpleMachine
    {
        public virtual void Foo()
        {
            
        }

        public virtual void Bar()
        {
            
        }

        public Simple State { get; set; }

        public static SimpleMachine Create()
        {
            var machine = StateMachineBuilder<SimpleMachine, Simple>.Build(
                (b,m) =>
                    b.CreateTransition(
                            when: Simple.One,
                            then: vm => vm.Foo(),
                            goesTo: Simple.Two)                     
                     .Setup(getState: () => m.State,
                            setState: s => m.State = s)                    
                );
            return machine;
        }
    }


    public class IllegalTransition
    {
        [Fact]
        public void VendingMachine_Throws_On_Illegal_Transition()
        {
            var machine = SimpleMachine.Create();
            machine.Foo();
            Assert.Throws<InvalidOperationException>(() => machine.Bar());
        }
         
    }
}