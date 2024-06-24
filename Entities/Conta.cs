namespace FinancialOperationAPI.Entities
{
    public class Conta
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public double Saldo { get; private set; }

        public Conta()
        {
            Id = Guid.NewGuid();
        }

        public void SetSaldo(double saldo) => Saldo = saldo;
        public void SetNome(string nome) => Nome = nome;
    }
}
