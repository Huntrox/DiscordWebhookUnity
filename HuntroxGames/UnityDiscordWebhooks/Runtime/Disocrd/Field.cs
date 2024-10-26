namespace HuntroxGames.Utils.DiscordWebhook
{
    [System.Serializable]
    public class Field
    {
        public string name;
        public string value;
        public bool inline;

        public Field(string name, string value, bool inline = false)
        {
            this.name = name;
            this.value = value;
            this.inline = inline;
        }

        public Field()
        {
            
        }
    }
}