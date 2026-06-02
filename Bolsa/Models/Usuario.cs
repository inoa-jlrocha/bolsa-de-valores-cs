namespace Bolsa.Models {
    public class Usuario(string nome, decimal saldo) {

        public string Nome { get; set; } = nome;
        public decimal Saldo { get; set; } = saldo;
    }
}
