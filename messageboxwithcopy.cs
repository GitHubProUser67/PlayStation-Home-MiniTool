using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStation_MiniTool
{
    public class MessageBoxWithCopyButton
    {
        public static void Show(string message, string caption)
        {
            Form prompt = new Form();
            prompt.Width = 400;
            prompt.Height = 200;
            prompt.Text = caption;

            TextBox textBox = new TextBox();
            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Text = message;
            textBox.Dock = DockStyle.Fill;

            prompt.Controls.Add(textBox);

            prompt.ShowDialog();
        }
    }
}
