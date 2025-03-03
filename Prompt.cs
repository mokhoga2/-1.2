using System;
using System.Drawing;
using System.Windows.Forms;

namespace InheritanceTreeCalculator
{
    public static class Prompt
    {
        /// <summary>
        /// نافذة حوار بسيطة لإدخال نص من المستخدم.
        /// </summary>
        /// <param name="text">النص الظاهر للمستخدم داخل النافذة.</param>
        /// <param name="caption">عنوان النافذة.</param>
        /// <param name="defaultValue">القيمة الافتراضية لحقل الإدخال.</param>
        /// <returns>النص الذي أدخله المستخدم أو سلسلة فارغة إذا ألغى.</returns>
        public static string ShowDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 180,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label labelText = new Label() { Left = 20, Top = 20, Text = text, AutoSize = true };
            TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Text = defaultValue };

            Button confirmation = new Button() { Text = "موافق", Left = 200, Width = 80, Top = 90, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "إلغاء", Left = 280, Width = 80, Top = 90, DialogResult = DialogResult.Cancel };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            cancel.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(labelText);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(cancel);

            prompt.AcceptButton = confirmation;
            prompt.CancelButton = cancel;

            return (prompt.ShowDialog() == DialogResult.OK) ? inputBox.Text : string.Empty;
        }
    }
}
