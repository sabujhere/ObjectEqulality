using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectEquality
{
    class Program
    {
        static void Main(string[] args)
        {

            Cup[] cups = new Cup[]
            {
                new Cup() { Size = 3 },
                new CoffeCup() { Size = 3,
                                    Material = "Petzold" },
                new Cup() { Size = 3 },
                new TeaCup() { Size = 3,
                                        Color = "Joe" },
                new CoffeCup() { Size = 3,
                                    Material = "Schimmel" },
                new TeaCup() { Size = 3,
                                        Color = "Henry" }
            };

            Array.Sort<Cup>(cups);

            for (int i = 0; i < cups.Length; i++)
                Console.WriteLine(cups[i]);

            Console.ReadLine();
        }
    }

    class Cup:IComparable<Cup>
    {
        public int Size { get; set; }
        public  int CompareTo(Cup other)
        {
            int result = CompareValue(other);
            if (result == 0)
                result = CompareType(other);

            return result;
        }

        private int CompareType(Cup other)
        {
            int result = 0;

            Type thisType = this.GetType();
            Type otherType = other.GetType();

            if (thisType.IsSubclassOf(otherType))
                result = 1;
            else if (otherType.IsSubclassOf(thisType))
                result = -1;
            else if (thisType != otherType)
                result = thisType.FullName.CompareTo(otherType.FullName);

            return result;
        }

        public virtual int CompareValue( Cup other)
        {
            if (object.ReferenceEquals(other, null))
                return 1;
            return Size.CompareTo(other.Size);
        }

        public override string ToString()
        {
            return string.Format("Size={0}, Cup", Size);
        }
    }

    class CoffeCup : Cup
    {
        public string Material { get; set; }

        public override int CompareValue(Cup other)
        {
            int result = base.CompareValue(other);
            if (result == 0 && other is CoffeCup)
            {
                CoffeCup cc = (CoffeCup)other;

                string thisMaterial = Material ?? string.Empty;
                string otherMaterial = cc.Material ?? string.Empty;
                result = thisMaterial.CompareTo(otherMaterial);
            }
            return result;
        }

        public override string ToString()
        {
            return string.Format("Size={0}, Material={1}, CoffeCup", Size, Material);
        }
    }


    class TeaCup : Cup
    {
        public string Color { get; set; }

        public override int CompareValue(Cup other)
        {
            int result = base.CompareValue(other);
            if (result == 0 && other is TeaCup)
            {
                TeaCup cc = (TeaCup)other;

                string thisColor = Color ?? string.Empty;
                string otherColor = cc.Color ?? string.Empty;
                result = thisColor.CompareTo(otherColor);
            }
            return result;
        }
        public override string ToString()
        {
            return string.Format("Size={0}, Color={1}, TeaCup", Size, Color);
        }
    }
}
