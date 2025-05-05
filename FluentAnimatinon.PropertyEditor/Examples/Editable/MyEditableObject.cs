using FluentAnimatinon.PropertyEditor.Editable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentAnimatinon.PropertyEditor.Examples.Editable
{
    public class MyEditableObject : IEditable
    {
        public MyEditableObject()
        {
            String = "Some String";
            Int = 42;
            Float = 3.14f;
            Double = 12.3456;
            Bool = true;
            Enum = MyEnum.Second;
            List = ["One", "Two", "Three", "Four"];
        }

        public MyEditableObject(string @string, int @int, float @float, double @double, bool @bool, MyEnum @enum, List<string> list)
        {
            String = @string;
            Int = @int;
            Float = @float;
            Double = @double;
            Bool = @bool;
            Enum = @enum;
            List = list;
        }

        [Editable]
        public string String { get; set; }

        [Editable]
        public int Int {  get; set; }

        [Editable]
        public float Float { get; set; }

        [Editable]
        public double Double { get; set; }
         
        [Editable]
        public bool Bool { get; set; }

        [Editable]
        public MyEnum Enum { get; set; }

        [Editable]
        public List<string> List { get; set; }

        public int NonEditableInt { get; set; } = 123456789;

        public enum MyEnum
        {
            First,
            Second,
            Third,
            Fourth,
        }
    }
}
