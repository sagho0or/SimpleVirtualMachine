namespace SVM.VirtualMachine;

using SVM.SimpleMachineLanguage;

#region Using directives
using System.Reflection;
#endregion

/// <summary>
/// Utility class which generates compiles a textual representation
/// of an SML instruction into an executable instruction instance
/// </summary>
internal static class JITCompiler
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
    internal static IInstruction CompileInstruction(string opcode, bool isBreakpoint)
    {
        // First, try to find the instruction in the hardcoded switch statement
        IInstruction instruction = TryGetHardcodedInstruction(opcode, isBreakpoint);
        if (instruction != null)
            return instruction;

        // If not found, search in external DLLs
        instruction = SearchInExternalAssemblies(opcode, isBreakpoint);
        if (instruction != null)
        {
            instruction.IsBreakpoint = isBreakpoint;
            return instruction;
        }

        throw new SvmCompilationException($"Unknown opcode: {opcode}");


       
    }

    internal static IInstruction CompileInstruction(string opcode, bool isBreakpoint, params string[] operands)
    {
        IInstructionWithOperand instruction = TryGetHardcodedInstructionWithOperand(opcode, isBreakpoint, operands);

        if (instruction != null)
            return instruction;

        instruction = SearchInExternalAssembliesWithOperand(opcode, isBreakpoint, operands);
        if (instruction != null)
        {
            instruction.IsBreakpoint = isBreakpoint;
            return instruction;
        }


        throw new SvmCompilationException($"Unknown opcode: {opcode}");

        
    }
    private static IInstruction TryGetHardcodedInstruction(string opcode, bool isBreakpoint)
    {
        IInstruction instruction = null;

        switch (opcode.ToLower())
        {

            case "writestring":
                instruction = new WriteString();
                break;
            case "add":
                instruction = new Add();
                break;
            case "decr":
                instruction = new Decr();
                break;
            case "incr":
                instruction = new Incr();
                break;
            case "subtract":
                instruction = new Subtract();
                break;
            // Handle other instructions similarly
            default:
                break;
        }

        if (instruction != null) instruction.IsBreakpoint = isBreakpoint;
        return instruction;
    }
    private static IInstructionWithOperand TryGetHardcodedInstructionWithOperand(string opcode, bool isBreakpoint, string[] operands)
    {
        IInstructionWithOperand instruction = null;

        switch (opcode.ToLower())
        {
            case "goto":
                instruction = new Goto();
                instruction.Operands = operands;
                break;
            case "loadstring":
                if (operands.Length != 1)
                {
                    throw new SvmCompilationException("LoadString instruction requires one string operand");
                }
                instruction = new LoadString();
                instruction.Operands = operands;
                break;
            case "loadint":
                if (operands.Length != 1)
                {
                    throw new SvmCompilationException("LoadInt instruction requires one integer operand");
                }
                instruction = new LoadInt();
                instruction.Operands = operands;
                break;
            default:
                break;
        }
        if(instruction != null) instruction.IsBreakpoint = isBreakpoint;
        return instruction;
    }
    private static IInstruction SearchInExternalAssemblies(string opcode, bool isBreakpoint)
    {
        string currentDirectory = System.Environment.CurrentDirectory;
        foreach (string dll in Directory.GetFiles(currentDirectory, "*.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(dll);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IInstruction).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        if (string.Equals(type.Name, opcode, StringComparison.OrdinalIgnoreCase))
                        {
                            IInstruction instruction = (IInstruction)Activator.CreateInstance(type);
                            instruction.IsBreakpoint = isBreakpoint;
                            return instruction;
                        }
                    }
                }
            }
            catch
            {
                throw new SvmCompilationException($"dll assembly type exception");
            }
        }

        return null; 
    }

    private static IInstructionWithOperand SearchInExternalAssembliesWithOperand(string opcode, bool isBreakpoint, string[] operands)
    {
        string currentDirectory = System.Environment.CurrentDirectory;
        foreach (string dll in Directory.GetFiles(currentDirectory, "*.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(dll);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IInstructionWithOperand).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                    {
                        if (string.Equals(type.Name, opcode, StringComparison.OrdinalIgnoreCase))
                        {
                            IInstructionWithOperand instruction = (IInstructionWithOperand)Activator.CreateInstance(type);
                            instruction.Operands = operands;
                            instruction.IsBreakpoint = isBreakpoint;
                            return instruction;
                        }
                    }
                }
            }
            catch
            {
                throw new SvmCompilationException($"dll assembly type exception");
            }
        }

        return null; 
    }

    #endregion
}
