using FinancialOperationAPI.Entities;

namespace FinancialOperationAPI.Services
{
    public interface IContaService
    {
        IList<Conta> GetAll();
        Conta GetById(Guid id);
        Conta CriarConta(string nome, double saldo);
        void Alterar(Guid id, String nome);
        void Delete(Guid id);
        Conta Depositar(Guid id, double valor);
        Conta Sacar(Guid id, double valor);
    }
}
