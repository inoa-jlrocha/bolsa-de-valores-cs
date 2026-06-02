namespace Bolsa.Models {
    public class Ativo(string ticker, string nomeEmpresa, decimal precoInicial) {

        // Propriedades do Ativo
        public string Ticker { get; set; } = ticker.ToUpper();
        public string NomeEmpresa { get; set; } = nomeEmpresa;
        public decimal PrecoInicial { get; set; } = precoInicial;
    }
}
