using System;
using System.Collections.Generic;
using System.Text;

namespace OracleDbTest.entity
{
    public class PersonDo:Person
    {
        public List<School> Schools { set; get; }

        public PersonDo(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Note = person.Note;
            Sex = person.Sex;
            Height = person.Height;
            Weight = person.Weight;
            FamilyName = person.FamilyName;
            IsMarried = person.IsMarried;
            Salary = person.Salary;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(base.ToString());
            foreach (var school in Schools)
            {
                builder.Append(school.ToString()).Append("\t");
            }
            return builder.ToString();
        }
    }
}