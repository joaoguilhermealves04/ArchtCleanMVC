using CleanArcheMvc.API.Models;
using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArcheMvc.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authenticate;
        private readonly IConfiguration _configuration;
        public TokenController(IAuthenticate authenticate,IConfiguration configuration)
        {
            _authenticate = authenticate ??
                throw new ArgumentNullException(nameof(authenticate));
            _configuration = configuration;
        }

        [HttpPost("LoginUser")]
        public async Task <ActionResult<UserToken>>Login ([FromBody] LoginModel userInfor)
        {
            var result =await _authenticate.Authenticate(userInfor.Email, userInfor.Password);

            if (result)
            {
                return GenereteToken(userInfor);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel userInfor)
        {
            var result = await _authenticate.RegistrerUser(userInfor.Email, userInfor.Password);

            if (result)
            {
                return Ok($"User {userInfor.Email} was create Successfully");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                return BadRequest(ModelState);
            }
        }
        private ActionResult<UserToken> GenereteToken(LoginModel userInfor)
        {
            // declaraçoes do usuario 

            var claims = new[]
            {
                 new Claim("email",userInfor.Email),
                 new Claim("meuvalor","oque voce quiser"),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            // gerar chave privada para  assinar o token

            var privateKeY = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Secretkey"]));

            // gerar a asssinatura do digital

            var credentials = new SigningCredentials(privateKeY, SecurityAlgorithms.HmacSha256);

            // definir o tempo de expiração 

            var expiration = DateTime.UtcNow.AddMinutes(10);

            // gerar o token

            JwtSecurityToken token = new JwtSecurityToken(
                //Emissor
                issuer: _configuration["Jwt:Issuer"],
                //audiencia
                audience: _configuration["Jwr:Audience"],
                //claims
                claims: claims,
                //data de Expiraçao
                expires: expiration,
                //Assinatura Digital
                signingCredentials: credentials
                );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
