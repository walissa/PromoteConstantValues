using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;


namespace BizTalkComponents.PipelineComponents.PromoteConstantValues
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("92A8A903-CBF7-4548-841A-8C39F9548AAF")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public partial class PromoteConstantValues : IComponent, IBaseComponent,
                                        IPersistPropertyBag, IComponentUI
    {
        public string ConstantValue { get; set; }
        [DisplayName("PropertyValuePair Array")]
        [Description("Context properties to promote with their values {namespace1#property1,value1};{namesapce2#property2,value2};...")]
        [RegularExpression(@"^({.+?#.+?,.+?}(?:;|$))*",
         ErrorMessage = "A property path should be formatted as namespace#property.")]
        [RequiredRuntime]
        public string PropertyValuePairArray { get; set; }


        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            if (!Enabled)
                return pInMsg;
            string errorMessage;
            if (!Validate(out errorMessage))
                throw new ArgumentException(errorMessage);
            IBaseMessageContext messageContext = pInMsg.Context;
            var m = Regex.Match(PropertyValuePairArray, @"{(?<contextProperty>.+?#.+?),(?<value>.+?)}");

            while (m.Success)
            {
                string contextProperty = m.Groups["contextProperty"].Value, valueToPromote = m.Groups["value"].Value;
                messageContext.Promote(new ContextProperty(contextProperty), valueToPromote);
                m = m.NextMatch();
            }
            return pInMsg;
        }
    }
}
