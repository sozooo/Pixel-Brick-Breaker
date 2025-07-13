namespace Project.Scripts.WorkObjects.MessageBrokers.Figure
{
    public struct M_FigureFell
    {
        public M_FigureFell(FigureSystem.Figure figure)
        {
            Figure = figure;
        }
    
        public FigureSystem.Figure Figure { get; private set; }
    }
}