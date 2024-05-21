// IDocumentService.cs
using System.Threading.Tasks;

namespace WpfApp1
{
    public interface IDocumentService
    {
        Task<string> OpenDocumentAsync(string filePath);
        Task SaveDocumentAsync(string filePath, string content);
    }
}
