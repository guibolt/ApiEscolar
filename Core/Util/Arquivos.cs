using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Util
{
    public static class Arquivos<T>
    {

        static string traj = AppDomain.CurrentDomain.BaseDirectory;
        public static T Recuperar(T generico, string caminho)
        {
            string path = $"{traj}{caminho}.json";
            if (!File.Exists(path)) File.Create(path).Close();
            using (StreamReader s = File.OpenText(path))
            {
                var file = File.ReadAllText(path);
                var paut = JsonConvert.DeserializeObject<T>(file);
                return paut;
            }
        }
        public static void Salvar(T generico, string caminho)
        {
            string path = $"{traj}{caminho}.json";
            if (!File.Exists(path)) File.Create(path).Close();

            using (StreamWriter s = File.AppendText(path))
            {
                string G = JsonConvert.SerializeObject(generico);
                s.WriteLine(G);
            }
        }
    }
}