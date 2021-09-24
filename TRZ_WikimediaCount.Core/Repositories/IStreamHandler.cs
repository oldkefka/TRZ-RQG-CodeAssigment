using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TRZ_WikimediaCount.Core.Entities;

namespace TRZ_WikimediaCount.Core.Repositories
{
   public interface IStreamHandler
    {

        IEnumerable<String> ReadFromUrlandDecompress(Uri url);
    }
}
