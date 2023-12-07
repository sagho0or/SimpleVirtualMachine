using System.Drawing;
using System.Windows.Forms;
using SVM.VirtualMachine;

namespace SML_Extensions
{

    public class DisplayImage : BaseInstruction
    {
        public override void Run()
        {
            if (VirtualMachine.Stack.Count == 0 || !(VirtualMachine.Stack.Peek() is Image))
            {
                throw new SvmRuntimeException("Top of the stack is not an Image.");
            }

            Image image = (Image)VirtualMachine.Stack.Pop();
            ShowImage(image);
        }

        private void ShowImage(Image image)
        {
            Form imageForm = new Form
            {
                Width = image.Width + 16,
                Height = image.Height + 39
            };
            PictureBox pictureBox = new PictureBox
            {
                Image = image,
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            imageForm.Controls.Add(pictureBox);
            Application.Run(imageForm);
        }
    }

}
