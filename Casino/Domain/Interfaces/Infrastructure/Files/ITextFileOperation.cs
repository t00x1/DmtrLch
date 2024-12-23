using System;
using System.IO;
using System.Text;


namespace Domain.Interfaces.Infrastructure.File
{
   
    public interface ITextFileOperation
    {
        string  ReadText(string path);
        void WriteText(string path,string name, string text);
    }
}

