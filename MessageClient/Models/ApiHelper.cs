using System.Threading.Tasks;
using RestSharp;

namespace MessageClient.Models
{
  public class ApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("http://localhost:5003/");
      RestRequest request = new RestRequest($"api/messages", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }
  }
}