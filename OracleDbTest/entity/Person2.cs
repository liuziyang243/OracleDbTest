using System.Collections.Generic;
using System.Text;

namespace OracleDbTest.entity
{
    public class Person2:Person
    {
        public List<School> Schools { set; get; }

        public Person2(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Sex = person.Sex;
            Note = person.Note;
            Height = person.Height;
            Weight = person.Weight;
            FamilyName = person.FamilyName;
            Salary = person.Salary;
            IsMarried = person.IsMarried;
            Schools = new List<School>();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());
            builder.Append("\t");
            foreach (var school in Schools)
            {
                builder.Append(school.ToString()).Append("\t");
            }
            return builder.ToString();
        }
    }
}