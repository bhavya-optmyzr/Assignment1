
using CommandLine;
using Google.Ads.Gax.Examples;
using Google.Ads.GoogleAds;
using Google.Ads.GoogleAds.Lib;
using Google.Ads.GoogleAds.V15.Common;
using Google.Ads.GoogleAds.V15.Errors;
using Google.Ads.GoogleAds.V15.Resources;
using Google.Ads.GoogleAds.V15.Services;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using ReportingFramework_bing;
using System;
using System.Collections.Generic;
using static Google.Ads.GoogleAds.V15.Enums.AdGroupAdStatusEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.AdGroupStatusEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.AdvertisingChannelSubTypeEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.AdvertisingChannelTypeEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.AppCampaignAppStoreEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.AppCampaignBiddingStrategyGoalTypeEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.BudgetDeliveryMethodEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.CampaignStatusEnum.Types;
using static Google.Ads.GoogleAds.V15.Enums.CriterionTypeEnum.Types;
using static Google.Ads.GoogleAds.V15.Resources.Campaign.Types;

namespace CreateCampaign
{
    public class AddAppCampaign : ExampleBase
    {

        public class Options : OptionsBase
        {
            [Option("customerId", Required = true, HelpText =
                "The Google Ads customer ID for which the call is made.")]
            public long CustomerId { get; set; }
        }


        public static void fn(long budgetAmount, string campaignName, string strategyType)
        {
            //Options options = ExampleUtilities.ParseCommandLine<Options>();

            AddAppCampaign codeExample = new AddAppCampaign();
            Console.WriteLine(codeExample.Description);
            codeExample.Run(GoogleAdsOperations.AuthHelpers.GetGoogleAdsClient(2, "bhavya@optmyzr.com", "4804910527", "4804910527"), 4804910527, budgetAmount, campaignName, strategyType);
        }


        public override string Description => "This code example adds a new App Campaign.";


