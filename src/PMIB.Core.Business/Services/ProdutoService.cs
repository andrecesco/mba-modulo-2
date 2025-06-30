using FluentValidation.Results;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Business.Notifications;
using PMIB.Core.Business.Requests;

namespace PMIB.Core.Business.Services;

public class ProdutoService : BaseService, IProdutoService
{
    private readonly IProdutoRepositorio _produtoRepository;
    private readonly ICategoriaRepositorio _categoriaRepository;
    private readonly IVendedorRepositorio _vendedorRepository;

    public ProdutoService(
        INotificador notificador,
        IProdutoRepositorio produtoRepository,
        ICategoriaRepositorio categoriaRepository,
        IVendedorRepositorio vendedorRepository) : base(notificador)
    {
        _produtoRepository = produtoRepository;
        _categoriaRepository = categoriaRepository;
        _vendedorRepository = vendedorRepository;
    }

    public async Task<Produto> ObterPorId(Guid id)
    {
        return await _produtoRepository.ObterPorId(id);
    }

    public async Task<IEnumerable<Produto>> ObterTodos()
    {
        return await _produtoRepository.ObterTodos();
    }

    public async Task<Produto> Adicionar(ProdutoRequest request)
    {
        if (!request.IsValid())
        {
            NotificarErrosValidacao(request.ValidationResult);
            return null;
        }

        // Verificar se a categoria existe
        var categoria = await _categoriaRepository.ObterPorId(request.CategoriaId);
        if (categoria == null)
        {
            Notificar("Categoria não encontrada.");
            return null;
        }

        // Verificar se o vendedor existe
        var vendedor = await _vendedorRepository.ObterPorId(request.VendedorId);
        if (vendedor == null)
        {
            Notificar("Vendedor não encontrado.");
            return null;
        }

        var produto = new Produto
        {
            Id = request.Id,
            Nome = request.Nome,
            Descricao = request.Descricao,
            Preco = request.Preco,
            QuantidadeEstoque = request.QuantidadeEstoque,
            CategoriaId = request.CategoriaId,
            VendedorId = request.VendedorId,
            Ativo = request.Ativo
        };

        await _produtoRepository.Adicionar(produto);
        
        return produto;
    }

    public async Task<Produto> Atualizar(Guid id, ProdutoRequest request)
    {
        if (!request.IsValid())
        {
            NotificarErrosValidacao(request.ValidationResult);
            return null;
        }

        var produtoAtualizacao = await _produtoRepository.ObterPorId(id);
        if (produtoAtualizacao == null)
        {
            Notificar("Produto não encontrado.");
            return null;
        }

        // Verificar se a categoria existe
        var categoria = await _categoriaRepository.ObterPorId(request.CategoriaId);
        if (categoria == null)
        {
            Notificar("Categoria não encontrada.");
            return null;
        }

        // Verificar se o vendedor existe
        var vendedor = await _vendedorRepository.ObterPorId(request.VendedorId);
        if (vendedor == null)
        {
            Notificar("Vendedor não encontrado.");
            return null;
        }

        produtoAtualizacao.Nome = request.Nome;
        produtoAtualizacao.Descricao = request.Descricao;
        produtoAtualizacao.Preco = request.Preco;
        produtoAtualizacao.QuantidadeEstoque = request.QuantidadeEstoque;
        produtoAtualizacao.CategoriaId = request.CategoriaId;
        produtoAtualizacao.VendedorId = request.VendedorId;
        produtoAtualizacao.Ativo = request.Ativo;

        await _produtoRepository.Atualizar(produtoAtualizacao);
        
        return produtoAtualizacao;
    }

    public async Task<bool> Remover(Guid id)
    {
        var produto = await _produtoRepository.ObterPorId(id);
        if (produto == null)
        {
            Notificar("Produto não encontrado.");
            return false;
        }

        await _produtoRepository.Remover(id);
        return true;
    }

    public async Task<IEnumerable<Produto>> ObterPorCategoria(Guid categoriaId)
    {
        return await _produtoRepository.ObterPorCategoriaId(categoriaId);
    }

    public async Task<IEnumerable<Produto>> ObterPorVendedor(Guid vendedorId)
    {
        return await _produtoRepository.ObterPorVendedorId(vendedorId);
    }
}
