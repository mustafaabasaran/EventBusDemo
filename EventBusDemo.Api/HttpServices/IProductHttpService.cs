using System.Threading.Tasks;

namespace EventBusDemo.Api.HttpServices
{
    public interface IProductHttpService
    {
        Task<object> GetList();
    }
}