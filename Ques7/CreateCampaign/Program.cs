

using CommandLine;
using Google.Ads.Gax.Examples;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V15.Common;
using Google.Ads.GoogleAds.V15.Errors;
using Google.Ads.GoogleAds.V15.Resources;
using Google.Ads.GoogleAds.V15.Services;
using System;
using System.Collections.Generic;
using static Google.Ads.GoogleAds.V15.Enums.AdvertisingChannelTypeEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.BudgetDeliveryMethodEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.CampaignStatusEnum.Types;
using static Google.Ads.GoogleAds.V15.Resources.Campaign.Types;

namespace Google.Ads.GoogleAds.Examples.V15
{
    public class CreateCampaign : ExampleBase
    {
        public static string fn()
        {
            int userid = 2;
            string email = "bhavya@optmyzr.com";
            //string accountid "1425022273";
            long mccID = 4804910527;
            
            CreateCampaign codeExample = new CreateCampaign();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(GoogleAdsOperations.AuthHelpers.GetGoogleAdsClient(userid, email, "4804910527", mccID.ToString()),
                mccID);
            return "done";
        }

        private const int NUM_CAMPAIGNS_TO_CREATE = 3;

        public override string Description => "This code example adds campaigns.";
        public void Run(GoogleAdsClient client, long customerId)
        {
            // Get the CampaignService.
            CampaignServiceClient campaignService = client.GetService(Services.V15.CampaignService);

            // Create a budget to be used for the campaign.
            string budget = CreateBudget(client, customerId);

            List<CampaignOperation> operations = new List<CampaignOperation>();

            for (int i = 0; i < NUM_CAMPAIGNS_TO_CREATE; i++)
            {
                // Create the campaign.
                Campaign campaign = new Campaign()
                {
                    Name = "Interplanetary Cruise #" + ExampleUtilities.GetRandomString(),
                    AdvertisingChannelType = AdvertisingChannelType.Search,
                    //Status = CampaignStatus.Paused,

                    // Set the bidding strategy and budget.
                    ManualCpc = new ManualCpc(),
                    CampaignBudget = budget,

                    // Set the campaign network options.
                    NetworkSettings = new NetworkSettings
                    {
                        TargetGoogleSearch = true,
                        TargetSearchNetwork = true,
                        TargetContentNetwork = true,
                        TargetPartnerSearchNetwork = false
                    },

                    // Optional: Set the start date.
                    StartDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),

                    // Optional: Set the end date.
                    EndDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd"),
                };

                // Create the operation.
                operations.Add(new CampaignOperation() { Create = campaign });
            }
            try
            {
                // Add the campaigns.
                MutateCampaignsResponse retVal = campaignService.MutateCampaigns(
                    customerId.ToString(), operations);

                // Display the results.
                if (retVal.Results.Count > 0)
                {
                    foreach (MutateCampaignResult newCampaign in retVal.Results)
                    {
                        Console.WriteLine("Campaign with resource ID = '{0}' was added.",
                            newCampaign.ResourceName);
                    }
                }
                else
                {
                    Console.WriteLine("No campaigns were added.");
                }
            }
            catch (GoogleAdsException e)
            {
                Console.WriteLine("Failure:");
                Console.WriteLine($"Message: {e.Message}");
                Console.WriteLine($"Failure: {e.Failure}");
                Console.WriteLine($"Request ID: {e.RequestId}");
                throw;
            }
        }

        private static string CreateBudget(GoogleAdsClient client, long customerId)
        {
            // Get the BudgetService.
            CampaignBudgetServiceClient budgetService = client.GetService(
                Services.V15.CampaignBudgetService);

            // Create the campaign budget.
            CampaignBudget budget = new CampaignBudget()
            {
                Name = "Interplanetary Cruise Budget #" + ExampleUtilities.GetRandomString(),
                DeliveryMethod = BudgetDeliveryMethod.Standard,
                AmountMicros = 500000
            };

            // Create the operation.
            CampaignBudgetOperation budgetOperation = new CampaignBudgetOperation()
            {
                Create = budget
            };

            // Create the campaign budget.
            MutateCampaignBudgetsResponse response = budgetService.MutateCampaignBudgets(
                customerId.ToString(), new CampaignBudgetOperation[] { budgetOperation });
            return response.Results[0].ResourceName;
        }
    }
}

