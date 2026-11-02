namespace EduHub.Ch02.Domain;

/// <summary>Câu lạc bộ. Quy tắc nghiệp vụ nên nằm ở đây, không nằm trong ClubManager.</summary>
public class Club
{
    public const int MaxMembers = 50;
    public const int MaxNameLength = 100;

    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Description { get; set; }

    /// <summary>
    /// Chuyển luật kiểm tra tên từ ClubManager.CreateClub vào đây.
    /// Tên rỗng/toàn khoảng trắng -> ArgumentException; dài hơn MaxNameLength -> ArgumentException.
    /// </summary>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống hoặc toàn khoảng trắng.", nameof(name));

        if (name.Length > MaxNameLength)
            throw new ArgumentException($"Tên câu lạc bộ không được vượt quá {MaxNameLength} ký tự.", nameof(name));

        return new Club
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim() ?? string.Empty
        };
    }

    /// <summary>
    /// Trả về true khi số thành viên hiện tại còn nhỏ hơn MaxMembers (50).
    /// </summary>
    public bool HasRoom(int currentMemberCount)
    {
        return currentMemberCount < MaxMembers;
    }
}