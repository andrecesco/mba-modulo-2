using FluentValidation.Results;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Business.Notifications;
using PMIB.Core.Business.Requests;

namespace PMIB.Core.Business.Services;

public class CategoriaService : BaseService, ICategoriaService
{
    private readonly ICategoriaRepositorio _categoriaRepository;

    public CategoriaService(
        INotificador notificador,
        ICategoriaRepositorio categoriaRepository) : base(notificador)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<Categoria> ObterPorId(Guid id)
    {
        return await _categoriaRepository.ObterPorId(id);
    }

    public async Task<IEnumerable<Categoria>> ObterTodos()
    {
        return await _categoriaRepository.ObterTodos();
    }

    public async Task<Categoria> Adicionar(CategoriaRequest request)
    {
        if (!request.IsValid()) 
        {
            NotificarErrosValidacao(request.ValidationResult);
            return null;
        }

        if (await _categoriaRepository.Buscar(c => c.Nome == request.Nome).Result.AnyAsync())
        {
            Notificar("Já existe uma categoria com este nome.");
            return null;
        }


        var categoria = new Categoria
        {
            Id = request.Id,
            Nome = request.Nome,
            Descricao = request.Descricao
        };

        await _categoriaRepository.Adicionar(categoria);
        
        return categoria;
    }

    public async Task<Categoria> Atualizar(Guid id, CategoriaRequest request)
    {
        if (!request.IsValid())
        {
            NotificarErrosValidacao(request.ValidationResult);
            return null;
        }

        var categoriaAtualizacao = await _categoriaRepository.ObterPorId(id);
        if (categoriaAtualizacao == null)
        {
            Notificar("Categoria não encontrada.");
            return null;
        }

        if (await _categoriaRepository.Buscar(c => c.Nome == request.Nome && c.Id != id).Result.AnyAsync())
        {
            Notificar("Já existe uma categoria com este nome.");
            return null;
        }


        categoriaAtualizacao.Nome = request.Nome;
        categoriaAtualizacao.Descricao = request.Descricao;

        await _categoriaRepository.Atualizar(categoriaAtualizacao);
        
        return categoriaAtualizacao;
    }

    public async Task<bool> Remover(Guid id)
    {
        var categoria = await _categoriaRepository.ObterPorId(id);
        if (categoria == null)
        {
            Notificar("Categoria não encontrada.");
            return false;
        }

        // Verificar se existem produtos vinculados a esta categoria
        var produtos = await _categoriaRepository.ObterProdutosPorCategoria(id);
        if (produtos != null && produtos.Any())
        {
            Notificar("Esta categoria possui produtos vinculados e não pode ser removida.");
            return false;
        }

        await _categoriaRepository.Remover(id);
        return true;
    }
}
