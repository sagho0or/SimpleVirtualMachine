namespace SVM.SimpleMachineLanguage;

/// <summary>
/// Implements the SML Decr  instruction
/// Decrements the integer value stored on top of the stack, 
/// leaving the result on the stack
/// </summary>
public class Decr : BaseInstruction
{
    #region TASK 3 - TO BE IMPLEMENTED BY THE STUDENT
    #endregion

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
    /// <summary>
    /// Determines whether the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>.
    /// </summary>
    /// <param name="obj">The <see cref="System.Object">Object</see> to compare with the current <see cref="System.Object">Object</see>.</param>
    /// <returns><b>true</b> if the specified <see cref="System.Object">Object</see> is equal to the current <see cref="System.Object">Object</see>; otherwise, <b>false</b>.</returns>
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    /// <summary>
    /// Serves as a hash function for this type.
    /// </summary>
    /// <returns>A hash code for the current <see cref="System.Object">Object</see>.</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    /// <summary>
    /// Returns a <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.
    /// </summary>
    /// <returns>A <see cref="System.String">String</see> that represents the current <see cref="System.Object">Object</see>.</returns>
    public override string ToString()
    {
        return base.ToString();
    }
    #endregion

    #region IInstruction Members

    public override void Run()
    {
        try
        {
            if (VirtualMachine.Stack.Count < 1)
            {
                throw new SvmRuntimeException(String.Format(BaseInstruction.StackUnderflowMessage,
                                                this.ToString(), VirtualMachine.ProgramCounter));
            }

            int op1 = (int)VirtualMachine.Stack.Pop();
            VirtualMachine.Stack.Push(op1 - 1);
        }
        catch (InvalidCastException)
        {
            throw new SvmRuntimeException(String.Format(BaseInstruction.OperandOfWrongTypeMessage,
                                            this.ToString(), VirtualMachine.ProgramCounter));
        }
    }

    #endregion
}
