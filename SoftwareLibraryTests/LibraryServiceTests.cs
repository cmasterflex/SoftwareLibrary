using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SoftwareLibrary.Models;
using SoftwareLibrary.Services;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareLibraryTests
{
    [TestClass]
    public class LibraryServiceTests
    {

        [TestMethod]
        public void FiltersWhenVersionsAreEqual()
        {

            var expectedName = "software2";

            var expectedList = new List<Software>{
                new Software { Name = "software1", Version = "1" },
                new Software { Name = expectedName, Version = "2" }
            };

            var mockSoftwareFacade = new Mock<ISoftwareFacade>();
            mockSoftwareFacade.Setup(x => x.GetAllSoftware()).Returns(expectedList);
            var target = new LibraryService(mockSoftwareFacade.Object);

            var actual = target.GetSoftwareGreaterThanVersionNumber("1").ToList();

            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.First().Name == expectedName);
        }

        [TestMethod]
        public void FltersMajorVersionCorrectly()
        {

            var expectedName = "software3";

            var expectedList = new List<Software>{ 
                new Software { Name = "software1", Version = "1" }, 
                new Software { Name = "software2", Version = "2" }, 
                new Software { Name = expectedName, Version = "3" }
            }; 

            var mockSoftwareFacade = new Mock<ISoftwareFacade>();
            mockSoftwareFacade.Setup(x => x.GetAllSoftware()).Returns(expectedList);
            
            var target = new LibraryService(mockSoftwareFacade.Object);

            var actual = target.GetSoftwareGreaterThanVersionNumber("2").ToList();

            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.First().Name == expectedName);
        }

        [TestMethod]
        public void FltersMinorVersionCorrectly()
        {

            var expectedName = "software3";

            var expectedList = new List<Software>{
                new Software { Name = "software1", Version = "1.0" },
                new Software { Name = "software2", Version = "1.1" },
                new Software { Name = expectedName, Version = "1.2" }
            };

            var mockSoftwareFacade = new Mock<ISoftwareFacade>();
            mockSoftwareFacade.Setup(x => x.GetAllSoftware()).Returns(expectedList);
            var target = new LibraryService(mockSoftwareFacade.Object);

            var actual = target.GetSoftwareGreaterThanVersionNumber("1.1").ToList();

            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.First().Name == expectedName);
        }

        [TestMethod]
        public void FltersPatchCorrectly()
        {

            var expectedName = "software3";

            var expectedList = new List<Software>{
                new Software { Name = "software1", Version = "1.1.0" },
                new Software { Name = "software2", Version = "1.1.1" },
                new Software { Name = expectedName, Version = "1.1.2" }
            };

            var mockSoftwareFacade = new Mock<ISoftwareFacade>();
            mockSoftwareFacade.Setup(x => x.GetAllSoftware()).Returns(expectedList);
            var target = new LibraryService(mockSoftwareFacade.Object);

            var actual = target.GetSoftwareGreaterThanVersionNumber("1.1.1").ToList();

            Assert.IsTrue(actual.Count == 1);
            Assert.IsTrue(actual.First().Name == expectedName);
        }

        [TestMethod]
        public void HandlesIncompleteVersionRequest()
        {

            var expectedName = "software3";

            var expectedList = new List<Software>{
                new Software { Name = "software1", Version = "1.1" },
                new Software { Name = "software2", Version = "1.1.1" },
                new Software { Name = expectedName, Version = "1.1.2" }
            };

            var mockSoftwareFacade = new Mock<ISoftwareFacade>();
            mockSoftwareFacade.Setup(x => x.GetAllSoftware()).Returns(expectedList);
            var target = new LibraryService(mockSoftwareFacade.Object);

            var actual = target.GetSoftwareGreaterThanVersionNumber("1.1.").ToList();

            Assert.IsTrue(actual.Count == 2);
        }
    }
}