using System.Threading.Tasks;

namespace GRPCClientLib
{
    public interface IStreamHandler
    {
        Task InitStreamingAsync(string initModel);
    }
}