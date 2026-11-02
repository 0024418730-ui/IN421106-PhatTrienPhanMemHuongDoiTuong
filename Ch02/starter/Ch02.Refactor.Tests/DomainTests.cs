using EduHub.Ch02.Domain;

namespace Ch02.Refactor.Tests;

// Mẫu AAA: Arrange (chuẩn bị) - Act (thực hiện) - Assert (kiểm tra).
// Mỗi test chỉ kiểm tra MỘT hành vi. Đặt tên: TênMethod_TìnhHuống_KếtQuảMongĐợi.
public class DomainTests
{
    // Test mẫu đã viết sẵn: kiểm tra ném ArgumentException khi tên rỗng.
    [Fact]
    public void CreateClub_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange & Act
        var act = () => Club.Create("", "Mô tả CLB");

        // Assert
        Assert.Throws<ArgumentException>(act);
    }

    [Fact]
    public void Club_Create_WithValidName_ReturnsClub()
    {
        // Arrange
        string validName = "Tech Club";
        string description = "CLB Công nghệ";

        // Act
        var club = Club.Create(validName, description);

        // Assert
        Assert.NotNull(club);
        Assert.Equal(validName, club.Name);
        Assert.Equal(description, club.Description);
        Assert.NotEqual(Guid.Empty, club.Id);
    }

    [Fact]
    public void Club_Create_WithInvalidName_ThrowsArgumentException()
    {
        // Arrange: Tạo tên dài hơn MaxNameLength (100 ký tự)
        string longName = new string('A', Club.MaxNameLength + 1);

        // Act
        var actWhitespace = () => Club.Create("   ", "Mô tả CLB");
        var actLongName = () => Club.Create(longName, "Mô tả CLB");

        // Assert
        Assert.Throws<ArgumentException>(actWhitespace);
        Assert.Throws<ArgumentException>(actLongName);
    }

    [Fact]
    public void Club_HasRoom_AtMaxMembers_ReturnsFalse()
    {
        // Arrange
        var club = Club.Create("Tech Club", "Mô tả");

        // Act
        bool hasRoomAt49 = club.HasRoom(49);
        bool hasRoomAt50 = club.HasRoom(50);

        // Assert
        Assert.True(hasRoomAt49, "Tại 49 thành viên thì HasRoom phải trả về true.");
        Assert.False(hasRoomAt50, "Tại 50 (hoặc lớn hơn MaxMembers) thành viên thì HasRoom phải trả về false.");
    }

    [Fact]
    public void Event_Register_NewMember_ReturnsTrue()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop AI" };
        var memberId = Guid.NewGuid();

        // Act
        bool result = ev.Register(memberId);

        // Assert
        Assert.True(result);
        Assert.Contains(memberId, ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_Register_SameMemberTwice_ReturnsFalse()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop AI" };
        var memberId = Guid.NewGuid();
        ev.Register(memberId); // Đăng ký lần 1

        // Act
        bool secondRegisterResult = ev.Register(memberId); // Đăng ký lần 2

        // Assert
        Assert.False(secondRegisterResult, "Đăng ký thành viên lần 2 phải trả về false.");
        Assert.Single(ev.RegisteredMemberIds);
    }

    [Fact]
    public void Event_CheckIn_UnregisteredMember_ThrowsInvalidOperationException()
    {
        // Arrange
        var ev = new Event { Id = Guid.NewGuid(), ClubId = Guid.NewGuid(), Title = "Workshop AI" };
        var unregisteredMemberId = Guid.NewGuid();

        // Act
        var act = () => ev.CheckIn(unregisteredMemberId);

        // Assert
        Assert.Throws<InvalidOperationException>(act);
        Assert.DoesNotContain(unregisteredMemberId, ev.CheckedInMemberIds);
    }
}