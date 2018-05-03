namespace OracleDbTest.orm
{
    public class OrmEntryFactory
    {
        public static IDataAccessor GetDataAccessor()
        {
            return new DefaultDataAccessor();
        }

        public static IDataSetAccessor GetDataSetAccessor()
        {
            return new DefaultDataSetAccessor();
        }
    }
}