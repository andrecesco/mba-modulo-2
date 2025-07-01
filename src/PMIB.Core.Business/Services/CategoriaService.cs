using AutoMapper;
using PMIB.Core.Business.Dtos;
using PMIB.Core.Business.Interfaces;
using PMIB.Core.Business.Models;
using PMIB.Core.Business.Requests;

namespace PMIB.Core.Business.Services;

public class CategoriaService(
    INotificador notificador,
    ICategoriaRepositorio categoriaRepository,
    IProdutoRepositorio produtoRepositorio,
    IMapper mapper) : BaseService(notificador), ICategoriaService
{
    public async Task<CategoriaDto> ObterPorId(Guid id)
    {
        var model = await categoriaRepository.ObterPorId(id);
        return model == null ? null : mapper.Map<CategoriaDto>(model);
    }

    public async Task<IEnumerable<CategoriaDto>> ObterTodos()
    {
        var models = await categoriaRepository.ObterTodos();
        return models.Select(mapper.Map<CategoriaDto>);
    }

    public async Task<Categoria> Adicionar(CategoriaRequest request)
    {
        if (!request.IsValid()) 
        {
            Notificar(request.ValidationResult);
            return null;
        }

        if ((await categoriaRepository.Buscar(c => c.Nome == request.Nome)).FirstOrDefault() != null)
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

        await categoriaRepository.Adicionar(categoria);
        
        return categoria;
    }

    public async Task<Categoria> Atualizar(Guid id, CategoriaRequest request)
    {
        if (!request.IsValid())
        {
            Notificar(request.ValidationResult);
            return null;
        }

        var categoriaAtualizacao = await categoriaRepository.ObterPorId(id);
        if (categoriaAtualizacao == null)
        {
            Notificar("Categoria não encontrada.");
            return null;
        }

        if ((await categoriaRepository.Buscar(c => c.Nome == request.Nome && c.Id != id)).FirstOrDefault() != null)
        {
            Notificar("Já existe uma categoria com este nome.");
            return null;
        }


        categoriaAtualizacao.Nome = request.Nome;
        categoriaAtualizacao.Descricao = request.Descricao;

        await categoriaRepository.Atualizar(categoriaAtualizacao);
        
        return categoriaAtualizacao;
    }

    public async Task<bool> Remover(Guid id)
    {
        var categoria = await categoriaRepository.ObterPorId(id);
        if (categoria == null)
        {
            Notificar("Categoria não encontrada.");
            return false;
        }

        // Verificar se existem produtos vinculados a esta categoria
        var produtos = await produtoRepositorio.ObterPorCategoriaId(id);
        if (produtos != null && produtos.Count != 0)
        {
            Notificar("Esta categoria possui produtos vinculados e não pode ser removida.");
            return false;
        }

        await categoriaRepository.Remover(id);
        return true;
    }
}
