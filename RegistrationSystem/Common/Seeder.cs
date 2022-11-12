using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Common
{
    public class Seeder
    {
        private readonly ControllerBase _sut;

        public Seeder(ControllerBase sut)
        {
            _sut = sut;
        }
        public void SeedUserMockRole()
        {
            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                new Claim("UserId", "2"),
                new Claim(ClaimTypes.Role, "User")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
        }

        public void SeedAdminMockRole()
        {
            var userMock = new ClaimsPrincipal(new ClaimsIdentity(
                new Claim[]
                {
                new Claim("UserId", "2"),
                new Claim(ClaimTypes.Role, "Admin")
                }));

            _sut.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = userMock }
            };
        }
    }
}