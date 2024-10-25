using System;
using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]

    public class Webhook : IJson<Webhook>
    {
        
        public delegate void OnWebhookResponse(string responseBody, bool isError);
        public delegate void OnWebhookRequestProgress(float progress);
        
        
        [NonSerialized]public string webhook_Url;
        public string username;
        public string avatar_url;
        public string content;
        public Embed[] embeds;
        public Attachment[] attachments;
        /// <summary>
        /// set this if you want to receive a response
        /// returns response text and if it was successful or not
        /// </summary>
        public OnWebhookResponse onWebhookResponse;
        /// <summary>
        /// set this if you want to receive a webhook request progress
        /// </summary>
        public OnWebhookRequestProgress onWebhookRequestProgress;
        
        public string thread_name;

        public string ToJson()
        {
            attachments ??= Array.Empty<Attachment>();
            embeds ??= Array.Empty<Embed>();
            for (var i = 0; i < attachments.Length; i++)
            {
                var index = i;
                attachments[index].id = index;
                attachments[index].LoadFile();
            }
            foreach (var embed in embeds) 
            {
                embed.RgbColorToInt();
                if (!attachments.IsNullOrEmpty())
                    embed.GetAttachments(attachments);
            }
            return JsonUtility.ToJson(this);
        }

        public Webhook FromJson(string json) => JsonUtility.FromJson<Webhook>(json);
        
        
        public Webhook AddEmbed(Embed embed)
        {
            embeds ??= Array.Empty<Embed>();
            var newEmbeds = new Embed[embeds.Length + 1];
            embeds.CopyTo(newEmbeds, 0);
            newEmbeds[embeds.Length] = embed;
            embeds = newEmbeds;
            return this;
        }
        public Webhook SetContent(string content)
        {
            this.content = content;
            return this;
        }

        /// <summary>
        /// Note: Webhooks can only create threads in forum channels
        /// </summary>
        /// <param name="thread_name"></param>
        /// <returns></returns>
        public Webhook SetThreadName(string thread_name)
        {
            this.thread_name = thread_name;
            return this;
        }
        
        

        public Webhook SetAuthor(string username, string avatar_url="")
        {
            this.username = username;
            this.avatar_url = avatar_url;
            return this;
        }

        public Webhook SetEmbeds(Embed[] embeds)
        {
            this.embeds = embeds;
            return this;
        }
        
        public Webhook AddAttachment(string filePath,string referenceKey,string description = "") 
            => AddAttachment(new Attachment(filePath, referenceKey, description));
        public Webhook AddTextAttachmentRes(string filePath, string referenceKey,string description = "")
            => AddAttachment(new TextAttachment(filePath, referenceKey,description));
        public Webhook AddImgAttachmentRes(string filePath, string referenceKey,string description = "")
            => AddAttachment(new ImageAttachment(filePath, referenceKey,description));

        public Webhook AddAttachment(Attachment attachment)
        {
            attachments ??= Array.Empty<Attachment>();
            var newAttachments = new Attachment[attachments.Length + 1];
            attachments.CopyTo(newAttachments, 0);
            newAttachments[attachments.Length] = attachment;
            attachments = newAttachments;
            return this;
        }
        
        public Webhook SetAttachments(Attachment[] attachments)
        {
            this.attachments = attachments;
            return this;
        }
        
        public Webhook SendWebhook(string webhook_Url)
        {
            this.webhook_Url = webhook_Url;
            WebService.SendDiscordWebhook(this);
            return this;
        }

        public Webhook SetResponseCallback(OnWebhookResponse callback)
        {
            onWebhookResponse = callback;
            return this;
        }
        
        public Webhook SetProgressCallback(OnWebhookRequestProgress callback)
        {
            onWebhookRequestProgress = callback;
            return this;
        }
    }
}