        public void Run(GoogleAdsClient client, long customerId, long budgetAmount, string campaignName, string strategyType)
        {
            try
            {
                // Creates a budget for the campaign.
                string budgetResourceName = CreateBudget(client, customerId, budgetAmount);

                // Creates the campaign.
                string campaignResourceName = CreateCampaign(client, customerId,
                    budgetResourceName, campaignName, strategyType);

                // Sets campaign targeting.
                SetCampaignTargetingCriteria(client, customerId, campaignResourceName);

                // Creates an ad group.
                string adGroupResourceName = CreateAdGroup(client, customerId,
                    campaignResourceName);

                // Creates an App ad.
                CreateAppAd(client, customerId, adGroupResourceName);
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

        private string CreateBudget(GoogleAdsClient client, long customerId, long budgetAmount)
        {
            // Get the BudgetService.
            CampaignBudgetServiceClient budgetService = client.GetService(
                Google.Ads.GoogleAds.Services.V15.CampaignBudgetService);

            // Creates a campaign budget.
            CampaignBudget budget = new CampaignBudget()
            {
                Name = "Interplanetary Cruise Budget #" + ExampleUtilities.GetRandomString(),
                DeliveryMethod = BudgetDeliveryMethod.Standard,
                AmountMicros = 50000,

                ExplicitlyShared = false
            };

            // Create the operation.
            CampaignBudgetOperation budgetOperation = new CampaignBudgetOperation()
            {
                Create = budget
            };

            // Create the campaign budget.
            MutateCampaignBudgetsResponse response = budgetService.MutateCampaignBudgets(
                customerId.ToString(), new CampaignBudgetOperation[] { budgetOperation });

            string budgetResourceName = response.Results[0].ResourceName;
            Console.WriteLine($"Created campaign budget with resource name " +
                $"'{budgetResourceName}'.");

            return budgetResourceName;
        }

        private string CreateCampaign(GoogleAdsClient client, long customerId,
            string budgetResourceName, string _campaignName, string strategyType)
        {
            // Get the CampaignService.
            CampaignServiceClient campaignService = client.GetService(Services.V15.CampaignService);


            Campaign campaign;
            if (strategyType == "ManualCpc")
            {
                campaign = new Campaign()
                {
                    Name = _campaignName + ExampleUtilities.GetRandomString(),
                    CampaignBudget = budgetResourceName,

                    Status = Google.Ads.GoogleAds.V15.Enums.CampaignStatusEnum.Types.CampaignStatus.Paused,


                    AdvertisingChannelType = AdvertisingChannelType.MultiChannel,
                    AdvertisingChannelSubType = AdvertisingChannelSubType.AppCampaign,


                    ManualCpc = new ManualCpc(),

                    // Sets the App campaign settings.
                    AppCampaignSetting = new AppCampaignSetting()
                    {
                        AppId = "com.google.android.apps.adwords",
                        AppStore = AppCampaignAppStore.GoogleAppStore,
                        // Optional: Optimize this campaign for getting new users for your app.
                        BiddingStrategyGoalType =
                            AppCampaignBiddingStrategyGoalType.OptimizeInstallsTargetInstallCost
                    },

                    // Optional: Set the start date.
                    StartDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),

                    // Optional: Set the end date.
                    EndDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd"),
                };
            }
            else if (strategyType == "MaximizeConversions")
            {
                campaign = new Campaign()
                {
                    Name = _campaignName + ExampleUtilities.GetRandomString(),
                    CampaignBudget = budgetResourceName,

                    Status = Google.Ads.GoogleAds.V15.Enums.CampaignStatusEnum.Types.CampaignStatus.Paused,


                    AdvertisingChannelType = AdvertisingChannelType.MultiChannel,
                    AdvertisingChannelSubType = AdvertisingChannelSubType.AppCampaign,


                    MaximizeConversions = new MaximizeConversions(),

                    // Sets the App campaign settings.
                    AppCampaignSetting = new AppCampaignSetting()
                    {
                        AppId = "com.google.android.apps.adwords",
                        AppStore = AppCampaignAppStore.GoogleAppStore,
                        // Optional: Optimize this campaign for getting new users for your app.
                        BiddingStrategyGoalType =
                            AppCampaignBiddingStrategyGoalType.OptimizeInstallsTargetInstallCost
                    },

                    // Optional: Set the start date.
                    StartDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),

                    // Optional: Set the end date.
                    EndDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd"),
                };
            }
            else
            {
                campaign = new Campaign()
                {
                    Name = _campaignName + ExampleUtilities.GetRandomString(),
                    CampaignBudget = budgetResourceName,

                    Status = Google.Ads.GoogleAds.V15.Enums.CampaignStatusEnum.Types.CampaignStatus.Paused,


                    AdvertisingChannelType = AdvertisingChannelType.MultiChannel,
                    AdvertisingChannelSubType = AdvertisingChannelSubType.AppCampaign,


                    TargetCpa = new TargetCpa()
                    {
                        TargetCpaMicros = 1000000
                    },

                    // Sets the App campaign settings.
                    AppCampaignSetting = new AppCampaignSetting()
                    {
                        AppId = "com.google.android.apps.adwords",
                        AppStore = AppCampaignAppStore.GoogleAppStore,
                        // Optional: Optimize this campaign for getting new users for your app.
                        BiddingStrategyGoalType =
                            AppCampaignBiddingStrategyGoalType.OptimizeInstallsTargetInstallCost
                    },

                    // Optional: Set the start date.
                    StartDate = DateTime.Now.AddDays(1).ToString("yyyyMMdd"),

                    // Optional: Set the end date.
                    EndDate = DateTime.Now.AddYears(1).ToString("yyyyMMdd"),
                };
            }

            // Creates a campaign operation.
            CampaignOperation operation = new CampaignOperation()
            {
                Create = campaign
            };

            // Add the campaigns.
            MutateCampaignsResponse response = campaignService.MutateCampaigns(
                customerId.ToString(), new CampaignOperation[] { operation });

            // Display the results.
            string campaignResourceName = response.Results[0].ResourceName;
            Console.WriteLine($"Created App campaign with resource name '{campaignResourceName}'.");

            return campaignResourceName;
        }


