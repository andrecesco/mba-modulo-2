using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMIB.Core.Business.Requests;

public class ProdutoRequest : RequestBase
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Nome { get; set; }
    [DisplayName("Preço")]
    public decimal Preco { get; set; }
    [DisplayName("Descrição")]
    public string Descricao { get; set; }
    public int QuantidadeEstoque { get; set; }
    public Guid VendedorId { get; set; }
    public Guid CategoriaId { get; set; }
    public bool Ativo { get; set; }
    public IFormFile Imagem { get; set; }

    public ProdutoRequest()
    {
        Id = Guid.NewGuid();
    }

    public override bool IsValid()
    {
        ValidationResult = new ProdutoValidation().Validate(this);

        return ValidationResult.IsValid;
    }

    private sealed class ProdutoValidation : AbstractValidator<ProdutoRequest>
    {
        public ProdutoValidation()
        {
            RuleFor(m => m.Id)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido");
            RuleFor(m => m.Nome)
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(100)
                .WithMessage("O campo {PropertyName} não pode exceder {MaxLength} caracteres");
            RuleFor(m => m.Descricao)
                .MaximumLength(100)
                .WithMessage("O campo {PropertyName} não pode exceder {MaxLength} caracteres");
        }
    }
}