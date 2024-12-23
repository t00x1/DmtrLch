using System;
using System.IO;

namespace Domain.Interfaces.Infrastructure.File
{
    public interface IFileOperations
    {
       public Stream Read(string path);
       void Write(string path,string name, Stream data);
    }
}