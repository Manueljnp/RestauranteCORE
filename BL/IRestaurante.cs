using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IRestaurante
    {
        //4. Hacer la firma de métodos
        ML.Result GetAll();
        ML.Result Delete(int idRestaurante);
        ML.Result Add(ML.Restaurante restaurante);
        ML.Result Update(ML.Restaurante restaurante);
        ML.Result GetById(int idRestaurante);
    }
}
