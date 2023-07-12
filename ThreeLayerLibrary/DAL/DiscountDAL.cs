using Persistence;
using MySqlConnector;
namespace DAL
{
    public class DiscountPolicyDAL
    {

        private MySqlConnection connection = DbConfig.GetConnection();
        private string query = "";
        public DiscountPolicy GetDiscountPolicy(MySqlDataReader reader)
        {
            DiscountPolicy discountPolicy = new DiscountPolicy();
            discountPolicy.PolicyID = reader.GetInt32("Policy_ID");
            discountPolicy.PolicyName = reader.GetString("Policy_Name");
            discountPolicy.FromDate = reader.GetDateTime("From_Date");
            discountPolicy.ToDate = reader.GetDateTime("To_Date");
            discountPolicy.Description = reader.GetString("Description");
            return discountPolicy;
        }
        public List<DiscountPolicy> GetDiscountPolicies()
        {
            List<DiscountPolicy> discountPolicies = new List<DiscountPolicy>();
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                query = @"select * from discountpolicies;";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    discountPolicies.Add(GetDiscountPolicy(reader));
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            return discountPolicies;
        }
    }
}
