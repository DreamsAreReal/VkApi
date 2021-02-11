using System.Collections.Generic;
using Authorization;
using AuthorizationTests;
using NUnit.Framework;
using User;

namespace UserTests
{
    public class MessagesServiceTests
    {
        private Core.User _user;
        private List<long> _messagesIds;

        [OneTimeSetUp]
        public void MessagesServiceTestsSetup()
        {
            _user = new UserDataMock().User;
            new BasicAuthorization(_user).Login();
        }

        [Test, TestCase(1, "1")]
        public void SendTests(long id, string text)
        {
            MessagesService messagesService = new MessagesService(_user);
            long idMessage = messagesService.SendToUser(id, text).Result;
            if (idMessage != 0)
            {
                _messagesIds.Add(idMessage);
                Assert.Pass();
            }
            Assert.Fail($"Message not sent. PeerId: {id}. Message: {text}.\n{_user}.");

        }

        [OneTimeTearDown]
        public void MessagesServiceTearDown()
        {
            // Todo delete sent messages
        }
    }
}