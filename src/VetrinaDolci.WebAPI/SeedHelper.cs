using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VetrinaDolci.WebAPI.Models;

namespace VetrinaDolci.WebAPI
{
    public static class SeedHelper
    {
        public static async Task SeedFromCsv(ApplicationContext db)
        {
            // Note: This sample requires the database to be created before running.
            Console.WriteLine($"Database path: {db.DbPath}.");
            Console.WriteLine();

            // add Ingredienti from csv.
            var reader = new StreamReader(File.OpenRead("Tabella.csv"));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                Console.WriteLine("Parse Tabella Ingredienti: {0}", line);

                db.Ingredienti.Add(new Ingrediente
                {
                    Id = int.Parse(values[0]),
                    Nome = values[1].Replace("\"", ""),
                    Proteine = double.Parse(values[2]),
                    Zuccheri = double.Parse(values[3]),
                    Grassi = double.Parse(values[4]),
                    Colesterolo = double.Parse(values[5]),
                    Fibra = double.Parse(values[6]),
                    Kcal = double.Parse(values[7])
                });
            }
            reader.Close();
            await db.SaveChangesAsync();

            reader = new StreamReader(File.OpenRead("Ricette.csv"));
            List<Dolce> listaDolci = new List<Dolce>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split('@');
                if (values[2].Replace("\"", "") == "Dessert")
                {
                    Console.WriteLine("Parse SOLO Ricette Dessert: {0}, {1}", values[0], values[1]);
                    string noteIngrediente = null;
                    Dolce dolce = new Dolce
                    {
                        Nome = values[1].Replace("\"", ""),
                        TipoPiatto = values[2].Replace("\"", ""),
                        IngPrincipale = values[3].Replace("\"", ""),
                        Persone = int.Parse(values[4].Replace("\"", "")),
                        IngredientiDolce = new List<IngredientiDolce>(),
                        Preparazione = values[6].Replace("\"", ""),
                        Note = values[8].Replace("\"", "")
                    };
                    listaDolci.Add(dolce);

                    foreach (var item in values[5].Replace("\"", "").Split(';'))
                    {
                        IngredienteCsv result = GetIngrediente(item);
                        if (result == null)
                        {
                            noteIngrediente = item.Trim();
                            continue;
                        }

                        // se non presente inserisci in tabella
                        Ingrediente ingrediente = db.Ingredienti
                            .Where(w => w.Nome == result.Nome)
                            .FirstOrDefault(); ;
                        if (ingrediente == null)
                        {
                            ingrediente = new Ingrediente
                            {
                                Nome = result.Nome
                            };
                            db.Ingredienti.Add(ingrediente);
                            await db.SaveChangesAsync();
                        }

                        // crea relazione dolce-ingrediente
                        dolce.IngredientiDolce.Add(new IngredientiDolce
                        {
                            IngredienteId = ingrediente.Id,
                            Quantita = result.Quantita,
                            UnitaDiMisura = result.UnitaDiMisura,
                            Note = noteIngrediente
                        });
                    }
                }
            }
            reader.Close();
            db.Dolci.AddRange(listaDolci);
            await db.SaveChangesAsync();

            Console.WriteLine("Seed Done");
        }

        private static IngredienteCsv GetIngrediente(string item)
        {
            // Parse campo Ingredienti
            string quantita = null;
            string unitaDiMisura = null;
            string nome = null;
            string regexSpecialChar = @"\'|\(|\)|\.|\/\%\+";

            string l = item.Trim().Replace("�", "");
            // sample: "Per Guarnire:", "Per La Placca:", ...
            if (Regex.Match(l, @$"^[\=|\s]*([\w|\s|{regexSpecialChar}]*)\:$").Success)
            {
                return null;
            }
            // sample: "1 Panetto Lievito Di Birra"
            Match match = Regex.Match(l, @$"^(\d*)\s*(\w*)\s*([\w|\s|{regexSpecialChar}]*)$");
            if (match.Success)
            {
                quantita = match.Groups[1].Value;
                unitaDiMisura = match.Groups[2].Value;
                nome = match.Groups[3].Value;
            }
            else
            {
                // sample: "1/2   Limone", "     Albumi D'uovo", "2 Albumi D'uovo", "2 1/2 Bicchieri   Acqua"
                match = Regex.Match(l, @$"^([\d|\/]*||\d\s[\d|\/]*)\s*([\w|\s|{regexSpecialChar}]*)$");
                if (match.Success)
                {
                    quantita = match.Groups[1].Value;
                    nome = match.Groups[2].Value;
                }
                else
                {
                    // sample: "Zucchero: Mezzo Del Peso Dei Fichi"
                    match = Regex.Match(l, @$"^([\w|\s]*)\:\s*([\w|\s|{regexSpecialChar}]*)$");
                    if (match.Success)
                    {
                        quantita = match.Groups[2].Value;
                        nome = match.Groups[1].Value;
                    }
                    else
                    {
                        throw new Exception("Regex missing or in error");
                    }
                }
            }

            return new IngredienteCsv { Quantita = quantita, UnitaDiMisura = unitaDiMisura, Nome = nome };
        }

        public class IngredienteCsv
        {
            public string Quantita { get; set; }
            public string UnitaDiMisura { get; set; }
            public string Nome { get; set; }
        }
    }
}
