using Infrastructure.LUIS.Models;
using System.Threading.Tasks;

namespace Infrastructure.LUIS
{
    public interface ILUISClient
    {
        Task<LUISResponse> GetResponse(LUISRequest request);
    }
}
