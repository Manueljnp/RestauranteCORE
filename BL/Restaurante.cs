using Azure.Core;
using DL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Restaurante : IRestaurante
    {
        //5. Crear la conexión y que sea de solo lectura (readonly)
        private readonly RestauranteCoreContext _connection;

        //6. crear un método que recibe la conexión y le asignamos el valor
        public Restaurante(RestauranteCoreContext connection)
        {
            _connection = connection;
        }

        //7. El método NO debe usar STATIC
        public ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            
            try
            {
                /*using (DL.RestauranteCoreContext context = new DL.RestauranteCoreContext())
                {
                        //8. Sacar del bloque USING, ya no ocuparemos CONTEXT como Conexión
                }*/

                //9. Usar la variable '_connection' declarada con anterioridad, esa ahora es la cadena de conexión
                var query = _connection.RestauranteGetAllSP.FromSqlRaw($"RestauranteGetAll").ToList();
                                                //FromSqlRaw => SELECT
                if (query.Count > 0)
                {
                    result.Objects = new List<object>();

                    foreach (var item in query)
                    {
                        ML.Restaurante restaurante = new ML.Restaurante();

                        restaurante.IdRestaurante = item.IdRestaurante;
                        restaurante.Nombre = item.Nombre;
                        restaurante.Eslogan = item.Eslogan;
                        restaurante.Imagen = item.Imagen;
                        restaurante.Descripcion = item.Descripcion;

                        result.Objects.Add(restaurante);
                    }
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result Delete(int idRestaurante)
        {
            ML.Result result = new ML.Result();

            try
            {                                           //ExecuteSqlRaw => INSERT, UPDATE, DELETE
                int filasAfectadas = _connection.Database.ExecuteSqlRaw($"RestauranteEliminar {idRestaurante}");

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result Add(ML.Restaurante restaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                var imagen = new SqlParameter("@Imagen", SqlDbType.VarBinary);

                if (restaurante.Imagen != null)
                {
                    imagen.Value = restaurante.Imagen;
                }
                else
                {
                    imagen.Value = DBNull.Value;
                }
                                                        //ExecuteSqlRaw => INSERT, UPDATE, DELETE
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"RestauranteAdd " +
                    $"'{restaurante.Nombre}', '{restaurante.Eslogan}', @Imagen, '{restaurante.Descripcion}'", imagen);

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result Update(ML.Restaurante restaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                var imagen = new SqlParameter("@Imagen", SqlDbType.VarBinary);

                if (restaurante.Imagen != null)
                {
                    imagen.Value = restaurante.Imagen;
                }
                else
                {
                    imagen.Value = DBNull.Value;
                }
                                                        //ExecuteSqlRaw => INSERT, UPDATE, DELETE
                var filasAfectadas = _connection.Database.ExecuteSqlRaw($"RestauranteUpdate " +
                    $" {restaurante.IdRestaurante}, '{restaurante.Nombre}', '{restaurante.Eslogan}', @Imagen, '{restaurante.Descripcion}'", imagen);

                if (filasAfectadas > 0)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public ML.Result GetById(int idRestaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _connection.Restaurantes.FromSqlRaw($"RestauranteGetById {idRestaurante}").AsEnumerable().FirstOrDefault();

                if (query != null)
                {
                    ML.Restaurante restaurante = new ML.Restaurante();

                    restaurante.IdRestaurante = query.IdRestaurante;
                    restaurante.Nombre = query.Nombre;
                    restaurante.Eslogan = query.Eslogan;
                    restaurante.Imagen = query.Imagen;
                    restaurante.Descripcion = query.Descripcion;

                    result.Object = restaurante;
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage= ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
