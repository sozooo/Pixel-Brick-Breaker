
namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;
        
        public float MusicLevel = 1.0f;
        public float SoundLevel = 1.0f;
        public bool Muted = false;

        public int Coins = 0;
        public float Highscore = 0;

        public int TimerLevel = 0;
        public int BlastRadiusLevel = 0;
        public bool IsAdRemoved = false;

        public int CoinsForAd = 500;
    }
}
