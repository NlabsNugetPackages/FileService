using Microsoft.AspNetCore.Http;
using System.Net;

namespace Nlabs.GenericFileSrevice;
public static class FileService
{
    public static string FileSaveToServer(IFormFile file, string filePath)
    {
        var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
        fileFormat = fileFormat.ToLower();
        var fileName = Guid.NewGuid().ToString() + fileFormat;
        var path = filePath + fileName;
        using (var stream = File.Create(path))
        {
            file.CopyTo(stream);
        }
        return fileName;
    }

    public static string FileSaveToFtp(IFormFile file, FileSaveToFtpModel fileSaveToFtpModel)
    {
        var fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));
        fileFormat = fileFormat.ToLower();
        var fileName = Guid.NewGuid().ToString() + fileFormat;

        var request = (FtpWebRequest)WebRequest.Create($"{fileSaveToFtpModel.FtpAddress}{fileName}");
        request.Credentials = new NetworkCredential(
            fileSaveToFtpModel.UserName,
            fileSaveToFtpModel.Password);
        request.Method = WebRequestMethods.Ftp.UploadFile;

        using (Stream ftpStream = request.GetRequestStream())
        {
            file.CopyTo(ftpStream);
        }

        return fileName;
    }

    public static byte[] FileConvertByteArrayToDatabase(IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var fileString = Convert.ToBase64String(fileBytes);
            return fileBytes;
        }
    }

    public static void FileDeleteToServer(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        catch (Exception)
        {
        }
    }

    public static void FileDeleteToFtp(string path, FileSaveToFtpModel fileSaveToFtpModel)
    {
        try
        {
            var request = (FtpWebRequest)WebRequest.Create($"{fileSaveToFtpModel}{path}");
            request.Credentials = new NetworkCredential(
                fileSaveToFtpModel.UserName,
                fileSaveToFtpModel.Password);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        }
        catch (Exception)
        {
        }
    }
}