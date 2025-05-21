using DL;
using Microsoft.AspNetCore.Mvc;

namespace PL_Web.Controllers
{
    public class RestauranteController : Controller
    {
        //10. Repetir el paso de la conexión, pero ahora referenciando a la INTERFAZ
        private readonly BL.IRestaurante _Irestaurante;
        public RestauranteController(BL.IRestaurante Irestaurante)
        {
            _Irestaurante = Irestaurante;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Restaurante restaurante = new ML.Restaurante();
            ML.Result result = _Irestaurante.GetAll(); //11. Mandar a llamar el método desde la interfaz

            if (result.Correct)
            {
                //Obtuvo toda la información
                restaurante.Restaurantes = result.Objects?.ToList();
            }
            else
            {
                restaurante.Restaurantes = new List<object>();
            }

            return View(restaurante);
        }

        [HttpGet]
        public IActionResult Form(int? idRestaurante)
        {
            ML.Restaurante restaurante = new ML.Restaurante();

            if(idRestaurante > 0)
            {
                ML.Result result = _Irestaurante.GetById(idRestaurante.Value);
                restaurante = (ML.Restaurante)result.Object;
            }

            return View(restaurante);
        }

        [HttpPost]
        public IActionResult Form(ML.Restaurante restaurante)
        {
            IFormFile file = Request.Form.Files.GetFile("inptFileImagen");

            if(file != null && file.Length > 0)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    restaurante.Imagen = memoryStream.ToArray();
                }
            }
            else
            {
                //Si es actualización conservar la imágen previa
                if(restaurante.IdRestaurante != 0)
                {
                    var result = _Irestaurante.GetById(restaurante.IdRestaurante);
                    if(result.Correct && result.Object != null)
                    {
                        var restauranteDB = (ML.Restaurante)result.Object;
                        restaurante.Imagen = restauranteDB.Imagen;
                    }
                }
            }

            if (restaurante.IdRestaurante == 0)
            {
                _Irestaurante.Add(restaurante);
            }
            else
            {
                _Irestaurante.Update(restaurante);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int idRestaurante)
        {
            _Irestaurante.Delete(idRestaurante);
            
            return RedirectToAction("GetAll");
        }
    }
}
