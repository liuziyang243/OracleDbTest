using System;
using System.Text;
using OracleDbTest.orm;

namespace OracleDbTest.entity
{
    [TableAttribute("person")]
    public class Person
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Sex { set; get; }
        public string Note { set; get; }
        public float Height { set; get; }
        public double Weight { set; get; }
        public char FamilyName { set; get; }
        public float Salary { set; get; }
        public bool IsMarried { set; get; }
        public DateTime Birthday { set; get; }
        public decimal Count { set; get; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("id:").Append(Id).Append("\t")
                .Append("name:").Append(Name).Append("\t")
                .Append("sex:").Append(Sex).Append("\t")
                .Append("note:").Append(Note).Append("\t")
                .Append("height:").Append(Height).Append("\t")
                .Append("weight:").Append(Weight).Append("\t")
                .Append("familayName:").Append(FamilyName).Append("\t")
                .Append("salary:").Append(Salary).Append("\t")
                .Append("isMarried:").Append(IsMarried).Append("\t")
                .Append("birthday:").Append(Birthday).Append("\t")
                .Append("Count:").Append(Count);
            return builder.ToString();
        }
    }
}