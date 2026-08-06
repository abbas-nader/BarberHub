using System.Runtime.InteropServices;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Exceptions;

namespace BarberHub.Domain.Entities;

public class Review : BaseEntity
{
    public byte Rating { get; private set; }
    public string Comment { get; private set; } = null!;
    public bool IsApproved { get; private set; }
    public string? Reply { get; private set; } = null!;

    public long CustomerId { get; private set; }
    public long BarberId { get; private set; }
    public long AppointmentId { get; private set; }
    public long SalonId { get; private set; }

    private Review()
    {
    }

    private Review(byte rating, string comment, bool isApproved, long customerId, long barberId, long appointmentId,
        long salonId,
        long userId)
    {
        Rating = rating;
        Comment = comment;
        IsApproved = isApproved;
        CustomerId = customerId;
        BarberId = barberId;
        AppointmentId = appointmentId;
        SalonId = salonId;
    }

    public static Review Create(byte rating, string comment, bool isApproved, long customerId, long barberId,
        long appointmentId, long salonId,
        long userId)
    {
        ValidateRating(rating);
        ValidateComment(comment);
        var review = new Review(rating, comment, isApproved, customerId, barberId, appointmentId, salonId, userId);
        review.SetCreationInfo(userId);
        return review;
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

        MarkAsModified(customerId);
    }

    public void Approve(long adminId)
    {
        if (IsApproved) throw new ReviewAlreadyApprovedException();
        IsApproved = true;
        MarkAsModified(adminId);
    }

    public void InsertReply(string reply, long adminId)
    {
        if (!IsApproved) throw new ReviewNotApprovedException();
        if (string.IsNullOrWhiteSpace(reply)) throw new InvalidReplyException();
        Reply = reply;
        MarkAsModified(adminId);
    }


    public void DeleteReply(long userId)
    {
        Reply = null;
        MarkAsModified(userId);
    }

    private static void ValidateRating(byte rating)
    {
        if (rating is < ReviewConstant.RateMinLength or > ReviewConstant.RateMaxLength)
            throw new InvalidRatingException();
    }

    private static void ValidateComment(string comment)
    {
        if (string.IsNullOrWhiteSpace(comment))
            throw new InvalidCommentException();
    }
}