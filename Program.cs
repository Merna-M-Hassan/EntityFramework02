using Assignment_25_EF_Core.Context;

namespace Assignment_25_EF_Core
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using EventHubDbContext dbCoantext = new EventHubDbContext();   //Opens connection with DB.
        }
    }
}
