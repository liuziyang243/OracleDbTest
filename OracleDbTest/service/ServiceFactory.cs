namespace OracleDbTest.service
{
    public class ServiceFactory
    {
        public static IPersionService GetPersionService()
        {
            return new PersonService();
        }
    }
}