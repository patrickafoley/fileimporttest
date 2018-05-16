using System;
namespace aspbasic.service
{
    public interface IImportFile
    {

        bool IsRowValid(string row, string[] columns);
        bool IsRowNew(string row);
        bool ImportRow(string row, string[] columns);

    }
}
