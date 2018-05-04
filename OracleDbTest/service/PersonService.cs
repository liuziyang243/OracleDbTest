using System.Collections.Generic;
using OracleDbTest.entity;
using OracleDbTest.orm;

namespace OracleDbTest.service
{
    public class PersonService : IPersionService
    {
        private IDataAccessor dataAccessor = OrmEntryFactory.GetDataAccessor();
        private IDataSetAccessor dataSet = OrmEntryFactory.GetDataSetAccessor();

        public List<Person2> GetPersionList()
        {
            List<Person> persons = dataSet.SelectList<Person>(null, null);
            List<Person2> resultList = new List<Person2>();
            foreach (var person in persons)
            {
                Person2 p = new Person2(person);
                p.Schools = GetSchoolsByPersionId(p.Id);
                resultList.Add(p);
            }

            return resultList;
        }

        public void SaveSchools(Person2 person)
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
/*            string sql = "SELECT school_id FROM person_school_map WHERE person_id=:id";
            Dictionary<string, object> parms = new Dictionary<string, object> {{":id", id}};
                        List<int> schoolIdList = dataAccessor.queryColumnList<int>(sql, parms);*/
            List<int> schoolIdList = dataSet.GetColumnList<int>(table, column, condition, id);
            return schoolIdList;
        }

        private void DelSchoolIdForPerson(int pid, int sid)
        {
            string table = "person_school_map";
            string condition = "person_id=? AND school_id=?";
            dataSet.DelData(table, condition, pid, sid);

            /*string sql = "DELETE FROM person_school_map WHERE school_id=:sid AND person_id=:pid";
            Dictionary<string, object> parms = new Dictionary<string, object> {{":sid", sid}, {":pid", pid}};
            dataAccessor.update(sql, parms);*/
        }

        private void AddSchoolIdForPerson(int pid, int sid)
        {
            string table = "person_school_map";
            Dictionary<string, object> parms = new Dictionary<string, object> {{"person_id", pid}, {"school_id", sid}};
            dataSet.InsertColumnData(table, parms);

            /*string sql = "INSERT INTO person_school_map(person_id, school_id) VALUES(:pid, :sid)";
            Dictionary<string, object> parms = new Dictionary<string, object> {{":pid", pid}, {":sid", sid}};
            dataAccessor.update(sql, parms);*/
        }
    }
}