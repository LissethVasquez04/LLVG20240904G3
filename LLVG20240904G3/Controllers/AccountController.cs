using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LLVG20240904G3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            // Comprueba si las credenciales son válidas
            if (login == "admin" && password == "12345")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login)
                };
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    // Puedes configurar propiedades adicionales aquí
                    // Por ejemplo, para hacer que la cookie persista:
                    IsPersistent = true,
                    // Para especificar una fecha de expiración:
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                };
                // Inicia sesión del usuario
                HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity), authProperties);
                return Ok("Inició sesión correctamente.");
            }
            else
            {
                return Ok("Credenciales Incorrectas.");

            }
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Cierra la sesión del usuarios
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Devuelve una respuesta exitosa
            return Ok("Cerró sesión correctamente.");
        }
    }

}
