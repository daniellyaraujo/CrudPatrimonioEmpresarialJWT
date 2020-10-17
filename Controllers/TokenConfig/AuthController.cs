using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CrudPatrimonioEmpresarialJWT.Models.TokenConfig.IdentityUser;

namespace CrudPatrimonioEmpresarialJWT.Controllers.TokenConfig
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<AppSettings> _appSettings;


        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings;
        }

        [HttpPost("nova-conta")]
        public async Task<ActionResult> RegistrarAsync ([FromBody]RegisterUser registerUser)
        {
            //se for valido ele passa, se não for ele retorna um bad request com todos os erros que encontrou.
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUser.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);

            return Ok(await GerarJWTAsync(registerUser.Email));
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> LoginAsync([FromBody]LoginUser loginUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                return Ok(await GerarJWTAsync(loginUser.Email));
            }

            return BadRequest("Usuário ou senha Inválidos");
        }

        private async Task<string> GerarJWTAsync (string email)
        {
            _ = await _userManager.FindByEmailAsync(email);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Value.Emissor,
                Audience = _appSettings.Value.ValidoEm,
                //formato utc pra nao ter erro de onde a pessoa q executou está rs.
                Expires = DateTime.UtcNow.AddHours(_appSettings.Value.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}