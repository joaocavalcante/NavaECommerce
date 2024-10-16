using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

namespace Application.Services
{
    public class ProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Produto> CriarProdutoAsync(string nome, string descricao, decimal preco, int estoque)
        {
            if (preco <= 0)
                throw new DomainException("O preço deve ser maior que zero.");
            if (estoque < 0)
                throw new DomainException("Estoque não pode ser negativo.");

            var produto = new Produto(nome, descricao, preco, estoque);
            await _produtoRepository.AdicionarAsync(produto);
            return produto;
        }

        public async Task AtualizarProdutoAsync(Guid id, string nome, string descricao, decimal preco)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            if (produto == null)
                throw new DomainException("Produto não encontrado.");

            produto.AtualizarDetalhes(nome, descricao, preco);
            await _produtoRepository.AtualizarAsync(produto);
        }

        public async Task RemoverProdutoAsync(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            if (produto == null)
                throw new DomainException("Produto não encontrado.");

            await _produtoRepository.RemoverAsync(id);
        }

        public async Task<IEnumerable<Produto>> ListarProdutosAsync()
        {
            return await _produtoRepository.ListarAsync();
        }

        public async Task<Produto> ObterPorIdAsync(Guid id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);
            if (produto == null)
                throw new DomainException("Produto não encontrado.");
            return produto;
        }

    }
}
