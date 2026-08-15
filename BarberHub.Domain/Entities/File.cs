using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class File : BaseEntity
{
    public string FileName { get; private set; } = null!;
    public string OriginFileName { get; private set; } = null!;
    public string Url { get; private set; } = null!;
    public string ContentType { get; private set; } = null!;
    public long Size { get; private set; }

    private File()
    {
    }

    public File(string fileName, string originFileName, string url, string contentType, long size, long creationBy)
    {
       ValidateFileName(fileName);
       ValidateOriginFileName(originFileName);
       ValidateUrl(url);
       ValidateContentType(contentType);
       ValidateSize(size);
        FileName = fileName;
        OriginFileName = originFileName;
        Url = url;
        ContentType = contentType;
        Size = size;

        Creation(creationBy);
    }

    private static void ValidateFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new RequiredFieldException(nameof(fileName));
    }

    private static void ValidateOriginFileName(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new RequiredFieldException(nameof(fileName));
    }

    private static void ValidateUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new RequiredFieldException(nameof(url));
    }

    private static void ValidateContentType(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            throw new RequiredFieldException(nameof(contentType));
    }
private  static void ValidateSize(long size)
    {
        if (size <= FileConstants.SizeMinLength)
            throw new InvalidFileSizeException();
    }
}