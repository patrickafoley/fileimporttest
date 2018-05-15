using System;
using System.Collections.Generic;
using System.Linq;

namespace aspbasic.service
{
    public class AspBasicService : IAspBasicService
    {

        private static string CUSTOMERS = "Customers";
        private static string ORDERS = "Orders";
        private static string STUDYGUIDES = "StudyGuides";

        private Dictionary<string, string[]> FileTypes = new Dictionary<string, string[]>() {
            {CUSTOMERS, new string[]{"CUSTOMERID", "CUSTOMER NAME", "CUSTOMER EMAIL"}},
            {ORDERS, new string[]{"CUSTOMERID", "STUDY GUIDE IDS ORDERED"}},
            {STUDYGUIDES, new string[]{"STUDY GUIDE CODE", "STUDY GUIDE NAME", "PRICE"}}
         };


        public Boolean IsFileValid(string[] lines, string fileType)
        {
            if(lines[0].Split("|").SequenceEqual(FileTypes[fileType])){
                return true;
            }else {
                return false;
            }
        }

        public string[] GetFileTypes() {
            return FileTypes.Keys.ToArray();
        }

        public string[] ReadFile(string filePath) {
            return System.IO.File.ReadAllLines(filePath);
        }


        private ImportResult ImportCustomers(string[] lines)
        {
            var result = new ImportResult();
            return result;
        }

        private ImportResult ImportOrders(string[] lines)
        {

            var result = new ImportResult();
            return result;

        }

        private ImportResult ImportStudyGuides(string[] lines)
        {

            var result = new ImportResult();
            return result;

        }


        public ImportResult ImportFile(string[] lines, string fileType) {
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
                    isError = true,
                    errorText = "Error processing file - invalid file format."
                };
            }
        }
    }
}
