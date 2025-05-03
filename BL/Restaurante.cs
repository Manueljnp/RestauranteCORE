using Azure.Core;
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
    public class Restaurante
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RestauranteCoreContext context = new DL.RestauranteCoreContext())
                {
                    //FromSqlRaw => SELECT
                    //ExecuteSqlRaw => INSERT, UPDATE, DELETE

                    var query = context.RestauranteGetAllSP.FromSqlRaw($"RestauranteGetAll").ToList();

                    if(query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.Restaurante restaurante = new ML.Restaurante ();

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
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result Delete(int idRestaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RestauranteCoreContext context = new DL.RestauranteCoreContext())
                {
                    int filasAfectadas = context.Database.ExecuteSqlRaw($"RestauranteEliminar {idRestaurante}");

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
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
        public static ML.Result Add(ML.Restaurante restaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RestauranteCoreContext context = new DL.RestauranteCoreContext())
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

                    var filasAfectadas = context.Database.ExecuteSqlRaw($"RestauranteAdd " +
                        $"'{restaurante.Nombre}', '{restaurante.Eslogan}', @Imagen, '{restaurante.Descripcion}'", imagen);

                    if(filasAfectadas > 0 )
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
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
        public static ML.Result Update(ML.Restaurante restaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.RestauranteCoreContext context = new DL.RestauranteCoreContext())
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

                    var filasAfectadas = context.Database.ExecuteSqlRaw($"RestauranteUpdate " +
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
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result GetById(int idRestaurante)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RestauranteCoreContext context = new DL.RestauranteCoreContext())
                {
                    var query = context.Restaurantes.FromSqlRaw($"RestauranteGetById {idRestaurante}").AsEnumerable().FirstOrDefault();

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
