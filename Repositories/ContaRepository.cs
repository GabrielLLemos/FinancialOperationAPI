using FinancialOperationAPI.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace FinancialOperationAPI.Repositories
{
    public class ContaRepository : IContaRepository
    {
        private static List<Conta> _contas = new List<Conta>();

        public Conta Add(string nome, double saldo)
        {
            var conta = new Conta();

            conta.SetNome(nome);
            conta.SetSaldo(saldo);

            _contas.Add(conta);

            return conta;
        }

        public void Delete(Guid id)
        {
            var conta = _contas.FirstOrDefault(c => c.Id == id);

            _contas.Remove(conta);
        }

        public IEnumerable<Conta> GetAll()
        {
            return _contas;
        }

        public Conta GetById(Guid id)
        {
            return _contas.FirstOrDefault(c => c.Id == id);
        }

        public Conta Update(Guid id, string? nome, double saldo = 0)
        {
            var existingConta = _contas.FirstOrDefault(c => c.Id == id);

            if(nome is not null)
                existingConta.SetNome(nome);

            if (saldo > 0)
                existingConta.SetSaldo(saldo);

            return existingConta;
        }
    }
}
