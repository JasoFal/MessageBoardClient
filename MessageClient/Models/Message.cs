using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MessageClient.Models
{
  public class MessageClient
  {
    public int MessageId { get; set; }
    public string Content { get; set; }
    public string Group { get; set; }
    public DateTime PostTime { get; set; }
    public string user_name { get; set; }

    public static List<Message> GetMessages()
    {
      var apiCallTask = ApiHelper.GetAll();
      var result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.Deserialized<JArray>(result);
      List<Message> messageList = JsonConvert.DeserializedObject<List<Message>>(jsonResponse.ToString());

      return messageList;
    }

    
  }
}