namespace SVM.VirtualMachine;

/// <summary>
/// Defines the interface contract for all Simple 
/// Virtual Machine 
/// </summary>
public interface IVirtualMachine
{
    Stack Stack { get; } 
    void Run();

    int ProgramCounter { get; set; }

    Dictionary<string, int> LabelMap { get; }
    void ParseInstruction(string instruction, int lineNumber);

}

