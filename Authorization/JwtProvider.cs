using DataAccess.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authorization;

public static class JwtProvider
{
    public static  string CreateToken(User user)
    {
        var claims = new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
        };

        JwtSecurityToken jwtSecurityToken = new(
            issuer: "Erdem Sincer",
            audience: "www.erdemsincer.com",
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes("mysecretmysecretmysecretmysecretmysecretmysecretmysecretmysecretmysecret"))
            , SecurityAlgorithms.HmacSha256)
            );

        string token=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }
}
