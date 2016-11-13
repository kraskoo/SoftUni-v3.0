namespace Exercises.Models.Writers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Interfaces;

    public class FileWriter : IOutputWriter
    {
        private const string DefaultResultDirectory = "../../Results";
        private const string DefaultProgramName = "Notepad.exe";
        private readonly string fileName;
        private string programPath;
        private bool hasSetToAppend;

        public FileWriter(
            string fileName,
            bool hasSetToAppend = false,
            string programPath = DefaultProgramName)
        {
            this.fileName = $"{DefaultResultDirectory}/{fileName}";
            this.hasSetToAppend = hasSetToAppend;
            this.programPath = programPath;
        }

        public void Write(string message)
        {
            if (this.programPath == DefaultProgramName)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.System);
                this.programPath = $"{path}/{this.programPath}";
            }

            if (!Directory.Exists(DefaultResultDirectory))
            {
                Directory.CreateDirectory(DefaultResultDirectory);
            }

            if (!this.hasSetToAppend && File.Exists(this.fileName))
            {
                File.Delete(this.fileName);
            }

            using (var writer = new StreamWriter(this.fileName, this.hasSetToAppend))
            {
                writer.Write(message);
            }

            if (!this.hasSetToAppend)
            {
                Process.Start(new ProcessStartInfo(this.programPath, this.fileName));
            }
        }
    }
}