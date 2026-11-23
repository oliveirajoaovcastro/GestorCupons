using GestorCupons.Interfaces;
using GestorCupons.Models;

namespace GestorCupons.Repositories;

public class CupomRepository : ICupomRepository
{
    private readonly List<Cupom> _cupons = [];
    private int _proximoId = 1;

    public Cupom AdicionarCupom(Cupom cupom)
    {
        cupom.Id  = _proximoId++;
        _cupons.Add(cupom);
        
        return cupom;
    }
    
    public Cupom? ObterPorCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
            return null;

        foreach (var cupom in _cupons)
        {
            if (string.Equals(cupom.Codigo, codigo, StringComparison.OrdinalIgnoreCase))
            {
                return cupom;
            }
        }

        return null;
    }
    
    public IReadOnlyList<Cupom> ListarTodos()
    {
        return _cupons.AsReadOnly();
    }
    
    public Cupom? ObterPorId(int id)
    {
        if (id <= 0)
            return null;

        foreach (var cupom in _cupons)
        {
            if (cupom.Id == id)
            {
                return cupom;
            }
        }

        return null;
    }
}