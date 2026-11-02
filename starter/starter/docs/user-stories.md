# User Stories – EduHub

Viết ít nhất 5 User Story, đủ 3 vai trò (Sinh viên, Ban CLB, Admin).
Mỗi story có ít nhất 2 tiêu chí nghiệm thu (Given/When/Then hoặc gạch đầu dòng).

## US-01 · Sinh viên: Hủy phiếu đăng ký tham gia sự kiện

Là Sinh viên, tôi muốn hủy phiếu đăng ký sự kiện đã tạo, để nhường chỗ trống cho các sinh viên khác nếu tôi không thể tham dự.

- Tiêu chí 1: Given phiếu đăng ký ở trạng thái Pending hoặc Confirmed When sinh viên thực hiện hủy phiếu Then hệ thống cập nhật trạng thái thành Cancelled và hoàn lại 1 chỗ trống cho sự kiện.
- Tiêu chí 2: Given phiếu đăng ký đã ở trạng thái Cancelled When sinh viên cố gắng thực hiện hủy lần nữa Then hệ thống từ chối thao tác và báo lỗi.

## US-02 · Sinh viên: Đăng ký sự kiện trực tuyến (Online)

Là Sinh viên, tôi muốn đăng ký tham gia các sự kiện Online của CLB, để đảm bảo giữ được chỗ trước khi sự kiện đạt giới hạn người tham gia.

- Tiêu chí 1: Given sự kiện Online còn chỗ trống (số lượt đăng ký chưa đạt Capacity) When sinh viên thực hiện đăng ký Then hệ thống tạo phiếu Registration mới ở trạng thái Pending.
- Tiêu chí 2: Given sự kiện Online đã hết chỗ (số lượt đăng ký đã đạt số lượng Capacity tối đa) When sinh viên thực hiện đăng ký Then hệ thống từ chối và thông báo lỗi "Sự kiện đã hết chỗ".

## US-03 · Ban CLB: Duyệt phiếu đăng ký của sinh viên

Là Ban CLB, tôi muốn duyệt các phiếu đăng ký tham gia sự kiện của sinh viên, để xác nhận chính thức danh sách sinh viên được tham dự sự kiện.

- Tiêu chí 1: Given phiếu đăng ký đang ở trạng thái chờ xác nhận (Pending) When Ban CLB thực hiện bấm duyệt (gọi phương thức Confirm) Then trạng thái phiếu đăng ký chuyển thành Confirmed.
- Tiêu chí 2: Given phiếu đăng ký đã bị sinh viên thực hiện hủy (Cancelled) When Ban CLB thực hiện bấm duyệt Then hệ thống từ chối xử lý và báo lỗi.

## US-04 · Ban CLB: Cập nhật lại sức chứa của sự kiện

Là Ban CLB, tôi muốn điều chỉnh lại sức chứa (Capacity) của sự kiện, để phù hợp với quy mô địa điểm tổ chức thực tế.

- Tiêu chí 1: Given sức chứa Capacity mới lớn hơn hoặc bằng số lượng phiếu đăng ký đã Confirmed When Ban CLB nhấn lưu thay đổi Then hệ thống cập nhật Capacity mới thành công.
- Tiêu chí 2: Given sức chứa Capacity mới nhỏ hơn số lượng phiếu đăng ký đã Confirmed When Ban CLB nhấn lưu thay đổi Then hệ thống từ chối lưu và báo lỗi "Sức chứa mới không được nhỏ hơn số lượng phiếu đã duyệt".

## US-05 · Admin: Kích hoạt lại tài khoản thành viên

Là Admin, tôi muốn kích hoạt lại tài khoản cho các thành viên, để khôi phục quyền truy cập hệ thống cho sinh viên.

- Tiêu chí 1: Given tài khoản thành viên đang bị ngừng hoạt động (IsActive = false) When Admin chọn kích hoạt lại Then trạng thái tài khoản chuyển thành hoạt động (IsActive = true).
- Tiêu chí 2: Given tài khoản thành viên đã ở trạng thái hoạt động sẵn (IsActive = true) When Admin chọn kích hoạt lại Then hệ thống hiển thị thông báo "Tài khoản này đã ở trạng thái hoạt động".

## US-06 · Admin: Kiểm tra tính hợp lệ khi tạo thành viên

Là Admin, tôi muốn hệ thống tự động kiểm tra dữ liệu đầu vào khi tạo thành viên mới, để đảm bảo danh sách sinh viên trên hệ thống luôn chuẩn xác.

- Tiêu chí 1: Given thông tin Tên và MSSV không trống, đồng thời Email chứa ký tự '@' When Admin tiến hành tạo thành viên Then hệ thống tạo tài khoản thành công ở trạng thái Active.
- Tiêu chí 2: Given địa chỉ Email bị để trống hoặc thiếu ký tự '@' When Admin tiến hành tạo thành viên Then hệ thống ngăn chặn hành động và báo lỗi dữ liệu không hợp lệ.