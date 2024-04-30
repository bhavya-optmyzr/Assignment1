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


//CampaignPerformanceReportFetching

using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V15.Common;
using Google.Ads.GoogleAds.V15.Enums;
using Google.Ads.GoogleAds.V15.Errors;
using Google.Ads.GoogleAds.V15.Resources;
using Google.Ads.GoogleAds.V15.Services;
using Google.Api.Gax;
using Google.Apis.Sheets.v4.Data;
using OptmyzrHelpers;
using System;

namespace CampaignPerformanceReport
{
    public class Program
    {
        public static byte[] Run(GoogleAdsClient client, string accountId)
        {
            var returnableData = new List<string[]>();
            string[] fields = new string[] { "id", "name", "clicks", "impressions", "cost" };
            try
            {
                string query = @"
                    SELECT
                        campaign.id,
                        campaign.name,
                        metrics.clicks,
                        metrics.impressions,
                        metrics.cost_micros
                    FROM
                        campaign
                    WHERE
                        segments.date BETWEEN '2010-01-01' AND '2024-01-31' ";

                GoogleAdsServiceClient googleAdsService = client.GetService(
                    Google.Ads.GoogleAds.Services.V15.GoogleAdsService);

                // Creates a query request.
                SearchGoogleAdsRequest request = new SearchGoogleAdsRequest()
                {
                    CustomerId = accountId,
                    Query = query
                };

                // Retrieves the campaigns.
                PagedEnumerable<SearchGoogleAdsResponse, GoogleAdsRow> response =
                    googleAdsService.Search(request);

                var sortedResponse = response.OrderBy(x => x.Campaign.Id);

                // Prints the results.
                foreach (GoogleAdsRow googleAdsRow in sortedResponse)
                {
                    Campaign campaign = googleAdsRow.Campaign;
                    Metrics metrics = googleAdsRow.Metrics;

                    var temp = new string[]
                {
                    campaign.Id.ToString(),
                   campaign.Name,
                    metrics.Clicks.ToString(),
                    metrics.Impressions.ToString(),
                    metrics.CostMicros.ToString()
                };
                    returnableData.Add(temp);
                    //Console.WriteLine($"Campaign ID: {campaign.Id}, Name: {campaign.Name}, Clicks: {metrics.Clicks}, Impressions: {metrics.Impressions}, Cost: {metrics.CostMicros}");
                }

            }
            catch (GoogleAdsException e)
            {
                Console.WriteLine("Google Ads API request failed. See error message for more details:");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to retrieve campaign performance data:");
                Console.WriteLine(e.Message);
            }
            return OptmyzrProcessor.Helpers.JsonStreamMaker(returnableData, fields);
        }
    }
}
