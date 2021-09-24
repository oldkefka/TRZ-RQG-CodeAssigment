using TRZ_WikimediaCount.Controller;

namespace TRZ_WikimediaCount
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = new Config();
            var services = Initializer.Set();
            Process.Exec(config, services);
        }
    }
}
