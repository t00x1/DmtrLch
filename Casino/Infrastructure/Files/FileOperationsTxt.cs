using System;
using System.IO;
using System.Text;
using Domain.Interfaces.Infrastructure.File;

namespace Infrastructure.File
{
    public class TextFileOperations : FileOperations, ITextFileOperation
    {    
        public string ReadText(string path)
        {
            string exeDirectory = AppContext.BaseDirectory;
            Console.WriteLine($"Executable File Directory: {exeDirectory}");
            try
            {
                using (var fileStream = Read(path))
                using (var reader = new StreamReader(fileStream, Encoding.UTF8)) 
                {
                    return reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException ex)
            {
                // Если файл не найден, выбрасываем исключение
                throw new InvalidOperationException($"File not found: {path}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                // Если нет прав на доступ к файлу
                throw new InvalidOperationException($"Access denied to file: {path}", ex);
            }
            catch (IOException ex)
            {
                // Обработка других ошибок ввода-вывода
                throw new InvalidOperationException($"Error reading the file: {path}", ex);
            }
            catch (Exception ex)
            {
                // Обработка непредвиденных ошибок
                throw new InvalidOperationException($"An unexpected error occurred while reading the file: {path}", ex);
            }
        }

        public void WriteText(string path,string name, string text)
        {
           string exeDirectory = AppContext.BaseDirectory;
            Console.WriteLine($"Executable File Directory: {exeDirectory}");
            try
            {
                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(text))) 
                {
                    Write(path,name, memoryStream);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                // Если нет прав на запись в файл
                throw new InvalidOperationException($"Access denied to file: {path}", ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                // Если директория не найдена
                throw new InvalidOperationException($"Directory not found: {path}", ex);
            }
            catch (IOException ex)
            {
                // Обработка других ошибок ввода-вывода
                throw new InvalidOperationException($"Error writing to the file: {path}", ex);
            }
            catch (Exception ex)
            {
                // Обработка непредвиденных ошибок
                throw new InvalidOperationException($"An unexpected error occurred while writing to the file: {path}", ex);
            }
        }
    }
}
