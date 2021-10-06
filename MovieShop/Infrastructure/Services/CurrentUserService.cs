using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
namespace Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId => Convert.ToInt32( _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public bool IsAuthenticated => _httpContextAccessor != null && _httpContextAccessor.HttpContext.User.Identity!= null  &&  _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string Email => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value + " " +
            _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;

        public string RemoteIpAddress => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        public bool IsAdmin
        {
            get{
                var roles = _httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == "Roles").ToList();
                foreach(var role in roles)
                {
                    if (role.Value == "Admin")
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsSuperAdmin
        {
            
            get{
                var roles = _httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == "Roles").ToList();
                foreach (var role in roles)
                {
                    if (role.Value == "Super Admin")
                    {
                        return true;
                    }
                }
                return false;
            }
            
        }


        public IEnumerable<string> Roles
        {
            get
            {
                var roles = _httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == "Roles").ToList();
                List<string> roleList = new(); 
                foreach (var role in roles)
                {
                    roleList.Add(role.Value);
                }
                return roleList; 
            }
        }
    }
}
