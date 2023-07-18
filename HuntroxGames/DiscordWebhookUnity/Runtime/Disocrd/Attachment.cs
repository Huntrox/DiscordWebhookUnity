using System;

namespace HuntroxGames.Utils
{
    [System.Serializable]
    public class Attachment
    {
        public string filename;
        public string description;
        public int id;
        [NonSerialized]public string filePath;
    }
}