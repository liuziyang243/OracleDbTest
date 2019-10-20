using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OracleDbTest.orm;
using System;

namespace OracleDbTest.service
{
    [ServiceContract]
    public interface IPersionService
    {
        [OperationContract]
        List<PersonDo> GetPersonList();

        [OperationContract]
        PersonDo GetPerson(int id);

        [OperationContract]
        void SaveSchools(PersonDo person);
    }

    [DataContract]
    public class School
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Address { set; get; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("id:").Append(Id).Append("\t")
                .Append("name:").Append(Name).Append("\t")
                .Append("address:").Append(Address);
            return builder.ToString();
        }
    }

    [DataContract]
    [TableAttribute("person")]
    public class Person
    {
        [DataMember]
        public int Id { set; get; }

        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Sex { set; get; }

        [DataMember]
        public string Note { set; get; }

        [DataMember]
        public float Height { set; get; }

        [DataMember]
        public double Weight { set; get; }

        [DataMember]
        public char FamilyName { set; get; }

        [DataMember]
        public float Salary { set; get; }

        [DataMember]
        public bool IsMarried { set; get; }

        [DataMember]
        public DateTime Birthday { set; get; }

        [DataMember]
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

    [DataContract]
    public class PersonDo : Person
    {
        [DataMember]
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