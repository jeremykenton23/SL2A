// TextAnalyzerService.cs
using System.Threading.Tasks;

namespace WpfApp1.Services
{
    public class TextAnalyzerService : ITextAnalyzerService
    {
        public async Task<int> CountCharactersAsync(string text)
        {
            await Task.Delay(1000); // Simuleer een langlopend proces
            return text.Length;
        }
    }
}
