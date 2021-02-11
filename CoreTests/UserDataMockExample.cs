using System.Collections;
using System.Collections.Generic;

namespace CoreTests
{
    // TODO: Replace to AuthTest.

    /// <summary>
    /// This class need to all tests where using authorization.
    /// I added it here to not have dependencies with authorization.
    ///
    /// Create the same test mock like this class in this project. With the name UserDataMock.
    /// </summary>
    public class UserDataMockExample
    {

        /// <summary>
        /// This property will be used to test all methods where authorization is needed.
        /// </summary>
        public Core.User User
        {
            get
            {
                return new Core.User("test", "pass");
            }
        }

        public IEnumerator GetEnumerator()
        {
            yield return new Core.User("login", "password");
            yield return new Core.User("login1", "password1");
        }

    }
}