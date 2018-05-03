﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OracleDbTest.entity;
using OracleDbTest.orm;

namespace OracleDbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> map = ResultHandler.GetNameIndexMap(typeof(Person));

            Console.WriteLine("this is test for oracle db");
            IDataSetAccessor dataSet = OrmEntryFactory.GetDataSetAccessor();

            string condition = "1=1";
            List<Person> persons = dataSet.SelectList<Person>(condition, null);
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
                Height = 176,
                Note = "this is a record"
            };
            bool flag = dataSet.Insert(p);
            Console.WriteLine("Insert person successful?{0}", flag);

            p.Height = 180;
            p.Name = "xiaowang";
            condition = "id=?";
            flag = dataSet.Update(p, condition, id);

            var p2 = dataSet.Select<Person>(condition, id);
            Console.WriteLine("Update person successful?{0}", flag);
            Console.WriteLine("Is modify person successful?{0}", "xiaowang".Equals(p2.Name) && p2.Height == 180);

            flag = dataSet.Del<Person>(condition, id);
            Console.WriteLine("Delete person successful?{0}", flag);

            Console.Read();

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
        }
    }
}