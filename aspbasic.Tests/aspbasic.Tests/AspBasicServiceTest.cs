using System;
using System.Data.SqlClient;
using aspbasic.service;
using aspbasic.service.poco;
using Xunit;

namespace aspbasic.Tests
{
    public class AspBasicServiceTest
    {

        // TODO This is more of a functional test than a unit test and ideally this test would use some sort of database fixtures against a fresh database

        private IAspBasicService aspBasicService;
        public AspBasicServiceTest(){
            aspBasicService = new AspBasicService();
        }


        [Fact]
        public void TestGetOrders() {
            var orders = aspBasicService.GetOrders();
            Assert.NotNull(orders);
        }
       
        public class ImportTests {

            private string[] fileLines;
            private IAspBasicService aspBasicService;
            private string FILE_TYPE = AspBasicService.ORDERS;
            private ImportResult importResult;

            public ImportTests() {
                aspBasicService = new AspBasicService();
                resetDB();   
            }

              private void resetDB()
            {
                // TODO this is evil. We'd want to be more careful here incase this is run against a prod database. Also I need to learn more about fixtures and test databases in C#.
                string updateQuery = "DELETE from orders";
                using (SqlConnection connection = new SqlConnection(AspBasicService.connectionString))
                {
                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

            private void readFileLinesImportResult(string fileNameOnly) {
                fileLines = aspBasicService.ReadFile("../../../test_files/" + fileNameOnly); // TODO should be a smarter way to find the project dir
                importResult = aspBasicService.ImportFile(fileLines, FILE_TYPE);
            }

            [Fact]
            public void TestImportFile()
            {
                readFileLinesImportResult("Orders.txt");
                Assert.Equal(importResult.isFatalError, false);
                Assert.Equal(importResult.imported, 5); // TODO done differently when ensure a fresh db
            }


            [Fact]
            public void TestImportFileBadColumns()
            {
                readFileLinesImportResult("Orders_BadColumn.txt");
                Assert.Equal(importResult.isFatalError, true);
            }


            [Fact]
            public void TestImportFileMissingCustomer()
            {

                readFileLinesImportResult("Orders_MissingData.txt");
                Assert.Equal(importResult.isFatalError, false);

                Assert.Equal(importResult.skipped, 1);
                Assert.Equal(importResult.imported, 1);
            }


            [Fact]
            public void TestFulfillingOrder() {
                
            }

            [Fact]
            public void TestOtherThingsInASmartWay()
            {
            }


            // TODO I'd probably want to testing of large files here, had I implemented streaming of the files on upload

        }


    }
}
