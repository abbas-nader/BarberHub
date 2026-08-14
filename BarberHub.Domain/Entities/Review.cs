using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class Review : BaseEntity
{
    public byte Rating { get; private set; }
    public string Comment { get; private set; } = null!;
    public bool IsApproved { get; private set; }
    public string? Reply { get; private set; }

    public long CustomerId { get; private set; }
    public long BarberId { get; private set; }
    public long AppointmentId { get; private set; }
    public long SalonId { get; private set; }

    private Review()
    {
    }

    public Review(byte rating, string comment, long customerId, long barberId, long appointmentId,
        long salonId, long creationBy)
    {
        ValidateRating(rating);
        ValidateComment(comment);

        Rating = rating;
        Comment = comment;
        IsApproved = false;
        CustomerId = customerId;
        BarberId = barberId;
        AppointmentId = appointmentId;
        SalonId = salonId;
        Creation(creationBy);
    }

    public void EditComment(byte rating, string comment, long customerId)
    {
        ValidateRating(rating);
        ValidateComment(comment);
        if (IsApproved)
        {
            IsApproved = false;
            Reply = null;
        }
        Modified(customerId);
    }

    public void Approve(long adminId)
    {
        if (IsApproved) throw new ReviewAlreadyApprovedException();
        IsApproved = true;
        Modified(adminId);
    }

    public void InsertReply(string reply, long adminId)
    {
        if (!IsApproved) throw new ReviewNotApprovedException();
        if (string.IsNullOrWhiteSpace(reply)) throw new RequiredFieldException(reply);
        Reply = reply;
        Modified(adminId);
    }

    public void DeleteReply(long userId)
    {
        Reply = null;
        Modified(userId);
    }

    private static void ValidateRating(byte rating)
    {
        if (rating is < ReviewConstants.RateMinLength or > ReviewConstants.RateMaxLength)
            throw new InvalidRatingException();
    }

    private static void ValidateComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new RequiredFieldException(comment);
    }
}