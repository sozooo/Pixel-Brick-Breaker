namespace Project.Scripts.WorkObjects.MessageBrokers
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