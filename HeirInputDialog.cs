using System;
using System.Drawing;
using System.Windows.Forms;

namespace InheritanceTreeCalculator
{
    public class HeirInputDialog : Form
    {
        private TextBox txtName;
        private ComboBox cmbRelationship;
        private Button btnOK;
        private Button btnCancel;
        public string HeirName { get; private set; }
        public string Relationship { get; private set; }

        public HeirInputDialog(string title, string defaultName = "", string defaultRelationship = "")
        {
            this.Text = title;
            this.Size = new Size(400, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            InitializeComponents(defaultName, defaultRelationship);
        }

        private void InitializeComponents(string defaultName, string defaultRelationship)
        {
            // تعريف التسميات وحقل الإدخال
            Label lblName = new Label() { Text = "اسم الوريث:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox() { Location = new Point(120, 20), Width = 220, Text = defaultName };

            Label lblRelationship = new Label() { Text = "العلاقة:", Location = new Point(20, 60), AutoSize = true };
            cmbRelationship = new ComboBox() { Location = new Point(120, 60), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            // إضافة خيارات العلاقات
            cmbRelationship.Items.AddRange(new object[] {
                "ابن",
                "ابنة",
                "زوج",
                "زوجة",
                "والد",
                "والدة",
                "أخ",
                "أخت"
            });
            if (!string.IsNullOrEmpty(defaultRelationship))
            {
                cmbRelationship.SelectedItem = defaultRelationship;
            }
            else if (cmbRelationship.Items.Count > 0)
            {
                cmbRelationship.SelectedIndex = 0;
            }

            // تعريف الأزرار
            btnOK = new Button() { Text = "موافق", Location = new Point(120, 110), DialogResult = DialogResult.OK };
            btnCancel = new Button() { Text = "إلغاء", Location = new Point(220, 110), DialogResult = DialogResult.Cancel };

            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblRelationship);
            this.Controls.Add(cmbRelationship);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;

            btnOK.Click += BtnOK_Click;
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            // التحقق من صحة الإدخال
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("الرجاء إدخال اسم الوريث.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            if (cmbRelationship.SelectedItem == null)
            {
                MessageBox.Show("الرجاء اختيار العلاقة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.None;
                return;
            }
            HeirName = txtName.Text.Trim();
            Relationship = cmbRelationship.SelectedItem.ToString();
            this.Close();
        }
    }
}
