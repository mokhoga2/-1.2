namespace InheritanceTreeCalculator
{
    partial class Form1
    {
        /// <summary>
        /// المتغيرات المصممة المطلوبة.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // تعريف عناصر الواجهة
        private System.Windows.Forms.Panel panelTree;
        private System.Windows.Forms.DataGridView dataGridViewHeirs;
        private System.Windows.Forms.Button btnAddDeceased;
        private System.Windows.Forms.Button btnCalculateInheritance;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnEditHeir;
        private System.Windows.Forms.Button btnDeleteHeir;
        private System.Windows.Forms.TextBox txtEstateValue;
        private System.Windows.Forms.Label labelEstate;
        private System.Windows.Forms.ComboBox comboBoxMethod;
        private System.Windows.Forms.Label labelMethod;

        /// <summary>
        /// تنظيف الموارد المستخدمة.
        /// </summary>
        /// <param name="disposing">true إذا كان يجب تدمير الموارد المُدارة؛ وإلا false.</param>
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
        /// طريقة دعم المصمم - لا تعدل محتواها باستخدام محرر الأكواد.
        /// </summary>
        private void InitializeComponent()
        {
            panelTree = new Panel();
            dataGridViewHeirs = new DataGridView();
            btnAddDeceased = new Button();
            btnCalculateInheritance = new Button();
            btnReset = new Button();
            btnEditHeir = new Button();
            btnDeleteHeir = new Button();
            txtEstateValue = new TextBox();
            labelEstate = new Label();
            comboBoxMethod = new ComboBox();
            labelMethod = new Label();
            txtAlItibar = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHeirs).BeginInit();
            SuspendLayout();
            // 
            // panelTree
            // 
            panelTree.AutoScroll = true;
            panelTree.BorderStyle = BorderStyle.Fixed3D;
            panelTree.Location = new Point(12, 12);
            panelTree.Name = "panelTree";
            panelTree.Size = new Size(1111, 400);
            panelTree.TabIndex = 0;
            panelTree.Paint += panelTree_Paint;
            panelTree.MouseClick += panelTree_MouseClick;
            // 
            // dataGridViewHeirs
            // 
            dataGridViewHeirs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHeirs.Location = new Point(12, 420);
            dataGridViewHeirs.Name = "dataGridViewHeirs";
            dataGridViewHeirs.RowHeadersWidth = 51;
            dataGridViewHeirs.Size = new Size(933, 312);
            dataGridViewHeirs.TabIndex = 1;
            // 
            // btnAddDeceased
            // 
            btnAddDeceased.BackColor = Color.FromArgb(0, 192, 0);
            btnAddDeceased.Location = new Point(1193, 12);
            btnAddDeceased.Name = "btnAddDeceased";
            btnAddDeceased.Size = new Size(150, 57);
            btnAddDeceased.TabIndex = 2;
            btnAddDeceased.Text = "إضافة المتوفّى";
            btnAddDeceased.UseVisualStyleBackColor = false;
            btnAddDeceased.Click += btnAddDeceased_Click;
            // 
            // btnCalculateInheritance
            // 
            btnCalculateInheritance.BackColor = SystemColors.ActiveCaption;
            btnCalculateInheritance.Location = new Point(1193, 128);
            btnCalculateInheritance.Name = "btnCalculateInheritance";
            btnCalculateInheritance.Size = new Size(150, 50);
            btnCalculateInheritance.TabIndex = 3;
            btnCalculateInheritance.Text = "حساب الميراث";
            btnCalculateInheritance.UseVisualStyleBackColor = false;
            btnCalculateInheritance.Click += btnCalculateInheritance_Click;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.Red;
            btnReset.Location = new Point(1193, 358);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(150, 54);
            btnReset.TabIndex = 4;
            btnReset.Text = "إعادة تعيين";
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += btnReset_Click;
            // 
            // btnEditHeir
            // 
            btnEditHeir.BackColor = Color.FromArgb(255, 128, 0);
            btnEditHeir.Location = new Point(1193, 238);
            btnEditHeir.Name = "btnEditHeir";
            btnEditHeir.Size = new Size(150, 54);
            btnEditHeir.TabIndex = 5;
            btnEditHeir.Text = "تعديل وريث";
            btnEditHeir.UseVisualStyleBackColor = false;
            btnEditHeir.Click += btnEditHeir_Click;
            // 
            // btnDeleteHeir
            // 
            btnDeleteHeir.BackColor = Color.IndianRed;
            btnDeleteHeir.Location = new Point(1193, 298);
            btnDeleteHeir.Name = "btnDeleteHeir";
            btnDeleteHeir.Size = new Size(150, 54);
            btnDeleteHeir.TabIndex = 6;
            btnDeleteHeir.Text = "حذف وريث";
            btnDeleteHeir.UseVisualStyleBackColor = false;
            btnDeleteHeir.Click += btnDeleteHeir_Click;
            // 
            // txtEstateValue
            // 
            txtEstateValue.Location = new Point(1193, 95);
            txtEstateValue.Name = "txtEstateValue";
            txtEstateValue.Size = new Size(150, 27);
            txtEstateValue.TabIndex = 7;
            // 
            // labelEstate
            // 
            labelEstate.AutoSize = true;
            labelEstate.Location = new Point(1120, 436);
            labelEstate.Name = "labelEstate";
            labelEstate.RightToLeft = RightToLeft.No;
            labelEstate.Size = new Size(54, 20);
            labelEstate.TabIndex = 8;
            labelEstate.Text = "الاعتبار:";
            labelEstate.Click += labelEstate_Click;
            // 
            // comboBoxMethod
            // 
            comboBoxMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxMethod.FormattingEnabled = true;
            comboBoxMethod.Items.AddRange(new object[] { "الشريعة الإسلامية", "القانون العراقي" });
            comboBoxMethod.Location = new Point(1193, 204);
            comboBoxMethod.Name = "comboBoxMethod";
            comboBoxMethod.Size = new Size(150, 28);
            comboBoxMethod.TabIndex = 9;
            // 
            // labelMethod
            // 
            labelMethod.AutoSize = true;
            labelMethod.Location = new Point(1213, 181);
            labelMethod.Name = "labelMethod";
            labelMethod.Size = new Size(106, 20);
            labelMethod.TabIndex = 10;
            labelMethod.Text = "طريقة التقسيم:";
            labelMethod.Click += labelMethod_Click;
            // 
            // txtAlItibar
            // 
            txtAlItibar.Location = new Point(1193, 429);
            txtAlItibar.Name = "txtAlItibar";
            txtAlItibar.Size = new Size(145, 27);
            txtAlItibar.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1121, 432);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.No;
            label1.Size = new Size(66, 20);
            label1.TabIndex = 8;
            label1.Text = "الاعتبار   :";
            label1.Click += labelEstate_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(1372, 703);
            Controls.Add(txtAlItibar);
            Controls.Add(labelMethod);
            Controls.Add(comboBoxMethod);
            Controls.Add(label1);
            Controls.Add(labelEstate);
            Controls.Add(txtEstateValue);
            Controls.Add(btnDeleteHeir);
            Controls.Add(btnEditHeir);
            Controls.Add(btnReset);
            Controls.Add(btnCalculateInheritance);
            Controls.Add(btnAddDeceased);
            Controls.Add(dataGridViewHeirs);
            Controls.Add(panelTree);
            Name = "Form1";
            Text = "مشجّر الورثة وحساب الميراث";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewHeirs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtAlItibar;
        private Label label1;
    }
}
