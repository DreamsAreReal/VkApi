using Authorization;
using NUnit.Framework;

namespace AuthorizationTests
{
    public class BasicAuthorizationTests
    {
        [Test, TestCaseSource(typeof(UserDataMock))]
        public void LoginTests(User.User user)
        {

            if (new BasicAuthorization().Login(user))
            {
                Assert.Pass();
            }

            Assert.Fail($"Authorization not success. User information: {user}");
        }
    }
}