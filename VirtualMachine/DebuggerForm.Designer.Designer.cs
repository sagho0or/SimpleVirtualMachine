using System.Windows.Forms;

namespace SVM
{
    partial class DebuggerForm 
    {
        public event Action ContinueClicked;


        public void LoadDebuggerContent(List<IInstruction> codeLines)
        {
            foreach (var line in codeLines)
            {
                Code.Items.Add(line);
            }
        }

        public void HighlightBreakpointLine(int breakpointLine)
        {
            Code.SelectedIndex = breakpointLine - 1;
            Code.TopIndex = breakpointLine - 1;
            Code.SetSelected(breakpointLine - 1, true);
        }

     
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new System.Windows.Forms.Button();
            Code = new System.Windows.Forms.ListBox();
            Stack = new System.Windows.Forms.ListBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(95, 377);
            button1.Name = "button1";
            button1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            button1.Size = new System.Drawing.Size(599, 46);
            button1.TabIndex = 0;
            button1.Text = "Continue";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Code
            // 
            Code.FormattingEnabled = true;
            Code.ItemHeight = 32;
            Code.Location = new System.Drawing.Point(30, 42);
            Code.Name = "Code";
            Code.Size = new System.Drawing.Size(305, 324);
            Code.TabIndex = 1;
            Code.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Stack
            // 
            Stack.FormattingEnabled = true;
            Stack.ItemHeight = 32;
            Stack.Location = new System.Drawing.Point(381, 42);
            Stack.Name = "Stack";
            Stack.Size = new System.Drawing.Size(353, 324);
            Stack.TabIndex = 2;
            Stack.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            // 
            // DebuggerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(Stack);
            Controls.Add(Code);
            Controls.Add(button1);
            Name = "DebuggerForm";
            Text = "DebuggerForm";
            ResumeLayout(false);
        }

        #endregion
        public void ClearStack()
        {
            Stack.Items.Clear(); // Clears all items in the stack ListBox
        }

        public void AddToStack(string item)
        {
            Stack.Items.Add(item); // Adds an item to the stack ListBox
        }
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListBox Code;
        public System.Windows.Forms.ListBox Stack;
    }
}