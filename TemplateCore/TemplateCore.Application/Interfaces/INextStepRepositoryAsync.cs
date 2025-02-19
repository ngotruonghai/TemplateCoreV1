namespace TemplateCore.Application.Interfaces
{
    public interface INextStepRepositoryAsync: IGenericRepositoryAsync<NextStep>
    {
        Task<IEnumerable<NextStep>> GetbextStepBtQuyTrinhId(int quytrinhid);
    }
}
