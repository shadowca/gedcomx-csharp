using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using ApprovalTests.Reporters;
using Gedcomx.File;
using Gx.Agent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gedcomx.Model.Test {
    [TestClass]
    public class Serialization_of_agent {

        readonly XmlSerializer _serializer = new XmlSerializer(typeof(Agent));
        private string Serialize(Agent agent) {
            var string_builder = new StringBuilder();
            using (var string_writer = new StringWriter(string_builder)) {
                _serializer.Serialize(string_writer, agent);
            }
            return string_builder.ToString();

        }

        [TestMethod]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void When_agent_is_empty() {
            ApprovalTests.Approvals.VerifyXml(Serialize(new Agent()));
        }

        [TestMethod]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void When_agent_has_id() {
            var agent = new Agent { Id = "IamAId" };
            ApprovalTests.Approvals.VerifyXml(Serialize(agent));
        }

        [TestMethod]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void When_agent_has_name_mail_and_id() {
            var agent = (Agent)new Agent().SetName("Jane Doe").SetEmail("example@example.org").SetId("A-1");
            ApprovalTests.Approvals.VerifyXml(Serialize(agent));

        }

        [TestMethod]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void When_agent_has_online_account() {
            var agent = new Agent();
            agent.AddAccount(new OnlineAccount() {AccountName = "AccountName", Id = "onlineAccountId"});
            ApprovalTests.Approvals.VerifyXml(Serialize(agent));

        }
    }
}
