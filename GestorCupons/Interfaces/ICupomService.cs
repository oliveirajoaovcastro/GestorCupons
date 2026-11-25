using GestorCupons.DTOs;
using GestorCupons.Models;

namespace GestorCupons.Interfaces;

public interface ICupomService
{
    Cupom CriarCupom(CriarCupomRequest request);
    IReadOnlyList<Cupom> ListarCupons();
    Cupom? ObterPorId(int id);
    Cupom? AtualizarCupom(int id, AtualizarCupomRequest request);
}