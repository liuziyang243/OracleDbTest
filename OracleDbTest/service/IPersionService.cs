using System.Collections.Generic;
using OracleDbTest.entity;

namespace OracleDbTest.service
{
    public interface IPersionService
    {
        List<PersonDo> GetPersonList();

        PersonDo GetPerson(int id);

        void SaveSchools(PersonDo person);
    }
}