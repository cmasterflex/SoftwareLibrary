using SoftwareLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareLibrary.Services
{
    public class LibraryService : ILibraryService
    {
        private List<Software> _software;

        public List<Software> Software => _software ?? (_software = SoftwareManager.GetAllSoftware().ToList());

        public IEnumerable<Software> GetSoftwareByVersionNumber(string requestedVersion)
        {
            var ret = Software;
            return ret.Where(x => IsGreater(x.Version, requestedVersion)).OrderBy(x => x.Name);
            //return ret;
        }

        private bool IsGreater(string a, string b)
        {
            var partsA = a.Split('.');
            var lengthA = partsA.Length;
            var partsB = b.Split('.');
            var lengthB = partsB.Length;

            // test majorVersion
            if (lengthA > 0 && lengthB > 0)
            {
                var majorVersionA = int.Parse(partsA[0]);
                var majorVersionB = int.Parse(partsB[0]);
                if (majorVersionA > majorVersionB) return true;
                if (majorVersionA < majorVersionB) return false;
            }
            //major versions are the same
            //test minor version

            //if one has minor version and the other doesn't,
            //the one with the minor version is greater
            if (lengthA == 2 && lengthB == 1) return true;
            if (lengthA == 1 && lengthB == 2) return false;

            //if they both have minor versions, compare them
            if (lengthA > 1 && lengthB > 1)
            {
                var minorVersionA = int.Parse(partsA[1]);
                var minorVersionB = int.Parse(partsB[1]);
                if (minorVersionA > minorVersionB) return true;
                if (minorVersionA < minorVersionB) return false;
            }
            //minor versions are the same
            //test patch

            //if one has a patch number and the other doesn't,
            //the one with the patch number is greater
            if (lengthA == 3 && lengthB < 3) return true;
            if (lengthA < 3 && lengthB == 3) return false;
            
            //if they both have patch numbers, compare them
            if (lengthA > 2 && lengthB > 2)
            {
                var patchA = int.Parse(partsA[2]);
                var patchB = int.Parse(partsB[2]);
                if (patchA > patchB) return true;
                if (patchA < patchB) return false;
            }

            //if they get to this point, then they have the exact same version number
            //in that case, arbitrarily return true, list is already sorted alphabetically
            return true;
        }

    }
}