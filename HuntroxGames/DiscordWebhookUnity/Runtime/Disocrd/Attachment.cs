using System.Linq;
using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]
    public class Attachment
    {
        public string refKey;
        public string filePath;
        public string description;
        [HideInInspector]public string filename;
        [HideInInspector]public int id;
        [HideInInspector]public byte[] fileData;
        
        public Attachment(){}
        /// <summary>
        /// Initializes a new instance of the <see cref="Attachment"/> class.
        /// </summary>
        /// <param name="filePath"> The path to the file. e.g. "C:/image1.png"</param>
        /// <param name="referenceKey">Reference key used to refer to the attachment in the webhook. e.g. "attachment://image1.png"</param>
        /// <param name="description"></param>
        public Attachment(string filePath, string referenceKey, string description = "")
        {
            this.filePath = filePath;
            this.description = description;
            this.filename = System.IO.Path.GetFileName(filePath);
            this.refKey = referenceKey;
        }

        public virtual void LoadFile()
        {
            fileData = System.IO.File.ReadAllBytes(filePath);
        }
    }
    /// <summary>
    /// Class to load an image from a Resources folder or Texture2D
    /// </summary>
    public class ImageAttachment : Attachment
    {
        
        private bool alreadyEncoded = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageAttachment"/> class.
        /// </summary>
        /// <param name="filePath">The path to the image file.</param>
        /// <param name="referenceKey">Reference key used to refer to the attachment in the webhook.</param>
        /// <param name="description">Description of the attachment.</param>
        public ImageAttachment(string filePath, string referenceKey, string description = "")
        {
            this.filePath = filePath;
            this.filename = filePath.Split('/').Last() + ".png";
            this.refKey = referenceKey;
            this.description = description;
        }
        /// <summary>
        /// Creates an <see cref="ImageAttachment"/> from a <see cref="Texture2D"/>.
        /// </summary>
        /// <param name="texture2D">The texture to create the attachment from.</param>
        /// <param name="referenceKey">Reference key used to refer to the attachment in the webhook.</param>
        /// <param name="description">Description of the attachment.</param>
        /// <returns>A new instance of <see cref="ImageAttachment"/>.</returns>  
        public static ImageAttachment FromTexture2D(Texture2D texture2D, string referenceKey, string description = "")
        {
            var attachment = new ImageAttachment(texture2D.name, referenceKey, description)
            {
                fileData = texture2D.EncodeToPNG(),
                alreadyEncoded = true
            };
            return attachment;
        }
        public override void LoadFile()
        {
            var file = Resources.Load<Texture2D>(filePath);
            if (!filename.EndsWith("png"))
                filename += ".png";
            if (alreadyEncoded)
                return;
            fileData = file.EncodeToPNG();
        }
        
        public static string ToAttachmentPath(string filename) 
            => "attachment://" + filename.Replace("/", "_");
    }
    
    public class TextAttachment : Attachment
    {
        public TextAttachment(string filePath, string referenceKey, string description = "")
        {
            this.filePath = filePath;
            this.filename = filePath.Split('/').Last() + ".txt";
            this.refKey = referenceKey;
            this.description = description;
        }

        public override void LoadFile()
        {
            var file = Resources.Load<TextAsset>(filePath);
            fileData = System.Text.Encoding.UTF8.GetBytes(file.text);
            if (!filename.EndsWith("txt"))
                filename += ".txt";
        }
    }
}