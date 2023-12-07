using Microsoft.VisualStudio.TestTools.UnitTesting;
using SVM.VirtualMachine;
using SVM.SimpleMachineLanguage;

namespace UnitTest
{
    [TestClass]
    public class IncrementDecrementTests
    {
        [TestMethod]
        public void Increment()
        {
            var virtualMachine = new MockVirtualMachine();
            virtualMachine.SetInitialStackData(10, 20, 30);
            var initialStackCount = virtualMachine.Stack.Count;
            var incrInstruction = new Incr();

            incrInstruction.VirtualMachine = virtualMachine;
            incrInstruction.Run();

            var incrResult = incrInstruction.VirtualMachine.Stack.Peek();
            Assert.AreEqual(31 , incrResult);
        }

        [TestMethod]
        public void Decrement()
        {
            var virtualMachine = new MockVirtualMachine();
            virtualMachine.SetInitialStackData(10, 20, 30);
            var initialStackCount = virtualMachine.Stack.Count;
            var decrInstruction = new Decr();

            decrInstruction.VirtualMachine = virtualMachine;
            decrInstruction.Run();

            var decrResult = decrInstruction.VirtualMachine.Stack.Peek();
            Assert.AreEqual( 29 , decrResult);
            
        }

        [TestMethod]
        public void BgrInt()
        {
            var virtualMachine = new MockVirtualMachine();
            virtualMachine.SetInitialStackData(5);
            virtualMachine.LabelMap.Add("testLabel", 2);
            var bgrIntInstruction = new Bgrint { Operands = new string[] { "3", "testLabel" } };

            bgrIntInstruction.VirtualMachine = virtualMachine;
            bgrIntInstruction.Run();

            Assert.AreEqual(2, virtualMachine.ProgramCounter); 
        }

        [TestMethod]
        public void BltInt()
        {
            var virtualMachine = new MockVirtualMachine();
            virtualMachine.SetInitialStackData(2); 
            virtualMachine.LabelMap.Add("testLabel2", 5);
            var bltIntInstruction = new Bltint { Operands = new string[] { "3", "testLabel2" } };

            bltIntInstruction.VirtualMachine = virtualMachine;
            bltIntInstruction.Run();

            Assert.AreEqual(5, virtualMachine.ProgramCounter);
        }

        [TestMethod]
        public void EquInt()
        {
            var virtualMachine = new MockVirtualMachine();
            virtualMachine.SetInitialStackData(3); 
            virtualMachine.LabelMap.Add("testLabel", 2);
            var equIntInstruction = new EquInt { Operands = new string[] { "3", "testLabel" } };

            equIntInstruction.VirtualMachine = virtualMachine;
            equIntInstruction.Run();

            Assert.AreEqual(2, virtualMachine.ProgramCounter);
        }

        [TestMethod]
        public void NotEqu()
        {
            var virtualMachine = new MockVirtualMachine();
            virtualMachine.SetInitialStackData(5, 3); 
            virtualMachine.LabelMap.Add("testLabel", 2);
            var notEquInstruction = new Notqu { Operands = new string[] { "testLabel" } };

            notEquInstruction.VirtualMachine = virtualMachine;
            notEquInstruction.Run();

            Assert.AreEqual(2, virtualMachine.ProgramCounter);
        }
    }

    public class MockVirtualMachine : IVirtualMachine
    {
        private readonly System.Collections.Stack stack = new System.Collections.Stack();

        public System.Collections.Stack Stack => stack;

        public int ProgramCounter { get; set; }

        public Dictionary<string, int> LabelMap { get; private set; } = new Dictionary<string, int>();
        public void Run() { }

        public void ParseInstruction(string instruction, int lineNumber) { }

        public void SetInitialStackData(params object[] data)
        {
            foreach (var item in data)
            {
                stack.Push(item);
            }
        }

        public MockVirtualMachine()
        {
            LabelMap = new Dictionary<string, int>();
        }
    }
}

