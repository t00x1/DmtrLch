
using Domain.Interfaces.Infrastructure.File;
namespace Infrastructure.File
{
    public class FileOperationsImage : FileOperations, IImageFileOperations
    {
        public Stream ReadImage(string path)
        {
            string exeDirectory = AppContext.BaseDirectory;
            Console.WriteLine($"Executable File Directory: {exeDirectory}");
            Console.WriteLine($" Directory: {path}");

            return Read(path);
        }
    
        public void WriteImage(string path,string name, Stream stream)
        {
            string exeDirectory = AppContext.BaseDirectory;
            Console.WriteLine($"Executable File Directory: {exeDirectory}");
            Write(path, name, stream);
        }
    }
}