using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Puntos_NET.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll();
			usuario.Usuarios = result.Objects;
			return View(usuario);
        }
		public IActionResult DarDeAlta()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Form(int? IdUsuario)
		{
			ML.Usuario usuario = new ML.Usuario();
			if(IdUsuario != null)
			{
				ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
				if (result.Correct)
				{
					usuario = (ML.Usuario )result.Object;
				}
				else
				{

				}
			}
			return View(usuario);
		}
		[HttpPost]
		public IActionResult Form(ML.Usuario usuario)
		{
			if (ModelState.IsValid)
			{
                if (usuario.IdUsuario == 0)
                {
                    var result = BL.Usuario.Add(usuario);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha ingresado correctamente el Usuario";
                    }
                    else
                    {
                        ViewBag.Mensaje = "NO se ha ingresado correctamente el Usuario";
                    }
                }
                else
                {
                    var result = BL.Usuario.Update(usuario);
                    if (result != null)
                    {
                        ViewBag.Mensaje = "Se ha actualizado correctamente el Usuario";
                    }
                    else
                    {
                        ViewBag.Mensaje = "NO se ha actualizado correctamente el Usuario";
                    }
                }
            }
			else
			{

			}

            return PartialView("Modal");
        }
		[HttpGet]
		public IActionResult Login()
		{
			return View();
        }
        [HttpPost]
        public IActionResult Login(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
			if (result.Correct)
			{
				HttpContext.Session.SetInt32("UserID", (int)result.Object);
				ViewBag.Mensaje = "Se a creado la cuenta Correctamente";
			}
			else
			{
				ViewBag.Mensaje = "Error: No se pudo crear la cuenta de Usuario ";
			}
			return PartialView("Modal");
		}

		public IActionResult Logout(string username, string password)
		{
			ML.Result result = BL.Usuario.GetByEmail(username);
			if (result.Correct)
			{
				ML.Usuario usuario = (ML.Usuario)result.Object;
				if (password == "12345")
				{
					return RedirectToAction("ChangePassword", "Usuarios");
					ViewBag.Mensaje = "Es necesario cambiar la contraseña";
				}
				else if (password == usuario.Password)
				{
					HttpContext.Session.SetInt32("UserID", usuario.IdUsuario);
                    HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
                    return RedirectToAction("index", "Home");
				}
				else
				{
					ViewBag.Mensaje = "Contraseña Incorrecta";
					ViewBag.Correo = false;
					return RedirectToAction("Login", "Usuario");
				}
			}
			else
			{
				ViewBag.Mensaje = "No existe la cuenta";
				ViewBag.Correo = false;
				return PartialView("Modal");
			}
		}
		[HttpGet]
		public IActionResult ChangePassword()
		{
			return View();
		}
		[HttpPost]
		public IActionResult ChangePassword(string newPassword)
		{
			int? IdUsuario = HttpContext.Session.GetInt32("UserID");

			if (IdUsuario.HasValue)
			{
				ML.Result result = BL.Usuario.ChangePassword(IdUsuario.Value, newPassword);
				if (result.Correct)
				{
					ViewBag.Mensaje = "Se ha cambiado correctamente el usuario";
					return RedirectToAction("Login", "Usuarios");
				}
				else
				{
					result.Correct = false; 
					return RedirectToAction("ChangePassword", "Usuarios");
				}
			}
			else
			{
				ViewBag.Mensaje = "Usted no tiene Cuenta con nosotros";
			}

			return View();
		}
	}
}
