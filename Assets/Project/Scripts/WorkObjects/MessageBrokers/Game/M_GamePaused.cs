namespace Project.Scripts.WorkObjects.MessageBrokers.Game
{
    public struct M_GamePaused
    {
        public M_GamePaused(bool isIsPaused)
        {
            IsPaused = isIsPaused;
        }
        
        public bool IsPaused { get; }
    }
}