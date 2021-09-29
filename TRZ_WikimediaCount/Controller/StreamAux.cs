using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using TRZ_WikimediaCount.Controller.Interfaces;

namespace TRZ_WikimediaCount.Controller
{
    class StreamAux : IStreamAux
    {

        public MemoryStream ReadFromUrlandDecompress(Uri url)
        {
            using (WebClient Client = new WebClient())
            {
                var output = new MemoryStream();
                using Stream stream = Client.OpenRead(url);
                using (var zipStream = new GZipStream(stream, CompressionMode.Decompress))
                {
                    zipStream.CopyTo(output);
                    zipStream.Close();
                    output.Position = 0;
                    return output;
                }
            }
        }

        public GZipStream Decompress(Stream stream)
        {
            using GZipStream unzip = new GZipStream(stream, CompressionMode.Decompress);
            return unzip;
        }

        public List<string> ReadLines(Stream stream,
                                     Encoding encoding)
        {
            List<string> lines = new List<string>();
            using (var reader = new StreamReader(stream, encoding))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }

            return lines;
        }


    }
}
