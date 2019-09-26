using SoftwareLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareLibrary.Services
{
    public interface ILibraryService
    {
        IEnumerable<Software> GetSoftwareByVersionNumber(string version);
    }
}
