# Ch01 — Phân tích yêu cầu và Domain Modeling EduHub

Bài tập Chương 1 môn **IN4211 - Phát triển phần mềm hướng đối tượng** (EduHub Project).
**Sinh viên thực hiện:** Đỗ Nguyễn Mai Trang - MSSV: 002441730

---

## Nội dung đã hoàn thành

1. **`docs/user-stories.md`**: Đã hoàn thành 6 User Stories cho 3 nhóm vai trò (Sinh viên, Ban CLB, Admin) kèm tiêu chí nghiệm thu dạng Given/When/Then.
2. **`docs/class-diagram.md`**: Thiết kế Sơ đồ lớp (Class Diagram) bằng cú pháp Mermaid mô tả 5 Domain Entities, các thuộc tính, phương thức, khóa chính/khóa ngoại và bội số quan hệ.
3. **`Ch01.Console/Domain/`**: Cài đặt 5 C# Domain Classes (`Member`, `Club`, `Event`, `Registration`, `CheckIn`) áp dụng đầy đủ tính Encapsulation (`private set`), Static Factory Method `Create(...)`, Private Constructor, XML Doc Comments và các phương thức xử lý nghiệp vụ.
4. **`Ch01.Console/Program.cs`**: Xây dựng Console App Demo chạy luồng nghiệp vụ 7 bước chuẩn: Tạo CLB $\rightarrow$ Thêm thành viên $\rightarrow$ Tạo sự kiện $\rightarrow$ Đăng ký & Xác nhận $\rightarrow$ Check-in QR $\rightarrow$ In Báo cáo tham dự $\rightarrow$ Xử lý ngoại lệ nghiệp vụ (`InvalidOperationException`).

---

## Hướng dẫn Build và Chạy dự án

Yêu cầu môi trường: **.NET 10 SDK** trở lên.

Mở Terminal tại thư mục gốc dự án và chạy các lệnh sau:

```bash
# Build dự án
dotnet build starter/starter/Ch01.Console.sln

# Chạy chương trình Console Demo
dotnet run --project starter/starter/Ch01.Console
```
