using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;

namespace BizTalkComponents.PipelineComponents.PromoteConstantValues.Tests.UnitTests
{
    [TestClass]
    public class PromoteConstantValuesTests
    {
        [TestMethod]
        public void PromoteConstantValuesUnitTest()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            var component = new BizTalkComponents.PipelineComponents.PromoteConstantValues.PromoteConstantValues
            {
                Enabled = true,
                PropertyValuePairArray = @"{http://test#property1,somevalue};{http://namespacetest#property2,waving};{http://testing#property3,parsing}"
            };
            pipeline.AddComponent(component, PipelineStage.Validate);
            var message = MessageHelper.Create("");
            var ret = pipeline.Execute(message);
            Assert.IsTrue(message.Context.IsPromoted("property1", "http://test"));
            Assert.IsTrue(message.Context.IsPromoted("property2", "http://namespacetest"));
            Assert.IsTrue(message.Context.IsPromoted("property3", "http://testing"));
        }

        [TestMethod]
        public void PromoteConstantValuesWithWrongFormat()
        {
            var pipeline = PipelineFactory.CreateEmptyReceivePipeline();
            var component = new BizTalkComponents.PipelineComponents.PromoteConstantValues.PromoteConstantValues
            {
                Enabled = true,
                PropertyValuePairArray = @"{http://test/property1,someva}"
            };
            pipeline.AddComponent(component, PipelineStage.Validate);
            var message = MessageHelper.Create("");
            var ret = pipeline.Execute(message);
            Assert.IsTrue(message.Context.IsPromoted("property1", "http://test"));
            Assert.IsTrue(message.Context.IsPromoted("property2", "http://namespacetest"));
            Assert.IsTrue(message.Context.IsPromoted("property3", "http://testing"));
        }
    }
}
