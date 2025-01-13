namespace TemplateCore.Application.Interfaces
{
    public interface ITransaction
    {
        void Begin();

        void Commit();

        void Rollback();
    }
}
