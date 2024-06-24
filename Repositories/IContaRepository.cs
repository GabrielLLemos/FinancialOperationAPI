using FinancialOperationAPI.Entities;

namespace FinancialOperationAPI.Repositories
{
    public interface IContaRepository
    {
        IEnumerable<Conta> GetAll();
        Conta GetById(Guid id);
        Conta Add(string nome, double saldo);
        Conta Update(Guid id, string? nome, double saldo = 0);
        void Delete(Guid id);
    }
}
