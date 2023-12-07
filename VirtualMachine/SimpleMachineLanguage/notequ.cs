namespace SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;

/// <summary>
/// Implements the SML Add instruction
/// Retrieves two integer values from the top of the
/// stack and adds them together, leaving
/// the result on the stack
/// </summary>
public class Notqu : BaseInstructionWithOperand
{
    #region Constants
    #endregion

    #region Fields
    #endregion

    #region Constructors
    #endregion

    #region Properties
    #endregion

    #region Public methods
    #endregion

    #region Non-public methods
    #endregion

    #region System.Object overrides


    #endregion

    #region IInstruction Members

    public override void Run()
    {
        if (VirtualMachine.Stack.Count < 2)
        {
            throw new SvmRuntimeException("Stack underflow error - the stack does not contain enough elements for Notqu operation");
        }

        object firstValue = VirtualMachine.Stack.Pop();
        object secondValue = VirtualMachine.Stack.Pop();

        // Push back the values after comparison
        VirtualMachine.Stack.Push(secondValue);
        VirtualMachine.Stack.Push(firstValue);

        if (!firstValue.Equals(secondValue))
        {
            string branchLocation = Operands[0];
            if (VirtualMachine.LabelMap.TryGetValue(branchLocation, out int targetIndex))
            {
                VirtualMachine.ProgramCounter = targetIndex;
                ModifiedProgramCounter = true;
            }
            else
            {
                throw new SvmRuntimeException($"Label '{branchLocation}' not found for Notqu operation");
            }
        }
    }
    #endregion
}

