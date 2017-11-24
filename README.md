# PromoteConstantValues
BizTalk pipeline component to promote properties with their correspondent constant values

PropertyValuePairArray must be set in the following format:
{namesapce#propertyname,value}
{http://schemas.microsoft.com/BizTalk/2006/as2-properties#AS2From,TestAS2Sender};{http://schemas.microsoft.com/BizTalk/2006/as2-properties#AS2To,TestAS2Receiver}

The component can be used in either Receive Or Send pipelines at any stage/category.

