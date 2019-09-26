using SoftwareLibrary.Models;
using System.Collections.Generic;

namespace SoftwareLibrary.Services
{
    public interface ISoftwareFacade
    {
        IEnumerable<Software> GetAllSoftware();
    }
}
