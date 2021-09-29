using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace TRZ_WikimediaCount.Controller.Interfaces
{
   public interface IStreamAux
    {
        MemoryStream ReadFromUrlandDecompress(Uri url);
        GZipStream Decompress(Stream stream);
        List<String> ReadLines(Stream decompress, Encoding encoding);
    }
}
