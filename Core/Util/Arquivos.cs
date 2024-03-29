﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Util
{
    public static class Arquivos
    {

        static string traj = AppDomain.CurrentDomain.BaseDirectory;
        public static T Recuperar<T>(T generico)
        {
            try
            {

                string path = $"{traj}Turmas.json";
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
        public static void Salvar<T>(T generico)
        {

            try
            {
                string path = $"{traj}Turmas.json";
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