using SoftwareLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareLibrary.Services
{
    public class LibraryService : ILibraryService
    {
        public LibraryService(ISoftwareFacade softwareFacade)
        {
            _softwareFacade = softwareFacade;
        }
        private readonly ISoftwareFacade _softwareFacade;
        private List<Software> _software;

        public List<Software> Software => _software ?? (_software = _softwareFacade.GetAllSoftware().ToList());

        public IEnumerable<Software> GetSoftwareGreaterThanVersionNumber(string requestedVersion)
        {
            var ret = Software;
            return ret.Where(x => IsGreater(x.Version, requestedVersion)).OrderBy(x => x.Name);
            //return ret;
        }

        private bool IsGreater(string current, string requested)
        {
            if (requested.EndsWith('.')) requested = requested.TrimEnd('.');
            var partsCurrent = current.Split('.');
            var lengthCurrent = partsCurrent.Length;
            var partsRequested = requested.Split('.');
            var lengthRequested = partsRequested.Length;

            // test majorVersion
            if (lengthCurrent > 0 && lengthRequested > 0)
            {
                var majorVersionCurrent = int.Parse(partsCurrent[0]);
                var majorVersionRequested = int.Parse(partsRequested[0]);
                if (majorVersionCurrent > majorVersionRequested) return true;
                if (majorVersionCurrent < majorVersionRequested) return false;
            }
            //major versions are the same
            //test minor version

            //if current has a minor version and the requested version does not,
            //current is automatically greater
            //if the request has a minor version and current does not,
            //current is automatically less
            if (lengthCurrent == 2 && lengthRequested == 1) return true;
            if (lengthCurrent == 1 && lengthRequested == 2) return false;

            //if they both have minor versions, compare them
            if (lengthCurrent > 1 && lengthRequested > 1)
            {
                var minorVersionCurrent = int.Parse(partsCurrent[1]);
                var minorVersionRequest = int.Parse(partsRequested[1]);
                if (minorVersionCurrent > minorVersionRequest) return true;
                if (minorVersionCurrent < minorVersionRequest) return false;
            }
            //minor versions are the same
            //test patch

            //if current has a patch number and the requested version does not,
            //current is automatically greater
            //if the request has a minor version and current does not,
            //current is automatically less
            if (lengthCurrent == 3 && lengthRequested < 3) return true;
            if (lengthCurrent < 3 && lengthRequested == 3) return false;
            
            //if they both have patch numbers, compare them
            if (lengthCurrent > 2 && lengthRequested > 2)
            {
                var patchCurrent = int.Parse(partsCurrent[2]);
                var patchRequest = int.Parse(partsRequested[2]);
                if (patchCurrent > patchRequest) return true;
                if (patchCurrent < patchRequest) return false;
            }

            //if they get to this point, then they have the exact same version number
            //in that case, return false, since equal is technically not greater than
            return false;
        }

    }
}