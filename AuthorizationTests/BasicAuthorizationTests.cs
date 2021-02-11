using System;
using System.IO;
using Authorization;
using NUnit.Framework;


namespace AuthorizationTests
{
    public class BasicAuthorizationTests
    {
        [Test, TestCaseSource(typeof(UserDataMock))]
        public void LoginTests(User.User user)
        {
            try
            {
                new BasicAuthorization().Login(user).Wait();
            }
            catch (Exception e)
            {
                Assert.Fail($"Authorization not success. User information: {user}.\n\nException: {e.Message}");
            }


            Assert.Pass();
        }

    }
}