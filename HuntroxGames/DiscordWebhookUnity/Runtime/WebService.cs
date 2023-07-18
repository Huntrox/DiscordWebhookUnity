using System.Collections;
using System.Collections.Generic;
using System.IO;
using HuntroxGames.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace HuntroxGames.Utils
{
    
    public class WebService : Singleton<WebService>
    {    
        public const string DISCORD_WEBHOOK = "";

        protected override void Awake()
        {
            destroyOnLoad = false;
            base.Awake();
            if (gameObject.hideFlags != HideFlags.HideInHierarchy) 
                gameObject.hideFlags = HideFlags.HideInHierarchy;
            
        }
        
        
        public static void SendDiscordWebhook(Webhook webhook) => Instance.DiscordWebhook(webhook);

        private void DiscordWebhook(Webhook webhook)
        {
            StartCoroutine(DiscordWebhookProcess(webhook));

            IEnumerator DiscordWebhookProcess(Webhook webhook)
            {
                
                var payloadJsonBytes = Utils.JsonToByteArray(webhook);
                
                var multipartFormSections = new List<IMultipartFormSection>
                {
                    new MultipartFormDataSection("payload_json", payloadJsonBytes)
                };


                if (!webhook.attachments.IsNullOrEmpty())
                    for (var i = 0; i < webhook.attachments.Length; i++)
                    {
                        var filePath = webhook.attachments[i].filePath;
                        var index = i;
                        var fileData = File.ReadAllBytes(filePath);
                        
                        multipartFormSections.Add(new MultipartFormFileSection("files[" + index + "]", fileData, webhook.attachments[index].filename, "image/jpeg"));
                    }

                var webhookUrl = DISCORD_WEBHOOK;
                var request = UnityWebRequest.Post(webhookUrl, multipartFormSections);
                
                using (request)
                {
                    yield return request.SendWebRequest();
                    if (request.result == UnityWebRequest.Result.ConnectionError
                        || request.result == UnityWebRequest.Result.ProtocolError)
                    {
                        Debug.Log("Webhook request failed: " + request.error);
                        Debug.Log(request.downloadHandler.text);
                    }
                    else
                    {
                        Debug.Log("Webhook request sent successfully!");
                        Debug.Log(request.downloadHandler.text);

                    }
                }
            }
        }
    }   

}