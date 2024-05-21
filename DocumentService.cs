using System.IO;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class AsyncDocumentService : IDocumentService
    {
        public async Task<string> OpenDocumentAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        public async Task SaveDocumentAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}
