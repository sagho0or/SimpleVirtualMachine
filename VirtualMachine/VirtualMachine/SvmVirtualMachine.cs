using SVM.VirtualMachine;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection.Metadata;
/// <summary>
/// Implements the Simple Virtual Machine (SVM) virtual machine 
/// </summary>
public sealed class SvmVirtualMachine : IVirtualMachine
{
    #region Constants
    private const string CompilationErrorMessage = "An SVM compilation error has occurred at line {0}.\r\n\r\n{1}\r\n";
    private const string RuntimeErrorMessage = "An SVM runtime error has occurred.\r\n\r\n{0}\r\n";
    private const string InvalidOperandsMessage = "The instruction \r\n\r\n\t{0}\r\n\r\nis invalid because there are too many operands. An instruction may have no more than one operand.";
    private const string InvalidLabelMessage = "Invalid label: the label {0} at line {1} is not associated with an instruction.";
    private const string ProgramCounterMessage = "Program counter violation; the program counter value is out of range";
    #endregion

    #region Fields
    private IDebugger debugger = null;
    private List<IInstruction> program = new List<IInstruction>();
    private Stack stack = new Stack();
    private int programCounter;
    
    public Dictionary<string, int> LabelMap { get; private set; }

    #endregion

    #region Constructors

    public SvmVirtualMachine(string filepath)
    {
        LabelMap = new Dictionary<string, int>();
        try
        {
            Compile(filepath);
        }
        catch
        {
            Console.WriteLine("Compilation was not successful. This may be due to errors in JIT compilation or in the sml file loaded. SVM is exiting.");
            return;
        }
        InitializeDebugger();
        try
        {
            Run();
        }
        catch (Exception err)
        {
            if (err is SvmRuntimeException)
            {
                Console.WriteLine(RuntimeErrorMessage, err.Message);
            }
            Console.WriteLine("The sml did not run successfully. This may be due to errors in the runtime, instruction or in the sml file loaded. SVM is exiting.");
            return;
        }

        #region Task 5 - Debugging 
        
        #endregion
    }

    #endregion
    private void InitializeDebugger()
    {
        debugger = new Debugger(); // Instantiate Debugger implementing IDebugger
        debugger.VirtualMachine = this;
    }
    #region Properties
    /// <summary>
    ///  Gets a reference to the virtual machine stack.
    ///  This is used by executing instructions to retrieve
    ///  operands and store results
    /// </summary>
    public Stack Stack
    {
        get
        {
            return stack;
        }
    }

    /// <summary>
    /// Accesses the virtual machine 
    /// program counter (see programCounter in the Fields region).
    /// This can be used by executing instructions to 
    /// determine their order (ie. line number) in the 
    /// sequence of executing SML instructions
    /// </summary>
    public int ProgramCounter
    {
        #region TASK 1 - TO BE IMPLEMENTED BY THE STUDENT
        get { return programCounter; }
        set { programCounter = value; }
        #endregion
    }
    #endregion

    #region Public Methods

    #endregion

    #region Non-public Methods

    /// <summary>
    /// Reads the specified file and tries to 
    /// compile any SML instructions it contains
    /// into an executable SVM program
    /// </summary>
    /// <param name="filepath">The path to the 
    /// .sml file containing the SML program to
    /// be compiled</param>
    /// <exception cref="SvmCompilationException">
    /// If file is not a valid SML program file or 
    /// the SML instructions cannot be compiled to an
    /// executable program</exception>
    private void Compile(string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new SvmCompilationException("The file " + filepath + " does not exist");
        }

