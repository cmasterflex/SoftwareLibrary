using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareLibrary.Models;
using SoftwareLibrary.Services;

namespace SoftwareLibrary.Controllers
{
    [Route("api/[controller]")]
    public class LibraryController : Controller
    {
        private ILibraryService _libraryService;
        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        [HttpGet("[action]")]
        public IEnumerable<Software> Software(string version)
        {
            return _libraryService.GetSoftwareGreaterThanVersionNumber(version);
        }
    }
}