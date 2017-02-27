using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NumberJumbler.Models;

namespace NumberJumbler.Tests.Models
{
    [TestClass]
    public class NumberJumblerServicesTest
    {
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [TestMethod]
        public void FindLeader()
        {
            //Arrange
            NumberJumblerServices services = new NumberJumblerServices();
            NumberJumblerResult finalPositive;
            NumberJumblerResult finalNegative;



            //Act
            using (var stream = GenerateStreamFromString("1,1,1,1,1,5\n"))
            {
                finalPositive = services.FindLeader(stream);
            }

            using (var stream = GenerateStreamFromString("1,1,-2,4,4,5\n"))
            {
                finalNegative = services.FindLeader(stream);
            }


            //Assert
            Assert.AreEqual(1, finalPositive.Result);
            Assert.AreEqual("Negative number detected in input file.", finalNegative.ErrorMessage);

        }

        [TestMethod]
        public void FindLeaderNoValidation()
        {
            //Arrange
            NumberJumblerServices logic = new NumberJumblerServices();
            NumberJumblerResult finalPositive;
            NumberJumblerResult finalNegative;


            //Act
            using (var stream = GenerateStreamFromString("1,1,1,1,1,5\n"))
            {
                finalPositive = logic.FindLeaderNoValidation(stream);
            }

            using (var stream = GenerateStreamFromString("1,1,2,4,4,5\n"))
            {
                finalNegative = logic.FindLeaderNoValidation(stream);
            }

            //Assert
            Assert.AreEqual(1, finalPositive.Result);
            Assert.AreEqual(-1, finalNegative.Result);
            
        }

        [TestMethod]
        public void IsValid()
        {
            //Arrange
            NumberJumblerServices logic = new NumberJumblerServices();
            string errorMessageValid = "an error has occurred";
            string errorMessageInvalid = "";
            bool valid = false;
            bool decreasingOrder = false;
            bool containsNegativeNumber = false;
            bool digitNotInteger = false;
            bool tooManyItems = false;

            string extraLongString = "1";
            for (int i = 1; i <= 100001; i++)
            {
                extraLongString = extraLongString + "," + i.ToString();
            }
            extraLongString = extraLongString + "\n";

                //Act
            using (var stream = GenerateStreamFromString("1,3,5,6,6,6,6,6,6,6,7\n"))
            {
                valid = logic.isValid(stream, out errorMessageValid);
            }

            using (var stream = GenerateStreamFromString("1,5,6,1,3,2,33,55,1,1,1,1\n"))
            {
                decreasingOrder = !logic.isValid(stream, out errorMessageInvalid);
            }

            using (var stream = GenerateStreamFromString("-1,1,1,1,-6\n"))
            {
                containsNegativeNumber = !logic.isValid(stream, out errorMessageInvalid);
            }

            using (var stream = GenerateStreamFromString("1,21564748336477\n"))
            {
                digitNotInteger = !logic.isValid(stream, out errorMessageInvalid);
            }

            using (var stream = GenerateStreamFromString(extraLongString))
            {
                tooManyItems = !logic.isValid(stream, out errorMessageInvalid);
            }


            //Assert
            Assert.AreEqual(true, valid);

            Assert.AreEqual(true, decreasingOrder);

            Assert.AreEqual(true, containsNegativeNumber);

            Assert.AreEqual(true, digitNotInteger);

            Assert.AreEqual(true, tooManyItems);

     


        }

    }
}
