using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace HuntroxGames.Utils.DiscordWebhook
{
    
    public class WebService : Singleton<WebService>
    {
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
            return;

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
                        var index = i;
                        var fileData = webhook.attachments[index].fileData;

                        var multipartFormFile = CreateMultipartFormFileSection("files[" + index + "]", fileData,
                            webhook.attachments[index].filename);
                        multipartFormSections.Add(multipartFormFile);
                    }

                var webhookUrl = webhook.webhook_Url;
                var request = UnityWebRequest.Post(webhookUrl, multipartFormSections);

                var hasProgressCallback = webhook.onWebhookRequestProgress != null;
                
                
                request.SendWebRequest();

                
                while (!request.isDone)
                {
                    var progress = request.uploadProgress;
                    if (hasProgressCallback)
                        webhook.onWebhookRequestProgress?.Invoke(progress);
                    yield return null;
                }
                
                if (hasProgressCallback)
                    webhook.onWebhookRequestProgress?.Invoke(1.0f);

                var isError =
                    request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError;

                Debug.Log(isError
                    ? "Webhook request failed: " + request.error
                    : "Webhook request sent successfully!");


                webhook.onWebhookResponse?.Invoke(request.downloadHandler.text, isError);
                
                request.Dispose();
            }
        }

        private static MultipartFormFileSection CreateMultipartFormFileSection(string name,byte[] fileData, string filename)
        {
            var contentType = Path.GetExtension(filename) switch
            {
                ".txt" => "text/plain",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "file/" + Path.GetExtension(filename)
            };
            return new MultipartFormFileSection(name, fileData, filename, contentType);
        }
        
        
    }   

}