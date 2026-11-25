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
        ValidarDadosCupom(
            request.Codigo,
            request.IdAgencia,
            request.PercentualDesconto,
            request.DataInicioVigencia,
            request.DataFimVigencia
        );

        var codigoLimpo = request.Codigo.Trim().ToUpperInvariant();

        if (_cupomRepository.ObterPorCodigo(codigoLimpo) is not null)
        {
            throw new ArgumentException($"Já existe um cupom cadastrado com o código '{codigoLimpo}'.");
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

    public Cupom? AtualizarCupom(int id, AtualizarCupomRequest request)
    {
        if (id <= 0)
            return null;

        var cupom = _cupomRepository.ObterPorId(id);
        if (cupom is null)
            return null;

        ValidarDadosCupom(
            request.Codigo,
            request.IdAgencia,
            request.PercentualDesconto,
            request.DataInicioVigencia,
            request.DataFimVigencia
        );

        var codigoLimpo = request.Codigo.Trim().ToUpperInvariant();
        var cupomExistente = _cupomRepository.ObterPorCodigo(codigoLimpo);

        if (cupomExistente is not null && cupomExistente.Id != id)
            throw new ArgumentException($"Já existe outro cupom cadastrado com o código '{codigoLimpo}'.");

        cupom.Codigo = codigoLimpo;
        cupom.IdAgencia = request.IdAgencia;
        cupom.PercentualDesconto = request.PercentualDesconto;
        cupom.DataInicioVigencia = request.DataInicioVigencia;
        cupom.DataFimVigencia = request.DataFimVigencia;

        _cupomRepository.AtualizarCupom(cupom);

        return cupom;
    }

    private static void ValidarDadosCupom(
        string codigo,
        int idAgencia,
        int percentualDesconto,
        DateTime dataInicioVigencia,
        DateTime? dataFimVigencia)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            throw new ArgumentException("O código do cupom é obrigatório.");

        if (codigo.Trim().Length > 10)
            throw new ArgumentException("O código do cupom deve ter no máximo 10 caracteres.");

        if (idAgencia <= 0)
            throw new ArgumentException("O ID da agência deve ser maior que zero.");

        if (percentualDesconto < 1 || percentualDesconto > 100)
            throw new ArgumentException("O percentual de desconto deve estar entre 1% e 100%.");

        if (dataFimVigencia.HasValue && dataFimVigencia.Value < dataInicioVigencia)
            throw new ArgumentException("A data de fim da vigência deve ser posterior à data de início.");
    }
}