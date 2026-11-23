using GestorCupons.DTOs;
using GestorCupons.Interfaces;
using GestorCupons.Models;

namespace GestorCupons.Services;

public class CupomService : ICupomService
{
    private readonly ICupomRepository _cupomRepository;

    public CupomService(ICupomRepository cupomRepository)
    {
        _cupomRepository = cupomRepository;
    }

    public Cupom CriarCupom(CriarCupomRequest request)
    {
        var codigoLimpo = request.Codigo?.Trim().ToUpper() ?? string.Empty;
        
        if (string.IsNullOrWhiteSpace(codigoLimpo))
        {
            throw new ArgumentException("O código não pode ser nulo ou vazio.", nameof(codigoLimpo));
        }

        if (codigoLimpo.Length > 10)
        {
            throw new ArgumentException("O código não pode ter mais de 10 caracteres.", nameof(codigoLimpo));
        }

        if (_cupomRepository.ObterPorCodigo(codigoLimpo) is not null)
        {
            throw new ArgumentException($"Já existe um cupom cadastrado com o código '{codigoLimpo}'.", nameof(codigoLimpo));
        }
        
        if (request.IdAgencia == 0)
        {
            throw new ArgumentException("O ID da agência deve ser zero.", nameof(request.IdAgencia));
        }

        if (request.PercentualDesconto < 1 || request.PercentualDesconto > 100)
        {
            throw new ArgumentException("O percentual de desconto deve estar entre 1% e 100%.", nameof(request.PercentualDesconto));
        }
        
        if (request.DataFimVigencia.HasValue && request.DataFimVigencia.Value.Date < request.DataInicioVigencia.Date)
        {
            throw new ArgumentException("A data de fim de vigência não pode ser anterior à data de início.", nameof(request.DataFimVigencia));
        }
        
        var novoCupom = new Cupom
        {
            Codigo = codigoLimpo,
            IdAgencia = request.IdAgencia,
            PercentualDesconto = request.PercentualDesconto,
            DataInicioVigencia = request.DataInicioVigencia,
            DataFimVigencia = request.DataFimVigencia,
            CriadoPor = "Sistema",
            DataCriacao = DateTime.Now
        };

        return _cupomRepository.AdicionarCupom(novoCupom);
    }

    public IReadOnlyList<Cupom> ListarCupons()
    {
        return _cupomRepository.ListarTodos();
    }

    public Cupom? ObterPorId(int id)
    {
        if (id <= 0)
            return null;

        return _cupomRepository.ObterPorId(id);
    }
}