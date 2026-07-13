using System.Threading.Tasks;

namespace BitirmeProjesiLiman.Service.Services
{
    public interface IAIService
    {
        Task<string> AskGeminiAsync(string prompt);
    }
}
