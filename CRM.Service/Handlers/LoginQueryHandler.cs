using Exemplo.Domain.Model.Dto;
using Exemplo.Persistence;
using Exemplo.Service.Commands;
using Exemplo.Service.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MPS.Exemplo.Domain.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exemplo.Service.Handlers
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery,LoginDto>
    {
        private readonly ExemploDbContext _context;
        private readonly SettingsWebApi _settings;

        public LoginQueryHandler(ExemploDbContext context, SettingsWebApi settings)
        {
            _context = context;
            _settings = settings;
        }

        public async Task<LoginDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u=>u.Usuario == request.Usuario);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.SenhaHash))
                return null;

            var claims = new[]
            {
                new Claim("User",request.Usuario),
                new Claim("Name", usuario.Nome),
                new Claim("IsAdmin",usuario.IsAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.AuthSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _settings.AuthSettings.Issuer,
            audience: _settings.AuthSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
