using EduHub.Ch01.Domain;

Console.OutputEncoding = System.Text.Encoding.UTF8;

const string Separator = "========================================";
const int EventCapacity = 50;

static void PrintStep(int number, string title)
{
    Console.WriteLine($"\n{Separator}\nBước {number}: {title}\n{Separator}");
}

// ============================================================================
// BƯỚC 1: TẠO CÂU LẠC BỘ
// ============================================================================
PrintStep(1, "Tạo câu lạc bộ");

var club = Club.Create("Tech Club", "Câu lạc bộ công nghệ: workshop, hackathon, chia sẻ kiến thức.");
Console.WriteLine($"CLB: {club.Name} — {club.Description}");

// ============================================================================
// BƯỚC 2: THÊM 3 THÀNH VIÊN VÀO CLB
// ============================================================================
PrintStep(2, "Thêm 3 thành viên vào CLB");

var members = new[]
{
    Member.Create("Nguyễn Văn An", "an.nv@student.dthu.edu.vn", "DH24001"),
    Member.Create("Đỗ Nguyễn Mai Trang", "trang.dnm@student.dthu.edu.vn", "DH24002"),
    Member.Create("Lê Hoàng Cường", "cuong.lh@student.dthu.edu.vn", "DH24003")
};

foreach (var member in members)
{
    club.AddMember(member);
    Console.WriteLine($"+ {member} <{member.Email}>");
}

Console.WriteLine($"Tổng thành viên trong CLB: {club.MemberCount()}");

// ============================================================================
// BƯỚC 3: TẠO SỰ KIỆN CHO CLB
// ============================================================================
PrintStep(3, "Tạo 1 sự kiện cho CLB");

var eventDate = DateTime.Today.AddDays(7).AddHours(14);
var workshop = Event.Create(club.Id, "Workshop AI & Clean Architecture", eventDate, EventCapacity, isOnline: true);

Console.WriteLine($"Sự kiện: {workshop.Title} — Ngày tổ chức: {workshop.Date:dd/MM/yyyy HH:mm} — Sức chứa: {workshop.Capacity} chỗ.");

// ============================================================================
// BƯỚC 4: 2 THÀNH VIÊN ĐĂNG KÝ THAM DỰ SỰ KIỆN (STATUS = PENDING) VÀ XÁC NHẬN
// ============================================================================
PrintStep(4, "Đăng ký tham dự sự kiện");

var registrations = new List<Registration>();

// Lấy 2 thành viên đầu tiên thực hiện đăng ký
foreach (var member in members.Take(2))
{
    if (!workshop.HasCapacity(registrations.Count))
    {
        Console.WriteLine($"! Sự kiện đã hết chỗ, không nhận đăng ký của {member}");
        continue;
    }

    var registration = Registration.Create(member.Id, workshop.Id);
    registrations.Add(registration);
    Console.WriteLine($"Sinh viên {member} đăng ký thành công → Trạng thái: {registration.Status}");
}

Console.WriteLine("\n--- Tiến hành xác nhận phiếu đăng ký (Confirm) ---");
foreach (var registration in registrations)
{
    registration.Confirm();
    Console.WriteLine($"Phiếu đăng ký [{registration.Id.ToString()[..8]}] → Trạng thái mới: {registration.Status}");
}

// ============================================================================
// BƯỚC 5: 1 THÀNH VIÊN THỰC HIỆN CHECK-IN
// ============================================================================
PrintStep(5, "1 thành viên thực hiện Check-in");

// Thành viên đầu tiên thực hiện quét mã QR để điểm danh
var checkIns = new List<CheckIn>
{
    CheckIn.Create(registrations[0], CheckInMethod.QrCode)
};

Console.WriteLine($"Thực hiện Check-in thành công: {checkIns[0].Summary()}");

// ============================================================================
// BƯỚC 6: IN BÁO CÁO THAM DỰ SỰ KIỆN
// ============================================================================
PrintStep(6, "In báo cáo tham dự sự kiện");

Console.WriteLine($"Sự kiện: {workshop.Title}");
Console.WriteLine($"Số lượt đăng ký: {registrations.Count}/{workshop.Capacity}");
Console.WriteLine($"Số lượt check-in: {checkIns.Count}");
Console.WriteLine("Danh sách điểm danh:");

var memberById = members.ToDictionary(m => m.Id);

foreach (var registration in registrations)
{
    var attended = checkIns.Any(c => c.RegistrationId == registration.Id);
    var studentName = memberById[registration.MemberId].Name;
    var statusText = attended ? "Đã check-in" : "Chưa check-in";

    Console.WriteLine($"  - {studentName}: {statusText}");
}

// ============================================================================
// BƯỚC 7: THỬ NGHIỆM VI PHẠM QUY TẮC NGHIỆP VỤ (THỬ CONFIRM PHIẾU ĐÃ HỦY)
// ============================================================================
PrintStep(7, "Thử nghiệm vi phạm luật nghiệp vụ");

Console.WriteLine("Tiến hành hủy phiếu đăng ký thứ 2 (Cancel)...");
registrations[1].Cancel();
Console.WriteLine($"Phiếu [{registrations[1].Id.ToString()[..8]}] hiện có trạng thái: {registrations[1].Status}");

Console.WriteLine("Thử gọi phương thức Confirm() trên phiếu đã hủy:");
try
{
    registrations[1].Confirm();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"\u274c [BẮT LỖI NGHIỆP VỤ THÀNH CÔNG]: {ex.Message}");
}

Console.WriteLine($"\n{Separator}\nHOÀN THÀNH CHƯƠNG TRÌNH DEMO\n{Separator}");