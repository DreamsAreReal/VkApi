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
            new BasicAuthorization(_user).Login().Wait();
            _messagesIds = new List<long>();

        }

        [Test, TestCase(569878520, "Hello Test")]
        public void SendToUserTests(long id, string text)
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

        [Test, TestCase(181495053, "Hello Test")]
        public void SendToGroupTests(long id, string text)
        {
            MessagesService messagesService = new MessagesService(_user);
            long idMessage = messagesService.SendToGroup(id, text).Result;
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