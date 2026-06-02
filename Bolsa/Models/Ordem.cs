namespace Bolsa.Models {
    public enum TipoOrdem {
        Compra,
        Venda
    }

    // Classe Ordem utilizando Construtor Primário do C# moderno
    public class Ordem(Guid id, Usuario usuario, Ativo ativo, TipoOrdem tipo, int quantidade, decimal precoAlvo) {
        public Guid Id { get; } = id;
        public Usuario Usuario { get; } = usuario;
        public Ativo Ativo { get; } = ativo;
        public TipoOrdem Tipo { get; } = tipo;
        public int Quantidade { get; set; } = quantidade;
        public decimal PrecoAlvo { get; } = precoAlvo;
        public DateTime DataCriacao { get; } = DateTime.UtcNow;
    }
}
