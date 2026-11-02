using EduHub.Application.Clubs;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;
using Moq;
using Xunit;

namespace EduHub.Tests.Clubs;

public class GetClubByIdQueryTests
{
    /// <summary>
    /// TEST 5 (TỰ VIẾT): Kiểm thử GetClubByIdQueryHandler khi Id tồn tại.
    /// Dùng Moq giả lập IClubRepository trả về đối tượng Club đã chuẩn bị trước.
    /// </summary>
    [Fact]
    public async Task GetClubById_WhenIdExists_ReturnsCorrectClub()
    {
        // Arrange (Chuẩn bị dữ liệu và Mock repository)
        var club = Club.Create("Tech Club", "Câu lạc bộ công nghệ");
        var mockRepo = new Mock<IClubRepository>();

        // Dặn đồ giả (Setup): Khi gọi GetByIdAsync với club.Id thì trả về club vừa tạo
        mockRepo.Setup(r => r.GetByIdAsync(club.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(club);

        var handler = new GetClubByIdQueryHandler(mockRepo.Object);
        var query = new GetClubByIdQuery(club.Id);

        // Act (Thực thi)
        var result = await handler.HandleAsync(query);

        // Assert (Kiểm tra kết quả & Xác nhận đồ giả được gọi đúng 1 lần)
        Assert.NotNull(result);
        Assert.Equal(club.Id, result!.Id);
        Assert.Equal("Tech Club", result.Name);

        mockRepo.Verify(r => r.GetByIdAsync(club.Id, It.IsAny<CancellationToken>()), Times.Once);
    }
}