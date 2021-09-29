using ICSharpCode.SharpZipLib.GZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using TRZ_WikimediaCount.Core.Repositories;

namespace TRZ_WikimediaCount.Application
{
    public class StreamHandler : IStreamHandler
    {
        private const int DefaultBufferSize = 4096;
        private const int DefaultTimeout = 10;
        private readonly IHttpClientFactory httpClient;
        public StreamHandler(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory;
        }

        public IEnumerable<string> ReadFromUrlandDecompress(Uri url)
        {
            Stream contentStream = DownloadFileAsync(url)
                                 .GetAwaiter()
                                 .GetResult();
            return GetDataLinesFromStream(contentStream);
        }

        private async Task<Stream> DownloadFileAsync(Uri url)
        {
            var client = httpClient.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(DefaultTimeout);
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();
                using Stream content = await response.Content.ReadAsStreamAsync();
                return await DecompressGZip(content);
            }
        }
        public async Task<Stream> DecompressGZip(Stream sm)
        {
            var extractedStream = new MemoryStream();
            using (GZipInputStream gzipStream = new GZipInputStream(sm))
            {
                await gzipStream.CopyToAsync(extractedStream, DefaultBufferSize);
            }
            extractedStream.Seek(0, SeekOrigin.Begin);
            return extractedStream;
        }
        private IEnumerable<string> GetDataLinesFromStream(Stream fileContentStream)
        {
            var rows = new List<string>();
            using (fileContentStream)
            using (var file = new StreamReader(fileContentStream))
                while (!file.EndOfStream)
                    rows.Add(file.ReadLine());

            return rows;
        }

    }
}
