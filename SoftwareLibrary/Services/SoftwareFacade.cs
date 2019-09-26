using System.Collections.Generic;
using SoftwareLibrary.Models;

namespace SoftwareLibrary.Services
{
    public class SoftwareFacade : ISoftwareFacade
    {
        public IEnumerable<Software> GetAllSoftware()
        {
            return SoftwareManager.GetAllSoftware();
        }
    }
}
