using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

/// <summary>
/// Kho CLB lưu tạm trong bộ nhớ — dùng cho Chương 3 khi chưa có database.
/// Chương 4 sẽ thay bằng bản dùng EF Core và PostgreSQL.
/// </summary>
public class InMemoryClubRepository : IClubRepository
{
    // static: repository đăng ký Scoped (mỗi request một bản mới) nhưng dữ liệu phải sống suốt vòng đời ứng dụng.
    private static readonly List<Club> _clubs = new();

    /// <summary>
    /// TODO 1a: Trả về bản sao danh sách _clubs (dùng Task.FromResult)
    /// </summary>
    public Task<IEnumerable<Club>> GetAllAsync(CancellationToken ct = default)
    {
        IEnumerable<Club> copyList = _clubs.ToList();
        return Task.FromResult(copyList);
    }

    /// <summary>
    /// TODO 1b: Tìm club theo Id; không thấy thì trả null, KHÔNG throw
    /// </summary>
    public Task<Club?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var club = _clubs.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(club);
    }

    /// <summary>
    /// TODO 1c: Thêm club vào _clubs
    /// </summary>
    public Task AddAsync(Club club, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(club);
        _clubs.Add(club);
        return Task.CompletedTask;
    }

    /// <summary>
    /// TODO 1d: Tìm club cũ theo Id và thay bằng club mới; không thấy thì throw KeyNotFoundException
    /// </summary>
    public Task UpdateAsync(Club club, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(club);

        var index = _clubs.FindIndex(c => c.Id == club.Id);
        if (index == -1)
        {
            throw new KeyNotFoundException($"Không tìm thấy câu lạc bộ với Id: {club.Id}");
        }

        _clubs[index] = club;
        return Task.CompletedTask;
    }

    /// <summary>
    /// TODO 1e: Xoá club theo Id; không thấy thì throw KeyNotFoundException
    /// </summary>
    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var club = _clubs.FirstOrDefault(c => c.Id == id);
        if (club is null)
        {
            throw new KeyNotFoundException($"Không tìm thấy câu lạc bộ với Id: {id}");
        }

        _clubs.Remove(club);
        return Task.CompletedTask;
    }
}