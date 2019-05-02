using System;
using BirdsSoundsClassifier.Controllers.api;
using BirdsSoundsClassifier.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BirdsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterTest()
        {

            MobileController a = new MobileController();

            RegisterViewModel model = new RegisterViewModel { Email = "sbd4920045@gmail.com", Password = "Cosmos@#123", ConfirmPassword = "Cosmos@#123" };
            // a.CreateUserAsync(model);
        }


        [TestMethod]
        public void LoginTest()
        {

            MobileController a = new MobileController();
            //a.getBirdsListAll();
        }



        [TestMethod]
        public void UploadTest()
        {

            MobileController a = new MobileController();

           // a.uploadImage();
        }







    }
    
}
