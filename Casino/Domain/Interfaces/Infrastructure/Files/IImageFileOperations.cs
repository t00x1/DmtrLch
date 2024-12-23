namespace Domain.Interfaces.Infrastructure.File
{
    public interface IImageFileOperations
    {
        Stream ReadImage(string path);
        void WriteImage(string path,string name, Stream stream);
    }
}