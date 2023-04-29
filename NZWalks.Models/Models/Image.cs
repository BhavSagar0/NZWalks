using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Domain.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
    }
}
