//in optmyzrprocessservice.cs
//Ques 06
[Route("GetCPReport/{userid}/{accountid}")]
public Stream CPReport(int userid, string accountid)
{
    BaseToken token = TokenHeaderValidHelper.GetTokenIfHeaderIsValid(Request, userid);
    int dispid = TokenHeaderValidHelper.getDisplayUserIdFromToken(Request, userid);
    string? mccid = null;
    string? email = null;
    var OMySqlHelper = new OptmyzrMySqlHelper();

    (email, mccid) = OMySqlHelper.GetEmailAndMccIdForAccount(userid.ToString(), accountid, "", "adwords", dispid);

    GoogleAdsClient client = GoogleAdsOperations.AuthHelpers.GetGoogleAdsClient(userid, email, mccid, accountid);
    byte[] byteArray = GenericOperations.GetCampaignsListWithDetailsNew(client, accountid, false, true);

    string str = Encoding.UTF8.GetString(byteArray);

    MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(str));

    return stream;
}