namespace Project.Scripts.WorkObjects.MessageBrokers.Game
{
    public struct M_TimeRedeemed
    {
        public M_TimeRedeemed(float time)
        {
            Time = time;
        }

        public float Time { get; private set; }
    }
}