        private void SetCampaignTargetingCriteria(GoogleAdsClient client, long customerId,
            string campaignResourceName)
        {
            // Get the CampaignCriterionService.
            CampaignCriterionServiceClient campaignCriterionService = client.GetService(
                Services.V15.CampaignCriterionService);

            List<CampaignCriterionOperation> operations = new List<CampaignCriterionOperation>();


            int[] locationIds = new int[]
            {
                21137, // California
                2484 // Mexico
            };

            foreach (int locationId in locationIds)
            {
                // Creates a campaign criterion.
                CampaignCriterion campaignCriterion = new CampaignCriterion()
                {
                    Campaign = campaignResourceName,
                    Type = CriterionType.Location,
                    Location = new LocationInfo()
                    {
                        GeoTargetConstant = ResourceNames.GeoTargetConstant(locationId)
                    }
                };

                // Creates a campaign criterion operation.
                CampaignCriterionOperation operation = new CampaignCriterionOperation()
                {
                    Create = campaignCriterion
                };

                operations.Add(operation);
            }

            // Creates the language campaign criteria.
            int[] languageIds = new int[]
            {
                1000, // English
                1003 // Spanish
            };

            foreach (int languageId in languageIds)
            {
                // Creates a campaign criterion.
                CampaignCriterion campaignCriterion = new CampaignCriterion()
                {
                    Campaign = campaignResourceName,
                    Type = CriterionType.Language,
                    Language = new LanguageInfo()
                    {
                        LanguageConstant = ResourceNames.LanguageConstant(languageId)
                    }
                };

                // Creates a campaign criterion operation.
                CampaignCriterionOperation operation = new CampaignCriterionOperation()
                {
                    Create = campaignCriterion
                };

                operations.Add(operation);
            }

            // Submits the criteria operations and prints their information.
            MutateCampaignCriteriaResponse response =
                campaignCriterionService.MutateCampaignCriteria(customerId.ToString(), operations);
            Console.WriteLine($"Created {response.Results.Count} campaign criteria with " +
                $"resource names:");

            foreach (MutateCampaignCriterionResult result in response.Results)
            {
                Console.WriteLine(result.ResourceName);
            }
        }

        private string CreateAdGroup(GoogleAdsClient client, long customerId,
            string campaignResourceName)
        {
            // Get the AdGroupService.
            AdGroupServiceClient adGroupService = client.GetService(Services.V15.AdGroupService);


            AdGroup adGroup = new AdGroup()
            {
                Name = $"Earth to Mars Cruises #{ExampleUtilities.GetRandomString()}",
                Status = Google.Ads.GoogleAds.V15.Enums.AdGroupStatusEnum.Types.AdGroupStatus.Enabled,
                Campaign = campaignResourceName
            };

            // Creates an ad group operation.
            // Create the operation.
            AdGroupOperation operation = new AdGroupOperation()
            {
                Create = adGroup
            };

            // Submits the ad group operation to add the ad group and prints the results.
            MutateAdGroupsResponse response =
                adGroupService.MutateAdGroups(customerId.ToString(), new[] { operation });

            // Prints and returns the ad group resource name.
            string adGroupResourceName = response.Results[0].ResourceName;
            Console.WriteLine($"Created an ad group with resource name '{adGroupResourceName}'.");
            return adGroupResourceName;
        }


        private void CreateAppAd(GoogleAdsClient client, long customerId,
                    string adGroupResourceName)
        {
            // Get the AdGroupAdService.
            AdGroupAdServiceClient adGroupAdService = client.GetService(
                Services.V15.AdGroupAdService);

            // Creates an ad group ad.
            AdGroupAd adGroupAd = new AdGroupAd
            {
                AdGroup = adGroupResourceName,
                Status = AdGroupAdStatus.Enabled,
                Ad = new Ad
                {
                    AppAd = new AppAdInfo
                    {
                        Headlines = {
                            new AdTextAsset()
                            {
                                Text = "A cool puzzle game"
                            },
                            new AdTextAsset()
                            {
                                Text = "Remove connected blocks"
                            },
                        },
                        Descriptions = {
                            new AdTextAsset()
                            {
                                Text = "3 difficulty levels"
                            },
                            new AdTextAsset()
                            {
                                Text = "4 colorful fun skins"
                            }
                        },

                    }
                }
            };

            // Create the operation.
            AdGroupAdOperation operation = new AdGroupAdOperation
            {
                Create = adGroupAd
            };

            // Submits the ad group ad operation to add the ad group ad and prints the results.
            MutateAdGroupAdsResponse response =
                adGroupAdService.MutateAdGroupAds(customerId.ToString(), new[] { operation });
            Console.WriteLine($"Created an ad group ad with ad with resource name " +
                $"'{response.Results[0].ResourceName}'.");
        }
    }
}

