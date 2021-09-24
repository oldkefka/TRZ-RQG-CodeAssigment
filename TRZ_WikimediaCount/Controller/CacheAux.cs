using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TRZ_WikimediaCount.Controller.Interfaces;

namespace TRZ_WikimediaCount.Controller
{
    public class CacheAux : ICacheAux
    {
        private const string cache_FileName = "page-view-wik2";
        private Dictionary<string, List<string>> cache_PageView;

        public CacheAux()
        {
            cache_PageView = new Dictionary<string, List<string>>();
        }

        public List<string> GetPageView(string id)
        {
            if (cache_PageView.ContainsKey(id)) { 
            
                    Console.WriteLine($"-----03.C {id} - Recuperado de cache-----");
                return cache_PageView[id];

            }

            return null;
        }

        public void LoadPageView()
        {
            if (File.Exists(cache_FileName))
            {
                string pageViewCache = File.ReadAllText(cache_FileName);
                cache_PageView = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(pageViewCache);
            }
        }

        public void Refresh(string id, List<string> detalle)
        {
            Console.WriteLine($"-----03.C {id} - Guardando en cache-----");

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

            File.WriteAllText(cache_FileName, pageViewResults1String);
        }
    }
}
