using Microsoft.AspNetCore.Components.Forms;
using ExpensesTracker.Client.Services.ExpensesService;

namespace ExpensesTracker.Client.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly IExpensesService expensesService;

        public FileService(IExpensesService expensesService)
        {
            this.expensesService = expensesService;
        }

        public async Task<string> ReadFiles(IReadOnlyList<IBrowserFile> selectedFiles, string? message)
        {
            foreach (var file in selectedFiles)
            {
                if (file.ContentType == "text/plain")
                {
                    var stream = new MemoryStream();
                    await file.OpenReadStream().CopyToAsync(stream);
                    string fullList = ConvertBytesToString(stream.ToArray());

                    List<string> lines = new List<string>();
                    lines = fullList.Split('\n').ToList<string>();

                    foreach (string line in lines)
                    {
                        if (line.Length >= 6)
                        {
                            try
                            {
                                List<string> list = new List<string>();
                                list = line.Split(',').ToList();
                                Expense expense = new Expense {
                                    Money = Double.Parse(list.ElementAt(0)), 
                                    Comment = list.ElementAt(1), 
                                    CategoryId = Int32.Parse(list.ElementAt(2)), 
                                    Year = Int32.Parse(list.ElementAt(3)), 
                                    Month = Int32.Parse(list.ElementAt(4)), 
                                    Day = Int32.Parse(list.ElementAt(5)) };

                                await expensesService.CreateExpense(expense);

                            }
                            catch (Exception e)
                            {
                                message = "Some expenses were written in invalid form :/";
                            }
                        }
                    }
                }
                else
                {
                    message = "Please select .txt file!";
                }
            }
            return message;
        }

        public string ConvertBytesToString(byte[] bytes)
        {
            string output = String.Empty;
            MemoryStream stream = new MemoryStream(bytes);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                output = reader.ReadToEnd();
            }
            return output;
        }
    }
}
