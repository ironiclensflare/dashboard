using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dashboard.Models;
using System.Net;

namespace DashboardTests
{
    [TestClass]
    public class ApiControllerTests
    {
        [TestMethod]
        public void TestUserCreation()
        {
            GDSignup s = new GDSignup();
            s.SignUpUser("spam@ironiclensflare.com", "UKNOTTSCC_1");
            
            Assert.IsTrue(s.statusCode == HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestInvalidTopic()
        {
            GDSignup s = new GDSignup();
            s.SignUpUser("spam@ironiclensflare.com", "UKNOTTSCC_3");

            Assert.IsTrue(s.statusCode == HttpStatusCode.BadRequest);
        }
    }
}
