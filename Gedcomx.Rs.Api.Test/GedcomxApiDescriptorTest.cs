using System;
using NUnit.Framework;
using Gx.Rs.Api;
using System.Net;
using RestSharp;

namespace Gedcomx.Rs.Api.Test
{
	[TestFixture]
	public class GedcomxApiDescriptorTest
	{
		[Test]
		public void TestSetup ()
		{
			//make sure we accept any ssl certificates.
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
			RestClient client = new RestClient("https://sandbox.familysearch.org");
			GedcomxApiDescriptor description = new GedcomxApiDescriptor(client, ".well-known/app-meta");
			Assert.IsTrue(description.Links.Count > 0);
			Assert.IsFalse(description.Expired);
			Assert.IsTrue(description.Expiration > DateTime.Now);
			Assert.IsTrue(description.Refresh(client));
		}
	}
}