        int lineNumber = 0;
        try
        {
            using (StreamReader sourceFile = new StreamReader(filepath))
            {
                while (!sourceFile.EndOfStream)
                {
                    string instruction = sourceFile.ReadLine().Trim();
                    if (instruction.StartsWith("%") && instruction.EndsWith("%") || instruction.StartsWith("* %") && instruction.EndsWith("%")) // Check for a label
                    {
                        string label = ExtractLabel(instruction);
                        LabelMap[label] = lineNumber;
                        continue;
                    }
                    

                    if (!String.IsNullOrEmpty(instruction) &&
                        !String.IsNullOrWhiteSpace(instruction))
                    {
                        ParseInstruction(instruction, lineNumber);
                        lineNumber++;
                    }
                }
            }
        }
        catch (SvmCompilationException err)
        {
            Console.WriteLine(CompilationErrorMessage, lineNumber, err.Message);
            throw;
        }

    }
    private string ExtractLabel(string instructionLine)
    {
        if (instructionLine.StartsWith("* "))
        {
            instructionLine = instructionLine.Substring(2).Trim();
            
        }
        int start = instructionLine.IndexOf('%')+1;
        int end = instructionLine.LastIndexOf('%');
        return instructionLine.Substring(start, end - start).Trim();
    }
    /// <summary>
    /// Executes a compiled SML program 
    /// </summary>
    /// <exception cref="SvmRuntimeException">
    /// If an unexpected error occurs during
    /// program execution
    /// </exception>
    public void Run()
    {
        DateTime start = DateTime.Now;

        #region TASK 2 - TO BE IMPLEMENTED BY THE STUDENT
        try
        {
            // Execute the compiled SML program
            while (ProgramCounter < program.Count)
            {
                IInstruction instruction = program[ProgramCounter];
                instruction.VirtualMachine = this;
                

                if (instruction.IsBreakpoint)
                {
                    IDebugFrame debugFrame = CreateDebugFrame(instruction);
                    debugger.Break(debugFrame, ProgramCounter);
                }

                instruction.Run();
                if (!instruction.ModifiedProgramCounter)
                {
                    programCounter++;
                }
            }

            // Output a message indicating successful execution
            Console.WriteLine("SML program executed successfully.");
        }
        catch (Exception err)
        {
            if (err is SvmRuntimeException)
            {
                Console.WriteLine(RuntimeErrorMessage, err.Message);
            }
            Console.WriteLine("The SML did not run successfully. This may be due to errors in the runtime, instruction, or in the SML file loaded. SVM is exiting.");
            return;
        }

        long memUsed = System.Environment.WorkingSet;
        TimeSpan elapsed = DateTime.Now - start;
        Console.WriteLine(String.Format(
                                    "\r\n\r\nExecution finished in {0} milliseconds. Memory used = {1} bytes. Press any key to exit.",
                                    elapsed.Milliseconds,
                                    memUsed));
        Console.ReadKey();
    }
    private IDebugFrame CreateDebugFrame(IInstruction instruction)
    {
        // Return an instance of IDebugFrame
        DebuggerFrame debugFrame = new DebuggerFrame(instruction, program, stack);
        return debugFrame;
    }
  

    /// <summary>
    /// Parses a string from a .sml file containing a single
    /// SML instruction
    /// </summary>
    /// <param name="instruction">The string representation
    /// of an instruction</param>
    public void ParseInstruction(string instruction, int lineNumber)
    {
        #region TASK 5 & 7 - MAY REQUIRE MODIFICATION BY THE STUDENT
        bool isBreakpoint = false;

        if (instruction.StartsWith("* "))
        {
            instruction = instruction.Substring(2);
            isBreakpoint = true;
        }
 

        #endregion

        string[] tokens = null;
        if (instruction.Contains("\""))
        {
            tokens = instruction.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);

            // Remove any unnecessary whitespace
            for (int i = 0; i < tokens.Length; i++)
            {
                tokens[i] = tokens[i].Trim();
            }
        }
        else
        {
            // Tokenize the instruction string by separating on spaces
            tokens = instruction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }


        // Ensure the correct number of operands
        if (tokens.Length > 3)
        {
            throw new SvmCompilationException(String.Format(InvalidOperandsMessage, instruction));
        }

        switch (tokens.Length)
        {
            case 1:
                program.Add(JITCompiler.CompileInstruction(tokens[0], isBreakpoint));
                break;
            case 2:
                program.Add(JITCompiler.CompileInstruction(tokens[0], isBreakpoint, tokens[1].Trim('\"')));
                break;
            case 3:
                program.Add(JITCompiler.CompileInstruction(tokens[0], isBreakpoint, tokens[1].Trim('\"'), tokens[2].Trim('\"')));
                break;
        }
        
    }
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
}





#endregion