using SoftwareLibrary.Models;
using System.Collections.Generic;

namespace SoftwareLibrary.Services
{
    public interface ILibraryService
    {
        IEnumerable<Software> GetSoftwareGreaterThanVersionNumber(string version);
    }
}