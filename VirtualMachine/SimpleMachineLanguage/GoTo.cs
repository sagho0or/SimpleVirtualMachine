namespace SVM.SimpleMachineLanguage;
using SVM.VirtualMachine;

/// <summary>
/// Implements the SML Add instruction
/// Retrieves two integer values from the top of the
/// stack and adds them together, leaving
/// the result on the stack
/// </summary>
public class Goto : BaseInstructionWithOperand
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
        if (Operands.Length != 1)
        {
            throw new SvmRuntimeException("Goto instruction requires one operand: [label]");
        }

        string label = Operands[0];

        if (VirtualMachine.LabelMap.TryGetValue(label, out int targetIndex))
        {
            VirtualMachine.ProgramCounter = targetIndex;
            ModifiedProgramCounter = true;
        }
        else
        {
            throw new SvmRuntimeException($"Label '{label}' not found for Goto operation");
        }
    }
    #endregion
}

