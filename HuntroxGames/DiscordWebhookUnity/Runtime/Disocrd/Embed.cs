namespace HuntroxGames.Utils
{
    [System.Serializable]
    public class Embed 
    {
        public string title;
        public string description;
        public string url;
        public int color;
        public Footer footer;
        public Image image;
        public Thumbnail thumbnail;
        public Author author;
        public Field[] fields;
    }
}