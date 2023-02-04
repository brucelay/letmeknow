namespace LetMeKnowAPI.Models.Functions;

using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class TextFunction : IWorkflowFunction<TextResult>
{
    private string _targetPhoneNumber;
    private string _outgoingPhoneNumber;
    private readonly string _OutMessage;
    private readonly string _SID;
    private readonly string _AuthToken;

    private TextResult _textResult;
    
    public TextFunction(string targetPhoneNumber, string outgoingPhoneNumber, string outMessage, string sid, string authToken)
    {
        _targetPhoneNumber = targetPhoneNumber;
        _outgoingPhoneNumber = outgoingPhoneNumber;
        _OutMessage = outMessage;
        _SID = sid;
        _AuthToken = authToken;
    }
    
    public void RunFunction()
    {
        TwilioClient.Init(_SID, _AuthToken);
            
        var message = MessageResource.Create(
            to: new PhoneNumber(_targetPhoneNumber), // Replace with your recipient's phone number
            from: new PhoneNumber(_outgoingPhoneNumber), // Replace with a Twilio-provided phone number
            body: _OutMessage);
        
        _textResult = new TextResult(true);
    }

    public TextResult GetResult()
    {
        return _textResult;
    }
}