using System.Threading.Tasks;
using RestSharp;

namespace MessageClient.Models
{
  public class ApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("http://localhost:5001/");
      RestRequest request = new RestRequest($"api/messages", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("http://localhost:5001/");
      RestRequest request = new RestRequest($"api/messages/{id}", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async void Post(string newMessage)
    {
      RestClient client = new RestClient("http://localhost:5001/");
      RestRequest request = new RestRequest($"api/messages", Method.Post);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newMessage);
      await client.PostAsync(request);
    }
  }
}