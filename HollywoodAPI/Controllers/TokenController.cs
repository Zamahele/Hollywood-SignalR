using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HollywoodAPI.Data;
using HollywoodAPI.Model.Token;
using HollywoodAPI.Model.Tournament;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace HollywoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public TokenController(IConfiguration config, ApplicationDbContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Token>> GetToken(string userId)
        {
            if (userId != null)
            {
                var user = await GetUser(userId);
                if (user != null)
                {   
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                        new Claim("Id", user.Id),
                        new Claim("UserName", user.UserName),
                        new Claim("Email", user.Email),
                        new Claim("SecurityStamp", user.SecurityStamp)

                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    var generatedToken =new Token{TokenCode = new JwtSecurityTokenHandler().WriteToken(token).ToString()};
                    return generatedToken;
                }
                else
                {
                    return new Token();
                }
            }
            else
            {
                return new Token();
            }
        }

        //[HttpPost]
        //public async Task<OkObjectResult> Post(UserInfo userData)   
        //{

        //    if (userData?.Email != null && userData.Password != null)
        //    {
        //        var user = await GetUser(userData.Email, userData.Password);

        //        if (user != null)
        //        {
        //            //create claims details based on the user information
        //            var claims = new[] {
        //            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
        //            new Claim("Id", user.UserId.ToString()),
        //            new Claim("FirstName", user.FirstName),
        //            new Claim("LastName", user.LastName),
        //            new Claim("UserName", user.UserName),
        //            new Claim("Email", user.Email)
        //           };

        //            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

        //            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

        //            //return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        //        }
        //        else
        //        {
        //            return new OkObjectResult("Invalid credentials");
        //        }
        //    }
        //    else
        //    {
        //        return  new OkObjectResult(BadRequest());
        //    }
        //}


        private async Task<AspNetUsers> GetUser( string userId)
        {
            try
            {
                return  await _context.AspNetUsers.FirstOrDefaultAsync(x=>x.Id == userId);
               // return await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            }
            catch (Exception e)
            {
                    Console.WriteLine(e);
                    throw;
            }
        }
    }
}