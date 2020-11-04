using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using FamilyLINQTraining.DataGeneration;
using FamilyLINQTraining.SolutionExample;
using Models;

namespace FamilyLINQTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            // CreateAndSeed();
            new Solutions().RunSolutions();
        }

        private static void CreateAndSeed()
        {
            IList<Family> families = new FamilyGenerator().GenerateFamilies(500);
            string famSerialized = JsonSerializer.Serialize(families, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText("families.txt", famSerialized);
            DBSeeder.Seed(families);
        }
    }
}