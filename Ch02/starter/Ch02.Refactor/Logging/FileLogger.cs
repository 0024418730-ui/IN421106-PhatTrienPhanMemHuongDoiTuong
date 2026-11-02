using System;
using System.Collections.Generic;
using System.Text;

namespace EduHub.Ch02.Logging
{
    public class FileLogger : IAppLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath = "club-log.txt")
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            File.AppendAllText(_filePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n");
        }
    }
}
