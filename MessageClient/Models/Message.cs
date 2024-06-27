using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace MessageClient.Models
{
  public class Message
  {
    public int MessageId { get; set; }
    public string Content { get; set; }
    public string Group { get; set; }
    public DateTime PostTime { get; set; }
    public string user_name { get; set; }

    public static List<Message> GetMessages()
    {
      Task<string> apiCallTask = ApiHelper.GetAll();
      string result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<Message> messageList = JsonConvert.DeserializeObject<List<Message>>(jsonResponse.ToString());

      return messageList;
    }

    public static Message GetDetails(int id)
    {
      Task<string> apiCallTask = ApiHelper.Get(id);
      string result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
      Message message = JsonConvert.DeserializeObject<Message>(jsonResponse.ToString());
      return message;
    }

    public static void Post(Message message)
    {
      string jsonMessage = JsonConvert.SerializeObject(message);
      ApiHelper.Post(jsonMessage);
    }

    public static void Put(Message message, string userName)
    {
      string jsonMessage = JsonConvert.SerializeObject(message);
      ApiHelper.Put(message.MessageId, jsonMessage, userName);
    }

    public static void Delete(int id, string userName)
    {
      ApiHelper.Delete(id, userName);
    }
  }
}