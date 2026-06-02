namespace Bolsa.Models {
    public class Carteira(Usuario usuario) {
        public Usuario Usuario { get; } = usuario;

        // Dicionário para guardar: <Ticker da Ação, Quantidade Disponível>
        public Dictionary<string, int> Ativos { get; } = [];

        public void AdicionarAtivo(string ticker, int quantidade) {
            ticker = ticker.ToUpper();
            if (Ativos.ContainsKey(ticker))
                Ativos[ticker] += quantidade;
            else
                Ativos[ticker] = quantidade;
        }

        public void RemoverAtivo(string ticker, int quantidade) {
            ticker = ticker.ToUpper();
            if (Ativos.ContainsKey(ticker) && Ativos[ticker] >= quantidade) {
                Ativos[ticker] -= quantidade;
                if (Ativos[ticker] == 0) Ativos.Remove(ticker);
            } else {
                throw new InvalidOperationException("Quantidade insuficiente de ativos para venda");
            }
        }

    }
}
