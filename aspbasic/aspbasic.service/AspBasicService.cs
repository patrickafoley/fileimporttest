using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using aspbasic.service.poco;

namespace aspbasic.service
{
    public class AspBasicService : IAspBasicService
    {

        public static string CUSTOMERS = "Customers";
        public static string ORDERS = "Orders";
        public static string STUDYGUIDES = "StudyGuides";

        public static string connectionString =
            "Data Source=(local);Database=aspbasic;User=sa;Password=Passw0rd!"; // TODO shouldn't use sa here. Also should be in an external config

        private Dictionary<string, string[]> FileTypes = new Dictionary<string, string[]>() {
            {CUSTOMERS, new string[]{"CUSTOMERID", "CUSTOMER NAME", "CUSTOMER EMAIL"}},
            {ORDERS, new string[]{"CUSTOMERID", "STUDY GUIDE IDS ORDERED"}},
            {STUDYGUIDES, new string[]{"STUDY GUIDE CODE", "STUDY GUIDE NAME", "PRICE"}}
         };

        // checks to see if the right headers are used for a file type
        public Boolean IsFileValid(string[] lines, string fileType)
        {
            if (lines != null && lines[0].Split("|").SequenceEqual(FileTypes[fileType]))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string[] GetFileTypes()
        {
            return FileTypes.Keys.ToArray();
        }

        public string[] ReadFile(string filePath)
        {
            return System.IO.File.ReadAllLines(filePath);
        }


        private ImportResult ImportOrders(string[] lines)
        {

            var result = new ImportResult();
            var skipped = 0;
            var imported = 0;

            IImportFile importFile = new ImportOrders();

            if (lines != null)
            {

                for (int indx = 1; indx < lines.Length; indx++)
                {
                    var row = lines[indx];
                    if (row != null)
                    {
                        try
                        {
                            if (importFile.ImportRow(row, FileTypes[ORDERS]))
                            {
                                imported++;
                            }
                            else
                            {
                                skipped++;
                            }
                        }
                        catch (Exception sql)
                        {
                            // TODO should use proper logging facility here     
                            Console.WriteLine(sql.ToString());
                            skipped++;
                            result.errorText = "Import of row '" + row + "' Failed \nError:\n" + result.errorText + sql.Message; // TODO ideally we would make sense of these errors and provide the user with "Customer Not Found for Order Row <rowText>"
                        }
                    }else{
                        skipped++;
                    }

                }

            }
            result.skipped = skipped;
            result.imported = imported;
            return result;

        }


        // these would be done with a similar pattern using a new implementation of the IImportFile interface
        private ImportResult ImportCustomers(string[] lines)
        {
            var result = new ImportResult();
            return result;
        }


        private ImportResult ImportStudyGuides(string[] lines)
        {
            var result = new ImportResult();
            return result;
        }

        /**
         * 
         * Marks an order fulfilled and sets the datetime that it was fulfilled. 
         */
        public void FulfillOrder(int orderId)
        {
            string updateQuery = "update orders set fulfilled = @filled, dateFulfilled = @dateFilled where orderId = @orderId ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("orderId", orderId);
                command.Parameters.AddWithValue("filled", 1);
                command.Parameters.AddWithValue("dateFilled", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            // TODO Exception Handling
        }

        /*
         * Retrieves all of the orders from the database  
        */
        public Order[] GetOrders()
        {
            string queryString =
                "select o.fulfilled, c.customerId, c.customerName, c.customerEmail, o.studyGuidesOrdered, o.orderId, o.dateFulfilled " +
                "from orders o, customers c " +
                "where o.customerId = c.customerId";

            List<Order> orders = new List<Order>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlCommand command = new SqlCommand(queryString, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // TODO refactor the order in which fields are returned... 


                    // TODO investigate GetFieldValue<T> - this might be a better approach

                    var order = new Order
                    {
                        orderId = (int)reader[5],
                        fulfilled = (bool)reader[0],

                        customer = new Customer()
                        {
                            customerId = (string)reader[1],
                            customerName = (string)reader[2],
                            customerEmail = (string)reader[3]
                        }

                    };

                    // TODO best way to do this? I'd prefer to format it in the view.
                    if (reader[6] != DBNull.Value)
                    {
                        order.dateFulfilled = (DateTime)reader[6];
                    }

                    var studyguideCodes = ((string)reader[4]).Split(",");

                    List<StudyGuide> studyGuides = new List<StudyGuide>();
                    foreach (var codeStr in studyguideCodes)
                    {
                        int code = -1;
                        try
                        {
                            code = Int32.Parse(codeStr);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.ToString());
                            // TODO should validate these on the way in - a join table/fk will help
                        }

                        studyGuides.Add(new StudyGuide
                        {
                            studyGuideCode = code
                        });
                    }

                    order.studyGuides = studyGuides.ToArray();
                    orders.Add(order);
                }
                reader.Close();


                // ideally this would be in an additional join table or a structure that is easier to query. 
                // Otherwise I could probably get it working with a STRING_SPLIT function and some subqueries.
                // I've added this additional query here instead in the interest of time.


                //var guideQuery = "SELECT studyGuideCode, studyGuideName, price where studyGuideCode = @code";

                //foreach(Order order in orders){
                //    foreach(StudyGuide sg in order.studyGuides){
                //        SqlCommand guideCommand = new SqlCommand(guideQuery, connection);
                //        command.Parameters.AddWithValue("code", sg.studyGuideCode);
                //    }
                //}

                connection.Close();

                // TODO exception handling 

            }

            return orders.ToArray();
        }

        public ImportResult ImportFile(string[] lines, string fileType)
        {
            if (fileType == CUSTOMERS)
            {
                return ImportCustomers(lines);
            }
            else if (fileType == ORDERS)
            {
                return ImportOrders(lines);
            }
            else if (fileType == STUDYGUIDES)
            {
                return ImportStudyGuides(lines);
            }
            else
            {
                return new ImportResult()
                {
                    isFatalError = true,
                    errorText = "Error processing file - invalid file format."
                };
            }
        }
    }
}
