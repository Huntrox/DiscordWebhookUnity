using System;
using System.Linq;
using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]
    public class Embed 
    {
        public string title;
        public string description;
        public string url;
        public Color embedColor;
        [HideInInspector]public int color;
        public Footer footer;
        public Image image;
        public Thumbnail thumbnail;
        public Author author;
        public Field[] fields;


        public static Embed CreateEmbed(string title, string description, Color embedColor , string url = "")
        {
            return new Embed
            {
                title = title,
                description = description,
                url = url,
                embedColor = embedColor,
                color = ColorToInt(embedColor)
            };
        }
        
        public Embed SetTitle(string title)
        {
            this.title = title;
            return this;
        }
        
        
        public Embed CreateNewField(string name, string value, bool inline = false)
        {
            var field = new Field
            {
                name = name,
                value = value,
                inline = inline
            };
            if (fields == null)
                fields = new Field[1] { field };
            else
                Array.Resize(ref fields, fields.Length + 1);
            fields[^1] = field;
            return this;
        
        }
        public Embed CreateThumbnail(string url)
        {
            thumbnail = new Thumbnail
            {
                url = url
            };
            return this;
        }
        public Embed SetImage(string url)
        {
            image = new Image
            {
                url = url
            };
            return this;
        }
        public Embed SetFooter(string text, string icon_url = "")
        {
            footer = new Footer
            {
                text = text,
                icon_url = icon_url
            };
            return this;
        }
        public Embed SetAuthor(string name, string url = "", string icon_url = "")
        {
            author = new Author
            {
                name = name,
                url = url,
                icon_url = icon_url
            };
            return this;
        }
        
        public Embed CreateEmbedAuthor(string name, string url = "", string icon_url = "")
        {
            author = new Author
            {
                name = name,
                url = url,
                icon_url = icon_url
            };
            return this;
        }
        
        public Embed SetColor(Color rgbColor)
        {
            embedColor = rgbColor;
            color = ColorToInt(rgbColor);
            return this;
        }
        public void RgbColorToInt() 
            => color = ColorToInt(embedColor);

        public static int ColorToInt(Color color)
        {
            var r = (int)(color.r * 255);
            var g = (int)(color.g * 255);
            var b = (int)(color.b * 255);
            return r << 16 | g << 8 | b;
        }

        public void GetAttachments(Attachment[] attachments)
        {
            var attachmentDict = attachments.ToDictionary(x => x.refKey, x => x);
            
            if (image != null)
            {
                if (!image.url.StartsWith("http") && !image.url.StartsWith("https"))
                {
                    if (attachmentDict.TryGetValue(image.url, out var attachment))
                    {
                        image.url = ImageAttachment.ToAttachmentPath(attachment.filename);
                    }
                }
            }
            if (thumbnail != null)
            {
                if (!thumbnail.url.StartsWith("http") && !thumbnail.url.StartsWith("https"))
                {
                    if (attachmentDict.TryGetValue(thumbnail.url, out var attachment))
                    {
                        thumbnail.url = ImageAttachment.ToAttachmentPath(attachment.filename);
                    }
                }
            }
            if (footer != null)
            {
                if (!footer.icon_url.StartsWith("http") && !footer.icon_url.StartsWith("https"))
                {
                    if (attachmentDict.TryGetValue(footer.icon_url, out var attachment))
                    {
                        footer.icon_url = ImageAttachment.ToAttachmentPath(attachment.filename);
                    }
                }
            }
            if (author != null)
            {
                if (!author.icon_url.StartsWith("http") && !author.icon_url.StartsWith("https"))
                {
                    if (attachmentDict.TryGetValue(author.icon_url, out var attachment))
                    {
                        author.icon_url = ImageAttachment.ToAttachmentPath(attachment.filename);
                    }
                }
            }
        }
    }
}