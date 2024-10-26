# DiscordWebhookUnity
DiscordWebhookUnity is a Unity Tool that allows you to send Discord webhook messages directly from your Unity projects. This package supports sending messages with content, embeds, and attachments such as images and text files.

![img](https://i.imgur.com/SFLGpMf.png)

# Features

- Send Discord webhook messages with content, embeds, and attachments.
- Support for image and text file attachments.
- Support for sending multiple embeds and attachments in a single message.
- Easy-to-use API for creating and sending webhook messages.
- Callback support for handling webhook responses. 
- Progress callback when processing requests.
- Discord timestamp conversion utility function. 
- Example code and scenes.


# Installation

1. Clone or download the repository or download the latest [release](https://github.com/Huntrox/DiscordWebhookUnity/releases/download/v1.0.0/DiscordWebhookForUnity_1.0.0.unitypackage).
2. Copy the `HuntroxGames` folder into your Unity project's `Assets` directory.

# Usage

### Getting Webhook URL

To get the webhook URL you need to go to your Discord server settings and copy the webhook URL. 
for more information see [Discord documentation](https://support.discord.com/hc/en-us/articles/228383668-Intro-to-Webhooks)

### Simple Webhook

Easy way to send a webhook message using helpful methods:
```csharp
    var webhook = new Webhook()
        .SetAuthor("My Webhook name", "https://avatars.githubusercontent.com/u/34078403?v=4")
        .SetContent("This is a test message :smile:")
        .SendWebhook(myWebhookURl);
```
### Advanced Webhook

Construct a `Webhook` instance and have more control over its properties:
```csharp
    var webhook = new Webhook()
    {
        username = "My Webhook name",
        content = "This is a test message :smile:",
        avatar_url = "https://avatars.githubusercontent.com/u/34078403?v=4",
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
    webhook.SendWebhook(myWebhookURl);
```

### Adding Attachments

To include attachments in your webhook simply use the `AddAttachment` or `AddImgAttachmentRes` or `AddTextAttachmentRes` methods.
also note when adding Attachment, you need to provide a `referencekey` to identify the attachment and helps with referencing the attachment in the embed Thumbnail or Image fields.

```csharp
    var webhook = new Webhook()
        .SetAuthor("Bug Report Bot")
        .SetContent("USER INPUT")
        .AddAttachment(Application.persistentDataPath + "/player.log", "playerLogFile", "File Attachment")
        .AddImgAttachmentRes("Files/ExampleJpegResourceFile", "myExampleImage", "Image Attachment");
    webhook.SendWebhook(myWebhookURl);
```
![Imgur](https://i.imgur.com/ajMJlRM.png)

### Adding Embeds

To include embeds in your webhook messages:

```csharp
    var webhook = new Webhook()
        .SetAuthor("My Webhook name")
        .SetContent("This webhook contains an embed with img attachment")
        .AddImgAttachmentRes("Files/ExampleJpegResourceFile", "myExampleImage", "Image Attachment")
        .AddEmbed(Embed.CreateEmbed("Embed Title", "Embed Description", Color.red)
            .CreateNewField("Field Name", "Field Value", true)
            .CreateNewField("Another Field Name", "Another Field Value", true)
            .SetImage("myExampleImage")
            .SetFooter("Footer Text", "https://avatars.githubusercontent.com/u/34078403?v=4")
            .SetAuthor("This is the author","https://github.com/Huntrox","myExampleImage"));
    webhook.SendWebhook(myWebhookURl);
```
as you can see in `SetImage` we used the `referencekey: "myExampleImage"` to reference the attachment previously added with the same reference key.
this makes it possible to use the same attachment in multiple embeds. also 
providing an external URL to the image will work as well.
![Imgur](https://i.imgur.com/sXOPZcZ.png)
### Discord Timestamp Utility

this tool provides a utility function to convert a `DateTime` object to a Discord adding more to your webhook message.
```csharp
    var webhook = new Webhook()
        .SetAuthor("Time bot")
        .SetContent($"This webhook was sent {Utils.Utils.DateToDiscordTimestamp(DateTime.Now, DiscordTimestampFormat.Relative)}")
        .SendWebhook(webhookUrl);
```
![img](https://i.imgur.com/SqxmKRW.png)

# API Reference

### Webhook Class

- `SetAuthor(string username, string avatar_url = "")`: Sets the author of the webhook message.
- `SetContent(string content)`: Sets the content of the webhook message.
- `AddEmbed(Embed embed)`: Adds an embed to the webhook message.
- `AddAttachment(string filePath, string referenceKey, string description = "")`: Adds an attachment to the webhook message.
- `AddImgAttachmentRes(string filePath, string referenceKey, string description = "")`: Loads an image file from Resources as an attachment and adds it to the webhook message.
- `AddTextAttachmentRes(string filePath, string referenceKey, string description = "")`: Loads a text file from Resources as an attachment and adds it to the webhook message.
- `SetResponseCallback(Action<string,bool> callback)`: Sets the callback for webhook responses. 
- `SetProgressCallback(Action<float> callback)`: Sets the callback for webhook progress. Returns a floating-point value between 0.0 and 1.0, indicating the progress of uploading body data to the server.
- `SendWebhook(string webhook_Url)`: Sends the webhook message to the specified URL.


### Embed Class

- `CreateEmbed(string title, string description, Color color)`: Creates a new embed.
- `CreateNewField(string name, string value, bool inline)`: Adds a new field to the embed.
- `SetImage(string url)`: Sets the image of the embed. use the `referencekey` to reference the attachment previously added with the same reference key or **Direct URL**.
- `SetFooter(string text, string icon_url)`: Sets the footer of the embed.
- `SetThumbnail(string url)`: Sets the thumbnail of the embed. use the `referencekey` to reference the attachment previously added with the same reference key or **Direct URL**.
- `SetAuthor(string name, string url, string icon_url)`: Sets the author of the embed. use the `referencekey` in `icon_url` to reference the attachment previously added with the same reference key or **Direct URL**.
- `SetColor(Color color)`: changes the color of the embed.
### Attachment Types
- `Attachment`: Used to load file attachment directly from a file path. e.g. `"c:/path/to/file.txt"`.
- `TextAttachment`: Used to load text file attachment directly from Resources. e.g. `"Files/file.txt"`, files must be in the Resources folder. 
- `ImageAttachment`: Used to load image attachment directly from Resources. e.g. `"Files/file.png"`, files must be in the Resources folder.
- `ImageAttachment.FromTexture2D(Texture2D texture)`: Used to load image attachment directly from a Texture2D.
## License

This project is licensed under the MIT License. See the `LICENSE` file for details.