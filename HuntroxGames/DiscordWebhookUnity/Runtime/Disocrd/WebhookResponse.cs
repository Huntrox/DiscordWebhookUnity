using System;
using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [Serializable]
    public class WebhookResponse : IJson<WebhookResponse>
    {
        public int type;
        public string content;
        public bool tts;
        public bool allowed_mentions;
        public int flags;
        public Author author;
        public string timestamp;
        public bool pinned;
        public string webhook_id;
        public string channel_id;
        public string id;
        public Embed[] embeds = Array.Empty<Embed>();
        public Attachment[] attachments = Array.Empty<Attachment>();
        
        


        public WebhookResponse FromJson(string json) => JsonUtility.FromJson<WebhookResponse>(json);
        public string ToJson() => JsonUtility.ToJson(this);

    }
}