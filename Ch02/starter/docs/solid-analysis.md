# PHÂN TÍCH VI PHẠM 5 NGUYÊN LÝ SOLID TRONG CLUBMANAGER.CS

---

### 1. S — Single Responsibility Principle (SRP - Nguyên lý Đơn trách nhiệm)
- **Hiện trạng vi phạm:** Lớp `ClubManager` ôm quá nhiều trách nhiệm không liên quan: vừa quản lý dữ liệu CLB, vừa xử lý nghiệp vụ sự kiện, vừa trực tiếp ghi log ra file/console, vừa gửi thông báo (Email, SMS, Slack) và tự in báo cáo.
- **Hậu quả:** Khi có thay đổi về cơ chế ghi log, kênh thông báo hay định dạng báo cáo, ta buộc phải sửa đổi lớp `ClubManager`, nguy cơ phát sinh lỗi tiềm ẩn cao.
- **Hướng Refactor:** Tách `ClubManager` thành các Service nhỏ độc lập: `ClubService`, `EventService`, `ReportService`, `IAppLogger`, `INotificationService`.

---

### 2. O — Open/Closed Principle (OCP - Nguyên lý Đóng/Mở)
- **Hiện trạng vi phạm:** Phương thức gửi thông báo trong `ClubManager` sử dụng các chuỗi điều kiện `if/else` kiểm tra đuôi email (`@gmail.com`, `@yahoo.com`) và phương thức in báo cáo bị viết cứng định dạng xuất ra màn hình.
- **Hậu quả:** Khi muốn thêm một kênh thông báo mới (Slack/SMS) hoặc một định dạng báo cáo mới (CSV/PDF), ta bắt buộc phải chỉnh sửa trực tiếp mã nguồn cũ của `ClubManager`.
- **Hướng Refactor:** Áp dụng **Factory Pattern** cho `NotificationServiceFactory` và **Strategy Pattern** cho `IReportStrategy`.

---

### 3. L — Liskov Substitution Principle (LSP - Nguyên lý Thay thế Liskov)
- **Hiện trạng vi phạm:** Trong mã nguồn ban đầu, các phương thức khởi tạo và xử lý danh sách không đảm bảo tính nhất quán về hợp đồng (Contract). Việc ném ngoại lệ ngẫu nhiên (`InvalidOperationException`, `ArgumentException`) rải rác mà không có giao diện trừu tượng hóa khiến các lớp tiêu thụ không thể thay thế linh hoạt các lớp con/cài đặt mà không làm nảy sinh lỗi bất ngờ.
- **Hậu quả:** Các module phụ thuộc bị phá vỡ hành vi kỳ vọng khi hoán đổi giữa các thành phần lưu trữ hoặc thông báo khác nhau.
- **Hướng Refactor:** Đảm bảo tất cả các class cài đặt (`InMemoryClubRepository`, `LoggingClubRepository`, các `INotificationService`) tuân thủ nghiêm ngặt hợp đồng do Interface quy định, bảo đảm lớp con thay thế hoàn toàn lớp cha mà không làm sai lệch hành vi hệ thống.

---

### 4. I — Interface Segregation Principle (ISP - Nguyên lý Phân tách Interface)
- **Hiện trạng vi phạm:** `ClubManager` quản lý trực tiếp các danh sách `List<Club>` và `List<Member>` nội bộ mà không chia nhỏ qua bất kỳ Interface nào.
- **Hậu quả:** Mọi module khác muốn thao tác với dữ liệu đều bị ép buộc phụ thuộc vào toàn bộ cấu trúc và các phương thức phình to của `ClubManager` (Tight Coupling).
- **Hướng Refactor:** Chia nhỏ thành các Interface Repository chuyên biệt: `IClubRepository`, `IMemberRepository`, `IEventRepository`.

---

### 5. D — Dependency Inversion Principle (DIP - Nguyên lý Đảo ngược Phụ thuộc)
- **Hiện trạng vi phạm:** Lớp cấp cao `ClubManager` phụ thuộc trực tiếp vào các module I/O cấp thấp cứng của hệ thống như `File.AppendAllText` và `Console.WriteLine`.
- **Hậu quả:** Không thể viết **Unit Test** độc lập vì logic nghiệp vụ bị dính chặt vào tập tin đĩa và màn hình Console.
- **Hướng Refactor:** Tiêm phụ thuộc (Dependency Injection) qua Constructor Injection, các Service cấp cao chỉ giao tiếp thông qua Abstraction (`IAppLogger`, `IClubRepository`, `INotificationService`).