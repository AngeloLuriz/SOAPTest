using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SOAPApi
{
    [TestClass]
    public class CountryInfoServiceTests
    {
        public readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfo =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void ListOfCountryNamesByCode()
        {
            var listOfCountryNamesByCode = countryInfo.ListOfCountryNamesByCode();
            var listOfCountryNamesResult = listOfCountryNamesByCode.OrderBy(a => a.sISOCode);
            var isAscending = listOfCountryNamesByCode.SequenceEqual(listOfCountryNamesResult);
            Assert.IsTrue(isAscending);
        }

        [TestMethod]
        public void ValidateInvalidCountryCode()
        {
            var countryCode = "B";
            var response = countryInfo.CountryName(countryCode);
            Assert.IsTrue(response.Contains("Country not found in the database"), $"Country code {countryCode} can be found in the database.");
        }

        [TestMethod]
        public void ValidateLastCountryName()
        {
            var countryList = countryInfo.ListOfCountryNamesByCode();
            var lastCountry = countryList.LastOrDefault();
            var country = countryInfo.CountryName(lastCountry.sISOCode);
            Assert.AreEqual(lastCountry.sName, country);
        }
    }
}