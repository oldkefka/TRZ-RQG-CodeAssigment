using TRZ_WikimediaCount.Command.Controller;
using TRZ_WikimediaCount.Core;

namespace TRZ_WikimediaCount.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Initializer.Set();//Initialice interfaces and get config
            Process.Exec(services);//Process begin
        }
    }
}
