using GestorCupons.Models;

namespace GestorCupons.Interfaces;

public interface ICupomRepository
{
    Cupom AdicionarCupom(Cupom cupom);
    Cupom? ObterPorCodigo(string codigo);
}