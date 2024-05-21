// ITextAnalyzerService.cs
using System.Threading.Tasks;

namespace WpfApp1.Services
{
    public interface ITextAnalyzerService
    {
        Task<int> CountCharactersAsync(string text);
    }
}
