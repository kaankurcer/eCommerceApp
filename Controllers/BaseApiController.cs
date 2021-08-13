using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class BaseApiController : ControllerBase
    {
        public string connectionString = "Data Source=DESKTOP-51VU883\\SQLEXPRESS;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}