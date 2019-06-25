namespace CerebroML.Genetics
{
    public interface IEntity
    {
        Genome GetGenome();
        void SetGenome(Genome g);
    }
}