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
        private List<List<long>> _sentMessagesData;

        [OneTimeSetUp]
        public void MessagesServiceTestsSetup()
        {
            _user = new UserDataMock().User;
            new BasicAuthorization(_user).Login().Wait();
            _sentMessagesData = new List<List<long>>();

        }

        [TestCase(-1, "Test!")]
        public void SendTests(long id, string text)
        {
            MessagesService messagesService = new MessagesService(_user);
            long idMessage = messagesService.Send(id, text).Result;
            if (idMessage != 0)
            {
                List<long> messagesData = new List<long> {id, idMessage};
                _sentMessagesData.Add(messagesData);
                Assert.Pass();
            }
            Assert.Fail($"Message not sent. PeerId: {id}. Message: {text}.\n{_user}.");

        }




        [OneTimeTearDown]
        public void MessagesServiceTearDown()
        {
            MessagesService messagesService = new MessagesService(_user);
            for (int i = 0; i < _sentMessagesData.Count; i++)
            {
                messagesService.DeleteMessageForAll(_sentMessagesData[i][0], _sentMessagesData[i][1]).Wait();
            }
        }
    }
}