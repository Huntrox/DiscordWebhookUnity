using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]
    public class Image
    {
        public string url;
        [HideInInspector]public string proxy_url;
        [HideInInspector]public int height;
        [HideInInspector]public int width;

        public Image(string url)
        {
            this.url = url;
        }

        public Image()
        {
            
        }
    }
}