namespace Alexander.RunnerCandy
{
    public interface IUpdatable
    {
        void DoUpdate();
        int Priority { get; }
    }
}