using Bolsa.Models;

namespace Bolsa.Services {
    public class LivroOfertas(Ativo ativo) {
        public Ativo Ativo { get; } = ativo;

        // Lista ordenadas
        private readonly List<Ordem> _compras = [];
        private readonly List<Ordem> _vendas = [];

        public void NovaOrdem(Ordem novaOrdem) {

            if (novaOrdem.Tipo == TipoOrdem.Compra) {
                _compras.Add(novaOrdem);
                _compras.Sort((a, b) => b.PrecoAlvo == a.PrecoAlvo   // Ordena compras: maior preço primeiro
                ? a.DataCriacao.CompareTo(b.DataCriacao)            // Se igual, a mais antiga primeiro
                : b.PrecoAlvo.CompareTo(a.PrecoAlvo));
            } else {
                _vendas.Add(novaOrdem);
                _vendas.Sort((a, b) => b.PrecoAlvo == a.PrecoAlvo   // Menor preço primeiro
                ? a.DataCriacao.CompareTo(b.DataCriacao)           // Se igual, a mais antiga primeiro  
                : a.PrecoAlvo.CompareTo(b.PrecoAlvo));
            }

            ExecutarMatch();
        }

        public void ExecutarMatch() {
            // Enquanto houver ordens em ambas as filas e o maior preço de compra for >= ao menor preço de venda

            while (_compras.Count > 0 && _vendas.Count > 0 && _compras[0].PrecoAlvo >= _vendas[0].PrecoAlvo) {
                Ordem ordemCompra = _compras[0];
                Ordem ordemVenda = _vendas[0];

                // O preço do trade sera o da ordem que já estava esperando no livro
                decimal precoExecucao = ordemCompra.DataCriacao < ordemVenda.DataCriacao
                ? ordemCompra.PrecoAlvo
                : ordemVenda.PrecoAlvo;

                int qtdNegociada = Math.Min(ordemCompra.Quantidade, ordemVenda.Quantidade);

                // Executa a troca financeira e de ativos entre os usuários
                EfetivarTrade(ordemCompra.Usuario, ordemVenda.Usuario, qtdNegociada, precoExecucao);

                // Atualiza o preco atual do Ativo no mercado
                Ativo.PrecoInicial = precoExecucao;

                System.Console.WriteLine($"[TRADE EXECUTADO] {Ativo.Ticker}: {qtdNegociada} cotas negociadas a R${precoExecucao}");

                // Deduz a quantidade de ordens
                ordemCompra.Quantidade -= qtdNegociada;
                ordemVenda.Quantidade -= qtdNegociada;

                // Se a ordem foi totalmente preenchida, remove da fila
                if (ordemCompra.Quantidade == 0) _compras.RemoveAt(0);
                if (ordemVenda.Quantidade == 0) _vendas.RemoveAt(0);
            }

        }
        private static void EfetivarTrade(Usuario comprador, Usuario vendedor, int quantidade, decimal preco) {
            decimal valorTotal = quantidade * preco;
            // Ajusta saldos
            comprador.Saldo -= valorTotal;
            vendedor.Saldo += valorTotal;
        }

    }
}
