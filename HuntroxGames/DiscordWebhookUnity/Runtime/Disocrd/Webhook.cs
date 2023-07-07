using UnityEngine;

namespace HuntroxGames.Utils
{
    [System.Serializable]

    public class Webhook : IJson<Webhook>
    {
        
        public string username;
        public string avatar_url;
        public string content;
        public Embed[] embeds;
        public Attachment[] attachments;
        
        
        
        
        public string ToJson() => JsonUtility.ToJson(this);
        public Webhook FromJson(string json) => JsonUtility.FromJson<Webhook>(json);
    }
}