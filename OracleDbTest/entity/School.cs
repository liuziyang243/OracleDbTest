using System.Text;

namespace OracleDbTest.entity
{
    public class School
    {
        public int Id { set; get; }
        public string Name { set; get; }
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
}