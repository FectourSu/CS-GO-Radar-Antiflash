using Parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using СS_GO_Launcher.Helpers;
using СS_GO_Launcher.Models.ParserObjects;
using System.Windows.Forms;
using СS_GO_Launcher.ParserTemplate;

namespace СS_GO_Launcher
{
    class Program
    {

        static Program()
        {
            Resolver.RegisterDependencyResolver();
        }

        static void SerealizeOffsets(string fileName, IEnumerable<ParserResult> values)
        {
            string offsetsConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (File.Exists(offsetsConfig))
            {
                File.Delete(offsetsConfig);
            }
           
            foreach (var item in values)
            {
                File.AppendAllText(offsetsConfig, item.Value + "\n");
            }
        }

        static IEnumerable<ParserResult> FixValue(IEnumerable<ParserResult> values)
        {
            foreach (var item in values)
            {
                item.Value = Regex.Replace(item.Value, @"[^a-zA-Z0-9\._-]", string.Empty);
            }

            return values;
        }

        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            string mainProgram = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodyCSGO bSpotted Radar.exe");

            if (!File.Exists(mainProgram))
            {
                openFile.Filter = "Выберите exe чита (*.exe)|*.exe";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    
                    mainProgram = openFile.FileName;
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            IParserHazedumper parser = new ParserHazedumper();
            var process = new ProcessRunner();

            parser.Working += (s, e) =>
            {
                Console.WriteLine($"\tLoaded {e}...");
            };

            try
            {
                Console.WriteLine($"Сheck for updates:");

                IEnumerable<ParserResult> values = parser.RunAsync(
                    new EntityList(),
                    new Spostted(),
                    new LocalPlayer(),
                    new FlashDuration(),
                    new ClientDll())
                        .Result;

                SerealizeOffsets("offsets.json", FixValue(values));

                Console.WriteLine($"Runing: {mainProgram}");

                process.RunAsync(mainProgram)
                    .Wait();

                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}
