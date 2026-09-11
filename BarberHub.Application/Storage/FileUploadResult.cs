using BarberHub.Domain.Enums;

namespace BarberHub.Application.Storage;

public record FileUploadResult(string Url, string Key, StorageProvider StorageProvider);