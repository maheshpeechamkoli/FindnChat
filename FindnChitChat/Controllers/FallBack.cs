using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace FindnChitChat.Controllers
{
    public class FallBack : ControllerBase
    {
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
            "wwwwroot", "index.html"), "text/HTML");
        }
    }
}