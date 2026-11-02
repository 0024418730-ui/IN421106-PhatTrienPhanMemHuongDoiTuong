namespace EduHub.Ch01.Domain;

/// <summary>
/// Câu lạc bộ sinh viên.
/// </summary>
public class Club
{
    private readonly List<Member> _members = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Danh sách thành viên thuộc CLB (chỉ đọc, không cho phép .Add() trực tiếp).
    /// </summary>
    public IReadOnlyList<Member> Members => _members.AsReadOnly();

    private Club(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Tạo mới một Câu lạc bộ.
    /// </summary>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Tên câu lạc bộ không được để trống.", nameof(name));

        return new Club(name.Trim(), description?.Trim() ?? string.Empty);
    }

    /// <summary>
    /// Thêm thành viên vào câu lạc bộ (chặn null và thành viên trùng).
    /// </summary>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (_members.Any(m => m.Id == member.Id))
            throw new InvalidOperationException("Thành viên này đã có trong câu lạc bộ.");

        _members.Add(member);
    }

    /// <summary>
    /// Trả về số lượng thành viên hiện tại của câu lạc bộ.
    /// </summary>
    public int MemberCount() => _members.Count;
}