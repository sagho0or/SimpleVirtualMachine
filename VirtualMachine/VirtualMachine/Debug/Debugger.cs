using SVM;
using System.Collections.Generic;
using System.Windows.Forms;

public class Debugger : IDebugger
{
    #region TASK 5 - TO BE IMPLEMENTED BY THE STUDENT
    private DebuggerForm debuggerForm;

    public SvmVirtualMachine VirtualMachine { get; set; }

    public Debugger()
    {
        debuggerForm = new DebuggerForm(); 
        debuggerForm.ContinueClicked += OnContinueClicked;
    }
    private void OnContinueClicked()
    {
        debuggerForm.Close();
    }
    public void Break(IDebugFrame debugFrame, int programCounter)
    {
        Stack stack = VirtualMachine.Stack;
        DisplayStack(stack);

        debuggerForm.LoadDebuggerContent(debugFrame.CodeFrame);
        debuggerForm.HighlightBreakpointLine(programCounter);  
        debuggerForm.ShowDialog();
    }

     private void DisplayStack(Stack stack)
     {
        debuggerForm.ClearStack(); 

        foreach (var item in stack)
        {
            debuggerForm.AddToStack(item.ToString()); 
        }
     }

    #endregion
}

