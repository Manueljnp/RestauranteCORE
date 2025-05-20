using DL;
using Microsoft.AspNetCore.Mvc;

namespace PL_Web.Controllers
{
    public class RestauranteController : Controller
    {
        private readonly BL.IRestaurante _Irestaurante;
        public RestauranteController(BL.IRestaurante Irestaurante)
        {
            _Irestaurante = Irestaurante;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Restaurante restaurante = new ML.Restaurante();
            ML.Result result = _Irestaurante.GetAll();

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
                ML.Result result = BL.Restaurante.GetById(idRestaurante.Value);
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
                    var result = BL.Restaurante.GetById(restaurante.IdRestaurante);
                    if(result.Correct && result.Object != null)
                    {
                        var restauranteDB = (ML.Restaurante)result.Object;
                        restaurante.Imagen = restauranteDB.Imagen;
                    }
                }
            }

            if (restaurante.IdRestaurante == 0)
            {
                BL.Restaurante.Add(restaurante);
            }
            else
            {
                BL.Restaurante.Update(restaurante);
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int idRestaurante)
        {
            BL.Restaurante.Delete(idRestaurante);
            
            return RedirectToAction("GetAll");
        }
    }
}
