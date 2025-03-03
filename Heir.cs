using System.Collections.Generic;
using System.Drawing;

namespace InheritanceTreeCalculator
{
    public class Heir
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public List<Heir> Children { get; set; }
        public Rectangle Bounds { get; set; }
        public Rectangle PlusButton { get; set; }

        // نسبة الحصة من التركة (بالمئة) على شكل عدد عشري
        public decimal SharePercentage { get; set; }

        // القيمة المادية للحصة (هنا اخترناها عددًا صحيحًا لضمان عدم وجود كسور)
        public int InheritanceValue { get; set; }

        // وزن الوريث (يُستخدم لحساب الحصص وفقًا للعلاقة)
        public int Weight { get; set; }

        public Heir(string name, string relationship)
        {
            Name = name;
            Relationship = relationship;
            Children = new List<Heir>();
        }
    }
}
