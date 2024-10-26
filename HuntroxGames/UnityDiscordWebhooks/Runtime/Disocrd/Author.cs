using UnityEngine;

namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]
    public class Author
    {
        public string name;
        public string url;
        public string icon_url;
        [HideInInspector]public string proxy_icon_url;
    }
}