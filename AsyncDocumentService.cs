using System.IO;
using System.Threading.Tasks;

namespace WpfApp1.Services
{
    public class AsyncDocumentService : IDocumentService
    {
        public Task<string> OpenDocumentAsync(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEndAsync();
                }
            }
            catch (IOException)
            {
                // Handel IO-fouten hier af
                return Task.FromResult(string.Empty);
            }
        }

        public Task SaveDocumentAsync(string filePath, string content)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    return writer.WriteAsync(content);
                }
            }
            catch (IOException)
            {
                // Handel IO-fouten hier af
                return Task.CompletedTask;
            }
        }
    }
}
