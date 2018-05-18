using System;
using System.Data.SqlClient;

namespace aspbasic.service
{
    public class ImportOrders : IImportFile
    {
        public ImportOrders()
        {
        }

        private static string connectionString = AspBasicService.connectionString;

        public bool ImportRow(string row, string[] columns)
        { 

            var vals = row.Split("|");
            var customerId = vals[0];
            var studyGuidesOrdered = vals[1];

            if (IsRowValid(row, columns) && IsRowNew(row))
            {

                string updateQuery = "INSERT INTO ORDERS(customerId, studyGuidesOrdered) VALUES(@customerId, @studyGuidesOrdered);";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("customerId", customerId);
                    command.Parameters.AddWithValue("studyGuidesOrdered", studyGuidesOrdered);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                return true;

            }

            return false; 
        }


        /* 
         * Checks the orders table for this row
        */
        public bool IsRowNew(string row)
        {

            var vals = row.Split("|"); 
            var customerId = vals[0];
            var studyGuidesOrdered = vals[1]; 
            string orderQuery = "select COUNT(*) from orders where customerId = @customerId and studyGuidesOrdered = @studyGuides ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(orderQuery, connection);
                command.Parameters.AddWithValue("customerId", customerId);
                command.Parameters.AddWithValue("studyGuides", studyGuidesOrdered);
                connection.Open();

                var sqlCount = (int)command.ExecuteScalar();
                if(sqlCount > 0) {
                    connection.Close();
                    return false; 
                } else {
                    connection.Close();
                    return true;
                }
            }
        }

        public bool IsRowValid(string row, string[] columns)
        {
            try
            {
                // make sure the row has the right number of columns 
                var splitRow = row.Split("|");
                if ((splitRow != null && splitRow.Length == columns.Length))
                {
                    // make sure each of those columns is not null 
                    foreach (var str in splitRow)
                    {
                        if (String.IsNullOrEmpty(str))
                        {
                            return false;
                        }
                    }

                    return true;

                }
                else
                {
                    return false;
                }
            }catch (Exception e) {
                return false; 
            }
        }
    }
}
