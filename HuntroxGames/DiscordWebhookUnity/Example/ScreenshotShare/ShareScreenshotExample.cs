using System;
using HuntroxGames.Utils.DiscordWebhook;
using UnityEngine;

namespace HuntroxGames.Examples
{
    
    [RequireComponent(typeof(ScreenshotCapture))]
    public class ShareScreenshotExample : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ScreenshotCapture screenshotCapture;
        
        [Header("Webhook Settings")] 
        [SerializeField] private string webhookName = "Happy Bot";
        [SerializeField] private string webhookAvatarUrl = ""; 
        [SerializeField] private string webhookUrl = "";
        [SerializeField] private bool useEmbed = false;
        
        //listening to onCaptureFinished event on ScreenshotCapture component
        //when onCaptureFinished event is triggered it will output texture to OnCaptureFinished
        //we use this texture to create ImageAttachment and send it to webhook
        private void OnEnable() 
            => screenshotCapture.onCaptureFinished.AddListener(OnCaptureFinished);
        private void OnDisable() 
            => screenshotCapture.onCaptureFinished.RemoveListener(OnCaptureFinished);

        private void OnCaptureFinished(Texture2D texture)
        {
            //Creating ImageAttachment from Texture2D by using the utility function ImageAttachment.FromTexture2D
            
            var textureAttachment = ImageAttachment.FromTexture2D(texture, "MyScreenshot");

            var discordWebhook = new Webhook()
                .SetAuthor(webhookName, webhookAvatarUrl)
                .SetContent("Player Has Shared an Epic Moment!!")
                .AddAttachment(textureAttachment);
            
            //checking if using embed. If yes, create embed with Image and add it to webhook
            if (useEmbed)
            {
                var embed = new Embed()
                    .SetTitle("Epic Moment")
                    .SetImage("MyScreenshot");
                discordWebhook.AddEmbed(embed);
            }
            
            discordWebhook.SendWebhook(webhookUrl);
        }
    }
}
