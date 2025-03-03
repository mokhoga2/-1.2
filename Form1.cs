using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace InheritanceTreeCalculator
{
    public partial class Form1 : Form

    {
        private int DistributeShares(List<Heir> heirs, int estateValue, int totalWeight)
        {
            int sumDistributed = 0;
            foreach (var heir in heirs)
            {
                int share = (estateValue * heir.Weight) / totalWeight;
                heir.InheritanceValue = share;
                sumDistributed += share;
            }
            return estateValue - sumDistributed;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // إعادة تعيين الشجرة بحذف جذرها
            rootHeir = null;
            // مسح قيمة التركة من حقل الإدخال
            txtEstateValue.Text = "";
            // إعادة تعيين القائمة المنسدلة إلى الخيار الأول (مثلاً "الشريعة الإسلامية")
            comboBoxMethod.SelectedIndex = 0;
            // مسح جميع صفوف جدول الورثة
            dataGridViewHeirs.Rows.Clear();
            // تحديث اللوحة لإزالة أي رسومات للشجرة
            panelTree.Invalidate();
        }

        // جذر الشجرة (المتوفّى)
        private Heir rootHeir = null;

        public Form1()
        {
            InitializeComponent();
            // تعيين الخيار الافتراضي لطريقة التقسيم
            comboBoxMethod.SelectedIndex = 0;
            // تهيئة أعمدة DataGridView
            InitializeDataGridView();
        }

        // تهيئة DataGridView
        private void InitializeDataGridView()
        {
            dataGridViewHeirs.Columns.Clear();
            dataGridViewHeirs.Columns.Add("Name", "الاسم");
            dataGridViewHeirs.Columns.Add("Relationship", "العلاقة");
            dataGridViewHeirs.Columns.Add("Percentage", "نسبة الحصة (%)");
            dataGridViewHeirs.Columns.Add("Value", "قيمة الحصة");
        }

        // زر "إضافة المتوفّى"
        private void btnAddDeceased_Click(object sender, EventArgs e)
        {
            string name = Prompt.ShowDialog("أدخل اسم المتوفّى:", "إضافة المتوفّى", "المتوفّى");
            if (string.IsNullOrWhiteSpace(name))
                return;

            // تعيين المتوفّى كجذر للشجرة
            rootHeir = new Heir(name, "المتوفّى");
            panelTree.Invalidate(); // إعادة رسم
            UpdateDataGridView();
        }

        // حدث الرسم في اللوحة
        private void panelTree_Paint(object sender, PaintEventArgs e)
        {
            if (rootHeir != null)
            {
                // حفظ الإحداثيات الأفقية لكل مستوى
                Dictionary<int, int> levelXPositions = new Dictionary<int, int>();
                DrawHeir(e.Graphics, rootHeir, 0, levelXPositions);
            }
        }

        // رسم الوريث وأبنائه
        private void DrawHeir(Graphics g, Heir heir, int level, Dictionary<int, int> levelXPositions)
        {
            int nodeWidth = 100;
            int nodeHeight = 50;
            int verticalSpacing = 80;
            int horizontalSpacing = 20;

            if (!levelXPositions.ContainsKey(level))
                levelXPositions[level] = 10; // يبدأ من 10 بكسل أفقياً

            int x = levelXPositions[level];
            int y = level * verticalSpacing + 10;

            // رسم مستطيل الوريث
            Rectangle rect = new Rectangle(x, y, nodeWidth, nodeHeight);
            heir.Bounds = rect;
            g.DrawRectangle(Pens.Black, rect);

            // كتابة الاسم والعلاقة
            string text = $"{heir.Name}\n{heir.Relationship}";
            g.DrawString(text, this.Font, Brushes.Black, rect);

            // رسم زر "+" في زاوية المستطيل
            Rectangle plusRect = new Rectangle(x + nodeWidth - 15, y, 15, 15);
            heir.PlusButton = plusRect;
            g.FillRectangle(Brushes.LightGreen, plusRect);
            g.DrawRectangle(Pens.Black, plusRect);
            g.DrawString("+", this.Font, Brushes.Black, plusRect);

            // تحديث الإحداثي الأفقي للعقدة التالية
            levelXPositions[level] = x + nodeWidth + horizontalSpacing;

            // رسم أبناء الوريث
            foreach (var child in heir.Children)
            {
                DrawHeir(g, child, level + 1, levelXPositions);

                // رسم خط بين الوالد والابن
                Point parentPoint = new Point(rect.X + nodeWidth / 2, rect.Y + nodeHeight);
                Point childPoint = new Point(child.Bounds.X + nodeWidth / 2, child.Bounds.Y);
                g.DrawLine(Pens.Black, parentPoint, childPoint);
            }
        }

        // حدث النقر بالماوس على اللوحة
        // حدث النقر بالماوس على اللوحة (Form1.cs)
        private void panelTree_MouseClick(object sender, MouseEventArgs e)
        {
            if (rootHeir != null)
            {
                Heir clickedHeir = FindHeirByPlusButton(rootHeir, e.Location);
                if (clickedHeir != null)
                {
                    // استخدام نافذة الحوار الجديدة لإضافة وريث
                    HeirInputDialog dialog = new HeirInputDialog("إضافة وريث");
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        Heir newChild = new Heir(dialog.HeirName, dialog.Relationship);
                        clickedHeir.Children.Add(newChild);
                        panelTree.Invalidate(); // إعادة رسم الشجرة
                        UpdateDataGridView();
                    }
                }
            }
        }

        // البحث التكراري عن العقدة التي تحتوي على زر "+"
        private Heir FindHeirByPlusButton(Heir heir, Point clickPoint)
        {
            if (heir.PlusButton.Contains(clickPoint))
            {
                return heir;
            }
            foreach (var child in heir.Children)
            {
                Heir result = FindHeirByPlusButton(child, clickPoint);
                if (result != null)
                    return result;
            }
            return null;
        }

        // زر "حساب الميراث"
        // دالة لتحديد الوزن بناءً على العلاقة
        private int GetWeight(string relationship, string method)
        {
            if (method == "الشريعة الإسلامية")
            {
                // الذكر = 2، الأنثى = 1
                switch (relationship)
                {
                    case "ابن": return 2;
                    case "ابنة": return 1;
                    default: return 1; // أي علاقة أخرى نعطيها 1 كافتراض
                }
            }
            else // القانون العراقي
            {
                // الذكر = الأنثى = 1
                switch (relationship)
                {
                    case "ابن": return 1;
                    case "ابنة": return 1;
                    default: return 1;
                }
            }
        }

        // دالة حساب الميراث بحيث تُولد قيمة التركة تلقائياً وتُحسب الحصص بأرقام صحيحة
        // دالة مساعدة لحساب الـ GCD (أكبر قاسم مشترك)
        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // دالة مساعدة لحساب الـ LCM (أقل مضاعف مشترك) لعدد من الأعداد
        private int LCM(params int[] numbers)
        {
            if (numbers.Length == 0) return 1;
            int lcm = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                lcm = (lcm * numbers[i]) / GCD(lcm, numbers[i]);
            }
            return lcm;
        }

        private void btnCalculateInheritance_Click(object sender, EventArgs e)
        {
            // التحقق من وجود المتوفّى (جذر الشجرة)
            if (rootHeir == null)
            {
                MessageBox.Show("الرجاء إضافة المتوفّى أولاً.");
                return;
            }

            // جمع كل الورثة (باستثناء المتوفّى)
            List<Heir> heirs = new List<Heir>();
            GetAllHeirs(rootHeir, heirs);
            if (heirs.Count == 0)
            {
                MessageBox.Show("لا يوجد ورثة لحساب الميراث.");
                return;
            }

            // قراءة طريقة التقسيم من comboBoxMethod
            string method = comboBoxMethod.SelectedItem?.ToString() ?? "الشريعة الإسلامية";

            // تصنيف الورثة: الزوج/الزوجة، الأم (والدة)، الأب (والد)، والأبناء (ابن/ابنة)
            Heir spouse = null, mother = null, father = null;
            List<Heir> children = new List<Heir>();
            foreach (var h in heirs)
            {
                if (h.Relationship == "زوجة" || h.Relationship == "زوج")
                    spouse = h;
                else if (h.Relationship == "والدة")
                    mother = h;
                else if (h.Relationship == "والد")
                    father = h;
                else if (h.Relationship == "ابن" || h.Relationship == "ابنة")
                    children.Add(h);
            }

            int T = 0; // قيمة التركة "الاعتبار"

            if (method == "الشريعة الإسلامية")
            {
                // حساب وحدات الأطفال
                int S = 0; // مجموع وحدات الأطفال
                foreach (var child in children)
                {
                    if (child.Relationship == "ابنة")
                        S += 1;
                    else if (child.Relationship == "ابن")
                        S += 2;
                }

                // تحديد المقامات للفئات الثابتة (إن وُجدت)
                List<int> fixedDenoms = new List<int>();
                if (spouse != null)
                {
                    if (spouse.Relationship == "زوجة")
                        fixedDenoms.Add(8);
                    else if (spouse.Relationship == "زوج")
                        fixedDenoms.Add(4);
                }
                if (mother != null)
                {
                    // إذا وُجد أطفال، الأم = 6، وإلا = 3
                    fixedDenoms.Add((children.Count > 0) ? 6 : 3);
                }
                if (father != null)
                {
                    fixedDenoms.Add((children.Count > 0) ? 6 : 3);
                }

                // إذا لم يوجد أطفال، S = 1 لتجنب القسمة على صفر (لكن عادةً يجب أن يكون هناك أطفال)
                if (S == 0) S = 1;

                // حساب العامل المشترك M = LCM لكل المقامات الثابتة؛ إذا لم توجد فئات ثابتة، نستخدم 8 افتراضيًا
                int M = (fixedDenoms.Count > 0) ? LCM(fixedDenoms.ToArray()) : 8;

                // توليد قيمة التركة T ديناميكيًا: T = S * M
                if (string.IsNullOrWhiteSpace(txtAlItibar.Text))
                    T = S * M;
                else if (!int.TryParse(txtAlItibar.Text, out T))
                {
                    MessageBox.Show("قيمة الاعتبار غير صحيحة.");
                    return;
                }

                // توزيع الحصص الثابتة:
                int spouseShare = 0, motherShare = 0, fatherShare = 0;
                if (spouse != null)
                {
                    if (spouse.Relationship == "زوجة")
                        spouseShare = T / 8;
                    else if (spouse.Relationship == "زوج")
                        spouseShare = T / 4;
                    spouse.InheritanceValue = spouseShare;
                    spouse.SharePercentage = (spouseShare * 100m) / T;
                }
                if (mother != null)
                {
                    motherShare = (children.Count > 0) ? (int)Math.Round((decimal)T / 6) : (int)Math.Round((decimal)T / 3);
                    mother.InheritanceValue = motherShare;
                    mother.SharePercentage = (motherShare * 100m) / T;
                }
                if (father != null)
                {
                    fatherShare = (children.Count > 0) ? (int)Math.Round((decimal)T / 6) : (int)Math.Round((decimal)T / 3);
                    father.InheritanceValue = fatherShare;
                    father.SharePercentage = (fatherShare * 100m) / T;
                }

                int fixedTotal = 0;
                if (spouse != null) fixedTotal += spouseShare;
                if (mother != null) fixedTotal += motherShare;
                if (father != null) fixedTotal += fatherShare;

                // باقي التركة للأطفال:
                int remaining = T - fixedTotal;
                // وحدة الأطفال = remaining / S
                int unit = (S > 0) ? remaining / S : 0;

                // توزيع للأطفال: في الشريعة: ابنة = 1 وحدة، ابن = 2 وحدات
                foreach (var child in children)
                {
                    int childUnits = (child.Relationship == "ابنة") ? 1 : 2;
                    int childShare = unit * childUnits;
                    child.InheritanceValue = childShare;
                    child.SharePercentage = (childShare * 100m) / T;
                }
            }
            else // نظام القانون العراقي
            {
                // في النظام العراقي: نفترض نسب ثابتة:
                // الزوجة/الزوج: 1/4، الأم/الأب: 1/6، والأطفال: كل طفل وحدة واحدة.
                int S = children.Count; // هنا S هو عدد الأطفال
                if (string.IsNullOrWhiteSpace(txtAlItibar.Text))
                    T = (S > 0) ? S * 4 : 64;  // اختيار T = عدد الأطفال * 4
                else if (!int.TryParse(txtAlItibar.Text, out T))
                {
                    MessageBox.Show("قيمة الاعتبار غير صحيحة.");
                    return;
                }

                int spouseShare = (spouse != null) ? T / 4 : 0;
                if (spouse != null)
                {
                    spouse.InheritanceValue = spouseShare;
                    spouse.SharePercentage = (spouseShare * 100m) / T;
                }

                int motherShare = (mother != null) ? (int)Math.Round((decimal)T / 6) : 0;
                if (mother != null)
                {
                    mother.InheritanceValue = motherShare;
                    mother.SharePercentage = (motherShare * 100m) / T;
                }

                int fatherShare = (father != null) ? (int)Math.Round((decimal)T / 6) : 0;
                if (father != null)
                {
                    father.InheritanceValue = fatherShare;
                    father.SharePercentage = (fatherShare * 100m) / T;
                }

                int fixedTotal = 0;
                if (spouse != null) fixedTotal += spouseShare;
                if (mother != null) fixedTotal += motherShare;
                if (father != null) fixedTotal += fatherShare;

                int remaining = T - fixedTotal;
                int shareEach = (S > 0) ? remaining / S : 0;
                foreach (var child in children)
                {
                    child.InheritanceValue = shareEach;
                    child.SharePercentage = (shareEach * 100m) / T;
                }
            }

            // تحديث واجهة txtAlItibar لتظهر T النهائية
            txtAlItibar.Text = T.ToString();
            UpdateDataGridView();
            panelTree.Invalidate();
        }








        // دالة تكرارية لجمع جميع الورثة (باستثناء جذر الشجرة)
        private void GetAllHeirs(Heir heir, List<Heir> heirs)
        {
            foreach (var child in heir.Children)
            {
                heirs.Add(child);
                GetAllHeirs(child, heirs);
            }
        }

        // تحديث DataGridView
        // تحديث DataGridView لتخزين كائن الوريث في الخاصية Tag
        private void UpdateDataGridView()
        {
            dataGridViewHeirs.Rows.Clear();
            if (rootHeir == null)
                return;

            List<Heir> heirs = new List<Heir>();
            GetAllHeirs(rootHeir, heirs);

            decimal totalPercentage = 0m;
            int totalShares = 0;
            int receivingHeirsCount = 0; // عدد الورثة الذين يحصلون على ورث > 0

            // إضافة صفوف الورثة وحساب المجاميع
            foreach (var heir in heirs)
            {
                int rowIndex = dataGridViewHeirs.Rows.Add(
                    heir.Name,
                    heir.Relationship,
                    heir.SharePercentage.ToString("0.##"),
                    heir.InheritanceValue.ToString()
                );
                dataGridViewHeirs.Rows[rowIndex].Tag = heir;

                totalPercentage += heir.SharePercentage;
                totalShares += heir.InheritanceValue;

                if (heir.InheritanceValue > 0)
                {
                    receivingHeirsCount++;
                }
            }

            // إضافة صف الملخص باستخدام عدد الورثة الذين حصلوا على نصيب
            int summaryRowIndex = dataGridViewHeirs.Rows.Add(
                "المجموع:",
                receivingHeirsCount.ToString(),           // عدد الورثة الذين لهم نصيب
                totalPercentage.ToString("0.##"),           // مجموع النسب المئوية
                totalShares.ToString()                       // مجموع الحصص
            );

            // تنسيق صف الملخص بلون مميز (مثلاً LightCoral) وخط غامق
            DataGridViewRow summaryRow = dataGridViewHeirs.Rows[summaryRowIndex];
            summaryRow.DefaultCellStyle.BackColor = Color.LightCoral;
            summaryRow.DefaultCellStyle.Font = new Font(dataGridViewHeirs.Font, FontStyle.Bold);
        }

        // دالة تعديل الوريث
        private void btnEditHeir_Click(object sender, EventArgs e)
        {
            // التأكد من اختيار صف في DataGridView
            if (dataGridViewHeirs.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار وريث للتعديل.");
                return;
            }

            var selectedRow = dataGridViewHeirs.SelectedRows[0];
            Heir selectedHeir = selectedRow.Tag as Heir;
            if (selectedHeir == null)
            {
                MessageBox.Show("لم يتم العثور على بيانات الوريث.");
                return;
            }

            // استخدام نافذة الحوار مع القيم الحالية
            HeirInputDialog dialog = new HeirInputDialog("تعديل الوريث", selectedHeir.Name, selectedHeir.Relationship);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedHeir.Name = dialog.HeirName;
                selectedHeir.Relationship = dialog.Relationship;
                UpdateDataGridView();
                panelTree.Invalidate();
            }
        }
        // دالة حذف الوريث
        private void btnDeleteHeir_Click(object sender, EventArgs e)
        {
            // التحقق من اختيار صف في DataGridView
            if (dataGridViewHeirs.SelectedRows.Count == 0)
            {
                MessageBox.Show("الرجاء اختيار وريث للحذف.");
                return;
            }

            var selectedRow = dataGridViewHeirs.SelectedRows[0];
            Heir? selectedHeir = selectedRow.Tag as Heir;
            if (selectedHeir == null)
            {
                MessageBox.Show("لم يتم العثور على بيانات الوريث.");
                return;
            }

            // لا يسمح بحذف جذر الشجرة (المتوفّى)
            if (selectedHeir.Relationship == "المتوفّى")
            {
                MessageBox.Show("لا يمكن حذف المتوفّى.");
                return;
            }

            // تأكيد الحذف
            var result = MessageBox.Show("هل أنت متأكد من حذف الوريث؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            // إزالة الوريث من الشجرة باستخدام دالة مساعدة
            bool removed = RemoveHeir(rootHeir, selectedHeir);
            if (!removed)
            {
                MessageBox.Show("فشل حذف الوريث.");
                return;
            }

            // تحديث الجدول وإعادة رسم الشجرة
            UpdateDataGridView();
            panelTree.Invalidate();
        }

        // دالة مساعدة لحذف الوريث من شجرة الورثة (تبحث في الأطفال وتزيل الوريث إذا وجدته)
        private bool RemoveHeir(Heir parent, Heir target)
        {
            if (parent.Children.Contains(target))
            {
                parent.Children.Remove(target);
                return true;
            }
            foreach (var child in parent.Children)
            {
                bool removed = RemoveHeir(child, target);
                if (removed)
                    return true;
            }
            return false;
        }

        private void labelMethod_Click(object sender, EventArgs e)
        {

        }

        private void labelEstate_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// تقوم هذه الدالة بتوزيع قيمة التركة (estateValue) على الورثة بناءً على أوزانهم،
        /// باستخدام القسمة الصحيحة، وتعيد قيمة المتبقي (leftover) الذي لم يُوزّع.
        /// </summary>
        /// <param name="heirs">قائمة الورثة.</param>
        /// <param name="estateValue">قيمة التركة.</param>
        /// <param name="totalWeight">مجموع أوزان الورثة.</param>
        /// <returns>المبلغ المتبقي بعد التوزيع (leftover).</returns>

        private void InitializeAlItibarAndOther()
        {
            // ضع أي أوامر تهيئة تريدها
            // مثلاً: txtAlItibar.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
