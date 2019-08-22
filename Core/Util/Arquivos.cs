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
            try
            {
                string path = $"{traj}{caminho}.json";
                if (!File.Exists(path)) File.Create(path).Close();
                using (StreamReader s = File.OpenText(path))
                {
                    var file = File.ReadAllText(path); 
                    generico = JsonConvert.DeserializeObject<T>(file);
                    return generico;
                }
            }catch(JsonException ex)
            {
                Console.WriteLine($"Erro na deserialização: {ex.Message}");
                return default; 
            }
        }
        public static void Salvar(T generico, string caminho)
        {
            try
            {
                string path = $"{traj}{caminho}.json";
                using (StreamWriter file = File.CreateText(path))
                {
                    string strResultadoJson = JsonConvert.SerializeObject(generico);
                    file.Write($"{strResultadoJson}");
                }
            }
            catch(JsonException ex)
            {
                Console.WriteLine($"Erro na Serialização do Objeto: {ex.Message}");
            }
        }
    }
}