using System.Collections;
using System.Collections.Generic;

namespace AuthorizationTests
{
    /// <summary>
    /// This class need to all tests where using authorization.
    ///
    /// Create the same test mock like this class in this project. With the name UserDataMock.
    /// </summary>
    public class UserDataMockExample : IEnumerable
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


        /// <summary>
        /// This need only login tests
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            yield return new Core.User("login", "password");
            yield return new Core.User("login1", "password1");
        }

    }
}