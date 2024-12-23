using System;
using System.IO;
using System.Text;
using Domain.Interfaces.Infrastructure.File;

namespace Infrastructure.File
{
    public abstract class FileOperations : IFileOperations
    {
        protected bool IsDirectoryValid(string path) => Directory.Exists(path);

        public virtual Stream Read(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new FileNotFoundException($"File not found: {path}", path);
            }

            try
            {
                Console.WriteLine("Чиатем");
                return new FileStream(path, FileMode.Open, FileAccess.Read);
            }
            catch (Exception ex)
            {
                throw new IOException($"Error reading file: {path}", ex);
            }
        }


        public virtual void Write(string path,string name, Stream data)
        {
            if (!IsDirectoryValid(path))
            {
                Console.WriteLine( path);
                Directory.CreateDirectory(path);
                throw new DirectoryNotFoundException($"Directory does not exist: {path}");
            }

            try
            {
                using (var fileStream = new FileStream(Path.Combine(path,name) , FileMode.Create, FileAccess.Write))
                {
                    data.CopyTo(fileStream);
                }
            Console.WriteLine("ЗАПИСАН ФАЙЛ");
            }
            catch (Exception ex)
            {
                throw new IOException($"Error writing to file: {path}", ex);
            }
        }
    }

  
}
