using Microsoft.BingAds.V13.CampaignManagement;
using OptmyzrHelpers;
using OptmyzrHelpers.Blueprints;
using OptmyzrHelpers.Data_Classes;
using MySql.Data.MySqlClient;

namespace Assignment5
{
    public class Blueprints
    {

        public static Stream workingStatus()
        {
            Console.WriteLine("Blueprints hit");
            return new MemoryStream();  
        }

        public static List<BlueprintRow> getAllBluePrints()
        {
            OptmyzrMySqlHelper optmyzrMySqlHelper = new OptmyzrMySqlHelper();
            var users = OptmyzrUser.GetAllUsers();
            List<BlueprintRow> blueprintRows = new List<BlueprintRow>();
            foreach ( var user in users )
            {
                var blueprintRow = optmyzrMySqlHelper.GetAllBlueprintsForUser(user.UserId.ToString());
                blueprintRows.AddRange(blueprintRow);
            }

            return blueprintRows;
        }

        public static List<BlueprintTaskRow> getAllBluePrintTasks()
        {

            OptmyzrMySqlHelper optmyzrMySqlHelper = new OptmyzrMySqlHelper();
            var users = OptmyzrUser.GetAllUsers();

            List<BlueprintTaskRow> result = new List<BlueprintTaskRow>();

            foreach ( var user in users )
            {
                int uid = user.UserId;
                using (var connection = optmyzrMySqlHelper.GetMySqlConnection())
                {
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = $"SELECT * from blueprint_tasks WHERE user_id = {uid}";

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var taskDetails = new BlueprintTaskRow();
                        taskDetails.id = reader.GetInt32("id");
                        taskDetails.bp_id = reader.GetString("source_info_1");
                        taskDetails.bp_task_id = reader.GetString("source_info_2");
                        taskDetails.title = reader.GetString("title");
                        taskDetails.description = reader.GetString("description");
                        taskDetails.task_role_id = reader.GetInt32("task_role_id");
                        taskDetails.curr_owner_user_id = reader.GetInt32("current_owner_user_id");
                        taskDetails.due_date = reader.GetDateTime("due_date");
                        taskDetails.account_id = reader.GetString("account_id");
                        taskDetails.task_source = reader.GetString("task_source");
                        taskDetails.timezone = reader.GetString("timezone");
                        taskDetails.task_settings = reader.GetString("task_settings");
                        result.Add(taskDetails);
                    }
                }
            }
            return result;


        }




    }
}
