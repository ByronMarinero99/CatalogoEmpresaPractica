using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using CatalogoEmpresasG3.LogicaDeNegocio;
using CatalogoEmpresasG3.EntidadesDeNegocio;

namespace CatalogoEmpresasG3.InterfazGraficaMVC.Controllers
{
    public class ContactoController : Controller
    {
        ContactoBL contactoBL  = new ContactoBL(); //instancia de acceso a los métodos de la clase BL

        // Acción que muestra la página principal de Contactos
        public async Task<ActionResult> Index (Contacto pContacto = null)
        {
            if (pContacto == null)
                pContacto = new Contacto();
            if (pContacto.top_aux == 0)//Si no ha puesto cuantos quiere mostrar
                pContacto.top_aux = 10; //Trae 10 registros por default
            else if (pContacto.top_aux == -1)//Reseteamos
                pContacto.top_aux = 0;

            var contactos = await contactoBL.BuscarAsync(pContacto);
            ViewBag.Top = pContacto.top_aux;
            return View(contactos);
        }
        // Acción que muestra el detalle de un registro
        public async Task<IActionResult> Details(int id)
        {
            var contacto = await contactoBL.ObtenerPorIDAsync(new Contacto { ID=id });
            return View(contacto);
        }
        //Acción que muestra el formulario para agregar un contacto nuevo
        //No ponemos async porque no estamos yendo a la BD
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        //Acción que recibe los datos del formulario para enviarlos a la BD
        //Ponemos async porque vamos a la BD a buscar registros
        //ActionResult devuelve cualquier cosa
        //IActionResult más genérico
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacto pContacto)
        {
            try
            {
                int result = await contactoBL.CrearAsync(pContacto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pContacto);
            }
        }
        //Acción que muestra los datos del registro cargados en el formulario para editarlos
        public  async Task<IActionResult> Edit(Contacto pContacto)
        {
            var contacto = await contactoBL.ObtenerPorIDAsync(pContacto);
                ViewBag.Error = "";
            return View(contacto);
        }
        //Acción que recibe los datos modificados y los envía a la BD
        [HttpPost] 
        [ValidateAntiForgeryToken] //Herramienta que sirve para evitar que nos hagan inyecciones en SQL
        public async Task <IActionResult> Edit(int id,  Contacto pContacto)
        {
            try
            {
                int result = await contactoBL.ModificarAsync(pContacto);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = "";
                return View(pContacto);
            }
        }
        //Acción que muestra página para confirmar la eliminación
        public async Task<IActionResult> Delete(Contacto pContacto)
        {
            ViewBag.Error = "";

            var contacto = await contactoBL.ObtenerPorIDAsync(pContacto);
            return View(contacto);
        }
        //Acción que recibe la confirmación para eliminar el registro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Contacto pContacto)
        {
            try
            {
                int result = await contactoBL.EliminarAsync(pContacto);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pContacto);
            }
        }
    }
}