using System;
using System.Drawing;
using System.IO;
using SVM.VirtualMachine;

namespace SML_Extensions
{
    public class LoadImage : BaseInstructionWithOperand
    {
        public override void Run()
        {
            if (Operands.Length != 1)
            {
                throw new SvmRuntimeException("LoadImage instruction requires a single string operand (file path)");
            }

            string imagePath = Operands[0];

            if (!File.Exists(imagePath))
            {
                throw new SvmRuntimeException($"Image file not found at path: {imagePath}");
            }

            try
            {
                Image image = Image.FromFile(imagePath);
                VirtualMachine.Stack.Push(image);
            }
            catch (Exception ex)
            {
                throw new SvmRuntimeException($"Failed to load image from path '{imagePath}': {ex.Message}");
            }
        }
    }
}
