using System;
using aspbasic.service.poco;

namespace aspbasic.service
{
    public interface IAspBasicService
    {
        ImportResult ImportFile(string[] lines, string fileType);
        Boolean IsFileValid(string[] lines, string fileType);
        string[] ReadFile(string filePath);
        string[] GetFileTypes();
        Order[] GetOrders();
        void FulfillOrder(int orderId);
    }
}
