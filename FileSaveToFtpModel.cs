namespace Nlabs.FileService;
public class FileSaveToFtpModel
{
    public FileSaveToFtpModel(string ftpAddress, string userName, string password)
    {
        FtpAddress = ftpAddress;
        UserName = userName;
        Password = password;
    }
    public string FtpAddress { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
}