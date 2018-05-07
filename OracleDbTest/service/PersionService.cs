using System.Collections.Generic;
using OracleDbTest.entity;

namespace OracleDbTest.service
{
    public interface IPersionService
    {
        List<PersonDo> GetPersionList();

        void SaveSchools(PersonDo persion);
    }
}