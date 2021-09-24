using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TRZ_WikimediaCount.Core.Repositories;

namespace TRZ_WikimediaCount.Application
{
    public class TempHandler : ITempHandler
    {
        private readonly string temp_FileName;
        private Dictionary<string, List<string>> cache_PageView;
        private readonly IConfiguration config;

        public TempHandler(IConfiguration config)
        {
            this.config = config;
            cache_PageView = new Dictionary<string, List<string>>();
            temp_FileName = Path.GetTempPath() + this.config["Temps:FileName"];

            if (this.config["Temps:Clean"].Equals("Y")) File.Delete(temp_FileName);
        }

        public List<string> GetPageView(string id)
        {
            if (cache_PageView.ContainsKey(id))
            {

                Console.WriteLine($"04.C {id} - Get from temp");
                return cache_PageView[id];
            }
            return null;
        }

        public void LoadPageView()
        {
            if (File.Exists(temp_FileName))
            {
                string pageViewCache = File.ReadAllText(temp_FileName);
                cache_PageView = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(pageViewCache);
            }
        }

        public void Refresh(string id, List<string> detalle)
        {
            Console.WriteLine($"04.C {id} - Saving in temp");

            cache_PageView[id] = detalle;
            SavePageView();
        }

        public void SavePageView()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string pageViewResults1String = JsonSerializer.Serialize(cache_PageView, options);
            File.WriteAllText(temp_FileName, pageViewResults1String);
        }
    }
}
