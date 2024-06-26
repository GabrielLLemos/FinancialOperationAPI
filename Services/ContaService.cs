using FinancialOperationAPI.Entities;
using FinancialOperationAPI.Repositories;

namespace FinancialOperationAPI.Services
{
    public class ContaService : IContaService
    {
        private readonly IContaRepository _contaRepository;

        public ContaService(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public void Alterar(Guid id, string nome)
        {
            // Exemplo de regra de negócio: não permitir atualizar saldo diretamente
            var contaExistente = _contaRepository.GetById(id);
            if (contaExistente == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            if (nome != null)
            {
                _contaRepository.Update(id, nome);
            }
        }

        public Conta CriarConta(string nome, double saldo)
        {
            var novaConta = _contaRepository.Add(nome, saldo);

            return novaConta;
        }

        public void Delete(Guid id)
        {
            var conta = _contaRepository.GetById(id);

            if (conta == null)
            {
                throw new ArgumentException("Conta não encontrada.");
            }
            _contaRepository.Delete(id);
        }

        public Conta Sacar(Guid id, double valor)
        {
            var conta = _contaRepository.GetById(id);
            if(conta == null)
            {
                throw new ArgumentException("Conta não encontrada.");
            }

            if (valor > conta.Saldo && valor > 0)
            {
                throw new ArgumentException("Valor indisponível para saque.");
            }
            
            var novoSaldo = conta.Saldo - valor;
            return _contaRepository.Update(id, null, novoSaldo);
        }

        public Conta Depositar(Guid id, double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("A quantia do depósito deve ser maior que zero.");
            }

            var conta = _contaRepository.GetById(id);
            if(conta == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }

            var novoSaldo = conta.Saldo + valor;

            return _contaRepository.Update(id, null, novoSaldo);
        }

        public IList<Conta> GetAll()
        {
            return _contaRepository.GetAll().ToList();
        }

        public Conta GetById(Guid id)
        {
            var conta = _contaRepository.GetById(id);
            if (conta == null)
            {
                throw new KeyNotFoundException("Cliente não encontrado.");
            }
            return conta;
        }
    }
}
