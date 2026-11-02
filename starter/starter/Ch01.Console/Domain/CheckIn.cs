namespace EduHub.Ch01.Domain;

/// <summary>
/// Các hình thức điểm danh hợp lệ.
/// </summary>
public static class CheckInMethod
{
    public const string Manual = "Manual";
    public const string QrCode = "QrCode";
}

/// <summary>
/// Ghi nhận sự hiện diện thực tế của một Registration (quan hệ 1–1).
/// </summary>
public class CheckIn
{
    public Guid Id { get; private set; }
    public Guid RegistrationId { get; private set; }
    public DateTime CheckedInAt { get; private set; }
    public string Method { get; private set; }

    private CheckIn(Guid registrationId, string method)
    {
        Id = Guid.NewGuid();
        RegistrationId = registrationId;
        CheckedInAt = DateTime.UtcNow;
        Method = method;
    }

    /// <summary>
    /// Điểm danh cho một phiếu đăng ký (chỉ cho phép khi registration.Status == Confirmed).
    /// </summary>
    public static CheckIn Create(Registration registration, string method = CheckInMethod.Manual)
    {
        ArgumentNullException.ThrowIfNull(registration);

        if (registration.Status != RegistrationStatus.Confirmed)
            throw new InvalidOperationException("Chỉ phiếu đã Confirmed mới được check-in.");

        if (method is not (CheckInMethod.Manual or CheckInMethod.QrCode))
            throw new ArgumentException($"Hình thức điểm danh không hợp lệ: {method}.", nameof(method));

        return new CheckIn(registration.Id, method);
    }

    /// <summary>
    /// Chuỗi mô tả ngắn để in báo cáo.
    /// </summary>
    public string Summary() => $"Check-in {CheckedInAt:HH:mm:ss} qua {Method} (phiếu {RegistrationId.ToString()[..8]})";
}