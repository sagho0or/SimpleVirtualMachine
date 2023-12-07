namespace SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;

/// <summary>
/// Implements the SML Add instruction
/// Retrieves two integer values from the top of the
/// stack and adds them together, leaving
/// the result on the stack
/// </summary>
public class Bgrint : BaseInstructionWithOperand
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
        if (Operands.Length != 2)
        {
            throw new SvmRuntimeException("BgrInt instruction requires two operands: [integer value] and [branch location label]");
        }

        if (VirtualMachine.Stack.Count < 1)
        {
            throw new SvmRuntimeException("Stack underflow error - the stack does not contain enough elements for BgrInt operation");
        }

        var topOfStack = VirtualMachine.Stack.Peek();
        if (!(topOfStack is int topValue))
        {
            throw new SvmRuntimeException("Top of the stack is not an integer, BgrInt operation requires an integer at the top of the stack");
        }

        int valueToCompare = int.Parse(Operands[0]);
        string branchLocation = Operands[1];

        if (topValue > valueToCompare)
        {
            if (VirtualMachine.LabelMap.TryGetValue(branchLocation, out int targetIndex))
            {
                VirtualMachine.ProgramCounter = targetIndex;
                ModifiedProgramCounter = true; 
            }
            else
            {
                throw new SvmRuntimeException($"Label '{branchLocation}' not found for BgrInt operation");
            }
        }
    }
    #endregion
}

