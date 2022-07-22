using System.Net;
using System.Threading.Tasks;
using TFLRoadStatus.BL.Model;

namespace TFLRoadStatus.BL.Interface
{
    public interface IRestClient
    {
        HttpStatusCode StatusCode { get; set; }
        Task<string> Get(RoadStatusQuery roadStatusQuery);
    }
}
