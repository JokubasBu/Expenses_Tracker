using Microsoft.AspNetCore.Components.Forms;

namespace ExpensesTracker.Client.Services.FileService
{
    public interface IFileService
    {
        public Task<string> ReadFiles(IReadOnlyList<IBrowserFile>? selectedFiles, string? message);
        string ConvertBytesToString(byte[] bytes);
    }
}
