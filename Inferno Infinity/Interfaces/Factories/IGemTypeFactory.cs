namespace P10InfernoInfinity.Interfaces.Factories
{
    public interface IGemTypeFactory
    {
        IGemTypeable CreateGemType(string type);
    }
}