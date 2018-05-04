using System.Collections.Generic;
using OracleDbTest.entity;

namespace OracleDbTest.service
{
    public interface IPersionService
    {
        List<Person2> GetPersionList();

        void SaveSchools(Person2 persion);
    }
}