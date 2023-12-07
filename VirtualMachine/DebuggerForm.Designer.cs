using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SVM
{
    public partial class DebuggerForm : Form
    {
        public DebuggerForm()
        {
            InitializeComponent();
            button1.Click += button1_Click;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContinueClicked?.Invoke();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
