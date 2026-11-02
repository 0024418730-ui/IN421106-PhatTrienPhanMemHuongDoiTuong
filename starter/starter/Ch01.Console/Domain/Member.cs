namespace EduHub.Ch01.Domain;

/// <summary>
/// Các hằng số trạng thái của thành viên.
/// </summary>
public static class MemberStatus
{
    public const string Active = "Active";
    public const string Inactive = "Inactive";
}

/// <summary>
/// Thành viên sinh viên của câu lạc bộ.
/// </summary>
public class Member
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string StudentCode { get; private set; }
    public DateTime JoinedAt { get; private set; }
    public string Status { get; private set; }

    private Member(string name, string email, string studentCode)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = DateTime.UtcNow;
        Status = MemberStatus.Active;
    }

    /// <summary>
    /// Tạo mới một thành viên sinh viên.
    /// </summary>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên thành viên không được để trống.", nameof(name));

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Mã số sinh viên không được để trống.", nameof(studentCode));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("Email không hợp lệ (phải chứa ký tự '@').", nameof(email));

        return new Member(name.Trim(), email.Trim(), studentCode.Trim());
    }

    /// <summary>
    /// Kiểm tra thành viên có đang ở trạng thái hoạt động hay không.
    /// </summary>
    public bool IsActive() => Status == MemberStatus.Active;

    /// <summary>
    /// Hủy kích hoạt tài khoản thành viên.
    /// </summary>
    public void Deactivate()
    {
        Status = MemberStatus.Inactive;
    }

    /// <summary>
    /// Trả về chuỗi đại diện cho thành viên theo định dạng "{Name} ({StudentCode})".
    /// </summary>
    public override string ToString() => $"{Name} ({StudentCode})";
}