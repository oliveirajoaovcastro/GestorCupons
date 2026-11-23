using GestorCupons.DTOs;
using GestorCupons.Models;

namespace GestorCupons.Interfaces;

public interface ICupomService
{
    Cupom CriarCupom(CriarCupomRequest request);

}