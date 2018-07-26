using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using APIEndPoint.Models;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIEndPoint.Controllers
{
    [Route("api/[controller]")]
    public class V3Controller : ControllerBase
    {
        private readonly V3Context _context;

        public V3Controller(V3Context context)
        {
            _context = context;

            if (_context.V3s.Count() == 0)
            {
                _context.V3s.Add(new V3 { X = 0.2f, Y = 0.2f, Z = 0.2f });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public List<V3> GetAll()
        {
            return _context.V3s.ToList();
        }

        [HttpPost]
        public IActionResult Create([FromBody] V3 item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            //C:\Bin\Kinova_CartesianControl\Debug>WindowsExample_CartesianControl.exe -0.3 0.3 0.3
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "C:\\Bin\\Kinova_CartesianControl\\Debug\\WindowsExample_CartesianControl.exe";
            float _x = -item.X;
            float _y = -item.Z;
            float _z = item.Y;
            startInfo.Arguments = string.Format("{0} {1} {2}", _x, _y, _z);
            Process.Start(startInfo);

            _context.V3s.Add(item);
            _context.SaveChanges();

            // Edit exe above to only output final cartesian coordinates (no other output, likely only XYZ)
            // Then use code at link below to capture output and store it in another "table"
            // Then update "GetAll" with View page to show content from input and output side by side for calibration of base placement in Unity
            // https://stackoverflow.com/questions/4291912/process-start-how-to-get-the-output

            return Ok();
        }

    }
}
