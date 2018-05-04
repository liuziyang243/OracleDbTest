namespace OracleDbTest.orm
/***************
 * @author: liuziyang
 * @version: v1.0
 *
 * @create time: 2018.05.03
 * @document: 工厂方法，用于同一提供接口实例
 */
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