using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Infrastructure;
using BLL.Interfaces;
using BLL.Services;
using System.Collections.Generic;
using System.Linq;
using BLL.DataTransferObjects;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        public void GetBooksTestMethod()
        {
            IStatisticService serv = new StatisticService();
            List<BookDTO> books = serv.GetBooksToRead(1).ToList();
            int len = books.Count;
            int expected = 3;


            Assert.AreEqual(expected, len);

        }


        [TestMethod]

        public void GetBooksTestMethod2()
        {
            IStatisticService serv = new StatisticService();
            List<BookDTO> books = serv.GetBooksToRead(1).ToList();
            int len = books.Count;
            int expected = 0;


            Assert.AreNotEqual(expected, len);

        }
    }
}
