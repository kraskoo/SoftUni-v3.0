namespace P10InfernoInfinity.Interfaces.Factories
{
    public interface IGemLevelFactory
    {
        IGemLevelable CreateGemLevel(string type);
    }
}