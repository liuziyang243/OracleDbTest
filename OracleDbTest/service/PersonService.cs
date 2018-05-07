using System.Collections.Generic;
using OracleDbTest.entity;
using OracleDbTest.orm;

namespace OracleDbTest.service
{
    public class PersonService : IPersionService
    {
        private IDataAccessor dataAccessor = OrmEntryFactory.GetDataAccessor();
        private IDataSetAccessor dataSet = OrmEntryFactory.GetDataSetAccessor();

        public List<PersonDo> GetPersonList()
        {
            List<Person> persons = dataSet.SelectList<Person>(null);
            List<PersonDo> resultList = new List<PersonDo>();
            foreach (var person in persons)
            {
                PersonDo p = new PersonDo(person);
                p.Schools = GetSchoolsByPersionId(p.Id);
                resultList.Add(p);
            }

            return resultList;
        }

        public PersonDo GetPerson(int id)
        {
            var condition = "id=?";
            Person person = dataSet.Select<Person>(condition, id);
            if (person == null)
            {
                return null;
            }
            PersonDo p = new PersonDo(person);
            p.Schools = GetSchoolsByPersionId(p.Id);
            return p;
        }

        public void SaveSchools(PersonDo person)
        {
            List<int> oldSchoolIds = GetSchoolIdsByPersionId(person.Id);
            List<int> newSchoolIds = new List<int>();
            foreach (var school in person.Schools)
            {
                newSchoolIds.Add(school.Id);
            }

            // 添加新关系
            foreach (var id in newSchoolIds)
            {
                if (!oldSchoolIds.Contains(id))
                {
                    AddSchoolIdForPerson(person.Id, id);
                }
            }

            // 删除去除的关系
            foreach (var id in oldSchoolIds)
            {
                if (!newSchoolIds.Contains(id))
                {
                    DelSchoolIdForPerson(person.Id, id);
                }
            }
        }

        private List<School> GetSchoolsByPersionId(int id)
        {
            var schoolIdList = GetSchoolIdsByPersionId(id);
            List<School> schools = new List<School>();
            if (null == schoolIdList)
            {
                return schools;
            }
            foreach (var sid in schoolIdList)
            {
                var temp = dataSet.Select<School>("id=?", sid);
                if (null != temp)
                {
                    schools.Add(temp);
                }
            }

            return schools;
        }

        private List<int> GetSchoolIdsByPersionId(int id)
        {
            string table = "person_school_map";
            string column = "school_id";
            string condition = "person_id=?";
            List<int> schoolIdList = dataSet.GetColumnList<int>(table, column, condition, id);
            return schoolIdList ?? new List<int>();
        }

        private void DelSchoolIdForPerson(int pid, int sid)
        {
            string table = "person_school_map";
            string condition = "person_id=? AND school_id=?";
            dataSet.DelData(table, condition, pid, sid);
        }

        private void AddSchoolIdForPerson(int pid, int sid)
        {
            string table = "person_school_map";
            Dictionary<string, object> parms = new Dictionary<string, object> {{"person_id", pid}, {"school_id", sid}};
            dataSet.InsertColumnData(table, parms);
        }
    }
}