using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace СS_GO_Launcher
{
    public class ProcessRunner
    {
        public event EventHandler ProcessExited;

        public async Task<int> RunAsync(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            string fullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            bool isExists = await Task.Run(() => File.Exists(fullName));

            if (!isExists)
            {
                throw new FileNotFoundException($"{fullName} file not exist");
            }

            try
            {
                Process process = new Process();

                process.StartInfo.FileName = fullName;
                process.EnableRaisingEvents = true;

                process.Exited += (sender, e) =>
                {
                    ProcessExited?.Invoke(sender, e);
                };

                process.Start();

                return process.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return -1;
            }
        }
    }
}
