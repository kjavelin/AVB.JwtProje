﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AVB.JwtProje.BusinessLayer.Interfaces;
using AVB.JwtProje.BusinessLayer.StringInfos;
using AVB.JwtProje.Entities.Concrete;
using Microsoft.IdentityModel.Tokens;

namespace AVB.JwtProje.BusinessLayer.Concretes
{
    public class JwtManager : IJwtService
    {
        public string GenerateJwtToken(AppUser appUser, List<AppRole> roles)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.SecurityKey));


            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer:JwtInfo.Issuer,audience:JwtInfo.Audience,notBefore:DateTime.Now,expires:DateTime.Now.AddMinutes(JwtInfo.TokenExpiration),signingCredentials: signingCredentials,claims:GetClaims(appUser,roles));


            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(jwtSecurityToken);

        }

        private List<Claim> GetClaims(AppUser appUser, List<AppRole> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name , appUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()));

            if (roles?.Count > 0)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
            }

            return claims;
        }

    }
}
