using HuntroxGames.Utils.DiscordWebhook;
using UnityEngine;

namespace HuntroxGames.Utils
{
    public class WebhookSendExample : MonoBehaviour
    {
        public string myWebhookURl;
        public Webhook webhook;
        [Header("From Texture2D Example")]
        public Texture2D texture2D;
        
        
        private void Start() 
            => Send();
        
        public void Send() 
            => webhook.SendWebhook(myWebhookURl);
        
        public void SendWebHook()
        {
            var discordWebhook = new Webhook()
                .SetAuthor("My Webhook name", "https://avatars.githubusercontent.com/u/34078403?v=4")//author of the webhook
                .SetContent("this webhook contains img uploaded from Resources Folder")//text content of the webhook
                .AddAttachment(Application.dataPath+"/StreamingAssets/ExampleFiles/exampleTxtFile.txt","myExampleFile","File Attachment Loaded From StreamingAssets")
                .AddImgAttachmentRes("Files/ExampleJpegResourceFile","myExampleImage", "Example Img Attachment Loaded From Resources Folder")//adding img attachment from Resources
                .AddEmbed(Embed.CreateEmbed("HuntroxGames", "this is an example embed", Color.red)
                    .CreateNewField("this is an example inline field A", "this is an example value", true)
                    .CreateNewField("this is an example inline field B", "this is an example value", true)
                    .SetImage("myExampleImage")//referencing img attachment with unique reference key
                    .SetFooter("HuntroxGames", "https://avatars.githubusercontent.com/u/34078403?v=4"))
                .AddEmbed(Embed.CreateEmbed("Just another embed", "just another embed description", Color.blue)
                    .CreateThumbnail("myExampleImage"))
                .SetResponseCallback(Debug.Log);//log the response
            discordWebhook.SendWebhook(myWebhookURl);
        }

        public void SendWebHookWithTexture2D()
        {
            var discordWebhook = new Webhook()
                .SetAuthor("My Webhook name")
                .SetContent("this webhook contains img uploaded from Texture2D")
                .AddAttachment(ImageAttachment.FromTexture2D(texture2D, "myExampleImage"));
            discordWebhook.SendWebhook(myWebhookURl);
        }

        public void DemoWebhookCont()
        {
            var discordWebhook = new Webhook()
            {
                username = "My Webhook name",
                avatar_url = "https://avatars.githubusercontent.com/u/34078403?v=4",
                content = "this webhook contains img uploaded from Resources Folder",
                attachments = new Attachment[]
                {
                    new Attachment(Application.dataPath + "/StreamingAssets/ExampleFiles/exampleTxtFile.txt", "myExampleFile"),
                    new ImageAttachment("Files/ExampleJpegResourceFile", "myExampleImage")
                },
                embeds = new Embed[]
                {
                    new Embed()
                    {
                        title = "HuntroxGames",
                        description = "this is an example embed",
                        embedColor = Color.red,
                        fields = new Field[]
                        {
                            new Field("this is an example inline field A", "this is an example value", true),
                            new Field("this is an example inline field B", "this is an example value", true)
                        },
                        image = new Image
                        {
                            url = "myExampleImage"
                        },
                        footer = new Footer
                        {
                            text = "HuntroxGames",
                            icon_url = "https://avatars.githubusercontent.com/u/34078403?v=4"
                        }
                    },
                },
            };
            discordWebhook.SendWebhook(myWebhookURl);
        }
    }
}
