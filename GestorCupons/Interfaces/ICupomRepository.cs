using GestorCupons.Models;

namespace GestorCupons.Interfaces;

public interface ICupomRepository
{
    Cupom AdicionarCupom(Cupom cupom);
    Cupom? ObterPorCodigo(string codigo);
    IReadOnlyList<Cupom> ListarTodos();
    public Cupom? ObterPorId(int id);
    void AtualizarCupom(Cupom cupom);
    void DeletarCupom(Cupom cupom);
}