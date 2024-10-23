using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]
    public class Thumbnail
    {
        public string url;
        [HideInInspector]public string proxy_url;
        [HideInInspector]public int height;
        [HideInInspector]public int width;
        
        public Thumbnail(string url)
        {
            this.url = url;
        }

        public Thumbnail()
        {
            
        }
    }
}