using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OracleDbTest.orm;
using OracleDbTest.service;

namespace OracleDbTest
{
    class Program
    {
        //lql
        
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("this is test for oracle db");
            var dataSet = OrmEntryFactory.GetDataSetAccessor();

            var id = new Random().Next(10000);

            var p = new Person
            {
                Id = id,
                Name = "xiaoming",
                Sex = "male",
                Height = 176.1f,
                Weight = 32.23,
                Note = "this is a record",
                Salary = 123.23f,
                IsMarried = true,
                FamilyName = 'a',
                Birthday = new DateTime(2017, 6, 1),
                Count = 324321432143232324
            };
            var flag = dataSet.Insert(p);
            Console.WriteLine("Insert person successful?{0}", flag);

            const string condition = "id=?";
            var person = dataSet.Select<Person>(condition, id);
            Console.WriteLine("birthday:{0}", person.Birthday);
            Console.WriteLine("count:{0}", person.Count);

            */





            /*
            var con = "note like '%is%'";
            var list = dataSet.SelectList<Person>(con);
            var count = dataSet.GetCount<Person>(con);
            Console.WriteLine("Count of person is {0}",count);

            IPersionService service = ServiceFactory.GetPersionService();
            List<PersonDo> personList = service.GetPersonList();

            foreach (var p in personList)
            {
                Console.WriteLine(p.ToString());
            }

            var person = service.GetPerson(1921);
            person.Schools.RemoveAt(3);
            person.Schools.Add(new School() {Id = 6});
            person.Schools.Add(new School() {Id = 7});
            service.SaveSchools(person);
            */

            /*
            IDataSetAccessor dataSet = OrmEntryFactory.GetDataSetAccessor();

            long len = dataSet.GetCount<Person>(null, null);
            Console.WriteLine("Total count of persion:{0}", len);

            len = dataSet.GetCount<School>(null, null);
            Console.WriteLine("total count of school:{0}", len);

            List<Person> persons = dataSet.SelectList<Person>(null, null);
            foreach (var person in persons)
            {
                Console.WriteLine(person.ToString());
            }

            int id = new Random().Next(10000);

            Person p = new Person
            {
                Id = id,
                Name = "xiaoming",
                Sex = "male",
                Height = 176.1f,
                Weight = 32.23,
                Note = "this is a record",
                Salary = 123.23f,
                IsMarried = true,
                FamilyName = 'a'
            };
            bool flag = dataSet.Insert(p);
            Console.WriteLine("Insert person successful?{0}", flag);

            p.Height = 180;
            p.Name = "xiaowang";
            var condition = "id=?";
            flag = dataSet.Update(p, condition, id);

            var p2 = dataSet.Select<Person>(condition, id);
            Console.WriteLine("Update person successful?{0}", flag);
            Console.WriteLine("Is modify person successful?{0}", "xiaowang".Equals(p2.Name) && p2.Height == 180);

            flag = dataSet.Del<Person>(condition, id);
            Console.WriteLine("Delete person successful?{0}", flag);
            */

            /*
            OracleConnection conn = null;
            try
            {
                conn = OpenConn();

                using (var cmd = conn.CreateCommand())
                {
                    var condition = "id=?";

                    var sql = SqlHelper.GenSelectSql(typeof(Person), condition);
                    Console.WriteLine(sql);
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    var reader = cmd.ExecuteReader();
                    const string column1 = "name";
                    const string column2 = "height";
                    const string column3 = "sex";
                    const string column4 = "note";
                    while (reader.Read())
                    {
                        Console.WriteLine($"Name:{reader[column1]}, Sex:{reader[column3]}, Height:{reader[column2]}, Note:{reader[column4]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseConn(conn);
            }
            Console.Read();
        }

        static OracleConnection OpenConn()
        {
            var conn = new OracleConnection
            {
                ConnectionString =
                    "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.2.32.189)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=oracle.db.server)));Persist Security Info=True;User ID=crscd;Password=crscd123@;"
            };
            conn.Open();
            return conn;
        }

        private static void CloseConn(OracleConnection conn)
        {
            if (conn == null) { return; }
            try
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                conn.Dispose();
            }
        }
        */



            //lql
            /*
            Console.Read();
            */
        }
        


    }
}