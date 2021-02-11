using System;
using Authorization;
using NUnit.Framework;


namespace AuthorizationTests
{
    public class BasicAuthorizationTests
    {
        [Test, TestCaseSource(typeof(AuthorizationTests.UserDataMock))]
        public void LoginTests(Core.User user)
        {
            try
            {
                new BasicAuthorization(user).Login().Wait();
            }
            catch (Exception e)
            {
                Assert.Fail($"Authorization not success. User information: {user}.\n\nException: {e.Message}");
            }


            Assert.Pass();
        }

    }
}