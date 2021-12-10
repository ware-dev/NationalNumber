using Microsoft.VisualStudio.TestTools.UnitTesting;
using NNLib;

namespace NNTest
{
    [TestClass]
    public class TestNationalNumber
    {

        [TestMethod]
        [DataRow(true,"97.09.02-363.28", DisplayName = "Test Correct Length With Special Chars")]
        [DataRow(true,"97090236328", DisplayName = "Test Correct Length Without Special Chars")]
        [DataRow(false,"97.09.02-363.280", DisplayName = "Test Incorrect Length Too Long")]
        [DataRow(false,"97.09.02-363.2", DisplayName = "Test Incorrect Length Too Short")]
        public void TestLength(bool expectedResult, string number)
        {
            bool result = NationalNumber.IsValid(number);
            Assert.AreEqual(expectedResult,result);
        }

        [TestMethod]
        [DataRow(true,"97.09.02-363.28", DisplayName = "Test Correct BirthDate")]
        [DataRow(false,"31.16.02-363.28", DisplayName = "Test Incorrect BirthDate Year Greater Than 30")]
        [DataRow(false,"29.16.02-363.28", DisplayName = "Test Incorrect BirthDate Year Less Than 30")]
        [DataRow(false,"97.16.02-363.28", DisplayName = "Test Incorrect BirthDate Month Greater Than 12")]
        [DataRow(false,"97.00.02-363.28", DisplayName = "Test Incorrect BirthDate Month Less Than 0")]
        [DataRow(false,"97.16.00-363.28", DisplayName = "Test Incorrect BirthDate Day Less Than 0")]
        [DataRow(false,"97.09.32-363.28", DisplayName = "Test Incorrect BirthDate Day Greater Than Month Days")]
        [DataRow(false,"20.02.30-363.28", DisplayName = "Test Incorrect BirthDate February Leap")]
        [DataRow(false,"21.02.30-363.28", DisplayName = "Test Incorrect BirthDate February NotLeap")]
        public void TestBirthDate(bool expectedResult, string number)
        {
            bool result = NationalNumber.IsValid(number);
            Assert.AreEqual(expectedResult,result);
        }

        [TestMethod]
        [DataRow(true,"00.04.11-318.88", DisplayName = "Test Correct Control Digits Millenial Ok")]
        [DataRow(true,"17073003384", DisplayName = "Test Correct Control Digits Millenial Ok 2")]
        [DataRow(true,"97.09.02-363.28", DisplayName = "Test Correct Control Digits Not Millenial Ok")]
        [DataRow(false,"97.09.02-363.98", DisplayName = "Test Correct Control Digits Greater Than 97")]
        [DataRow(false,"97.09.02-363.00", DisplayName = "Test Correct Control Digits Less Than 1")]
        public void TestControlDigits(bool expectedResult, string number)
        {
            bool result = NationalNumber.IsValid(number);
            Assert.AreEqual(expectedResult,result);
        }
    }
}
