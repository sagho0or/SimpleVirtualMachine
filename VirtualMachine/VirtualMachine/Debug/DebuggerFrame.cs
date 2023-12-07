
public class DebuggerFrame : IDebugFrame
{
    public IInstruction CurrentInstruction { get; private set; }
    public List<IInstruction> CodeFrame { get; private set; }
    public Stack StackContents { get; private set; }

    public DebuggerFrame(IInstruction currentInstruction, List<IInstruction> codeFrame, Stack stackContents)
    {
        CurrentInstruction = currentInstruction;
        CodeFrame = codeFrame;
        StackContents = stackContents;
    }
}



