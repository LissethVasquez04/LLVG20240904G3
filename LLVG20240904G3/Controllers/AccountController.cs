using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace LLVG20240904G3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Simulación de una base de datos con usuarios y contraseñas
        private static Dictionary<string, string> usuarios = new Dictionary<string, string>
        {
            {"Admin", "123456"},
          
        };
        
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Validación de credenciales contra la base de datos simulada
            if (usuarios.TryGetValue(username, out var storedPassword) && storedPassword == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();

                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok("Inicio de sesión exitoso.");
            }
            else
            {
                return Unauthorized("Credenciales inválidas.");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Cierre de sesión exitoso.");
        }
    }
}