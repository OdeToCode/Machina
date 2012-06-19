using Xunit;

namespace Machina.Tests.SimpleVendingMachine
{
    public class VendingMachineTests
    {
        private VendingMachine _machine;

        public VendingMachineTests()
        {
            _machine = VendingMachine.Create();
        }

        [Fact] 
        public void VendingMachine_Starts_In_Waiting_State()
        {
            Assert.Equal(VendingState.Waiting, _machine.State);
        }

        [Fact]
        public void VendingMachine_Transitions_To_Paid_When_Pay_Invoked()
        {
            _machine.Pay();
            Assert.Equal(VendingState.Paid, _machine.State);
        }

        [Fact]
        public void VendingMachine_Transitions_Back_To_Waiting_After_Select()
        {
            _machine.Pay();
            _machine.Select();
            Assert.Equal(VendingState.Waiting, _machine.State);
        }        

        [Fact]
        public void Raises_Changing_Event()
        {
            var raised = false;
            
            _machine.StateChanging += (s, e) => raised = true;
            _machine.Pay();
            
            Assert.True(raised);
        }

        [Fact]
        public void Raises_Changed_Event()
        {
            var raised = false;

            _machine.StateChanged += (s, e) => raised = true;
            _machine.Pay();

            Assert.True(raised);
        }
    }
}
