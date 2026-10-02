Câu 1:
- Bản chất dữ liệu lưu trữ :
  + Value Types (Kiểu giá trị): Chứa trực tiếp dữ liệu thực tế. Khi được khai báo là biến cục bộ trong một phương thức, dữ   liệu của nó được lưu trữ trực tiếp trên vùng nhớ Stack.
  + Reference Types (Kiểu tham chiếu): Không chứa dữ liệu trực tiếp mà chứa địa chỉ bộ nhớ (con trỏ tham chiếu). Dữ liệu thực tế của đối tượng được lưu trữ trên vùng nhớ Heap, còn địa chỉ con trỏ trỏ tới dữ liệu đó được lưu trên Stack.
- Cơ chế gán biến :
  + Value types : Sao chép toàn bộ giá trị
    Vd : Khi gán b = a, hai biến độc lập hoàn toàn; việc thay đổi giá trị của b sẽ không ảnh hưởng đến a.
  + Reference Types: Thực hiện sao chép địa chỉ tham chiếu
    Vd : Khi gán b = a, cả hai biến cùng trỏ đến một vùng nhớ chung trên Heap; việc thay đổi dữ liệu thông qua b sẽ làm thay đổi dữ liệu mà a đang nhìn thấy.
  - Cơ chế khởi tạo và giải phóng bộ nhớ:
    + Value Types: Được khởi tạo và giải phóng bộ nhớ rất nhanh.
    + Reference Types: Việc cấp phát trên Heap tốn chi phí thời gian hơn.
  - Các kiểu dữ liệu đại diện:
    + Value Types: int, float, double, bool, char, struct, enum.
    + Reference Types: class, string, object, array, interface, delegate.
      
Câu 2 :
-  khác biệt so với set :
   + set: Cho phép gán/thay đổi giá trị của thuộc tính vào bất kỳ lúc nào trong suốt vòng đời của Object.
   + init: Chỉ cho phép gán giá trị duy nhất một lần trong quá trình khởi tạo đối tượng (qua Constructor hoặc Object Initializer { Name = "..." }). Sau khi đối tượng tạo xong, thuộc tính trở thành Read-Only (không thể sửa đổi).
- Trường hợp sử dụng thực tế :
  + Khởi tạo các đối tượng Bất biến (Immutable Objects) hoặc các lớp DTO (Data Transfer Object) nhận dữ liệu từ API/Database.
  + Giúp ghi mã nguồn sạch gọn hơn bằng Object Initializer mà vẫn đảm bảo tính an toàn dữ liệu, tránh việc vô tình sửa đổi giá trị sau khi tạo đối tượng.
    
Câu 3 :
- Phương thức virtual (Lớp cha):
  + Khai báo phương thức có hành vi mặc định ở lớp cha.
  + Cho phép các lớp con có quyền ghi đè (thay thế) hành vi này nếu cần.
  + Nếu lớp con không ghi đè, phương thức virtual của lớp cha vẫn sẽ được thực thi.
- Phương thức override (Lớp con):
  + Khai báo ở lớp con để thay thế hoàn toàn triển khai của phương thức virtual từ lớp cha.
  + Thực thi tính Đa hình: Khi gọi phương thức qua con trỏ lớp cha trỏ tới đối tượng lớp con, chương trình sẽ ưu tiên chạy phiên bản override ở lớp con.

Câu 4 :
- Thành phần static thuộc về bản thân Lớp (Class), được nạp vào bộ nhớ một lần duy nhất và chia sẻ chung cho toàn bộ chương trình.
- Thể hiện (Object Instance) tạo bằng new thuộc về một đối tượng cụ thể với dữ liệu riêng biệt nằm trên bộ nhớ Heap.
- C# không cho phép truy xuất static qua Instance nhằm tránh gây hiểu nhầm rằng thuộc tính/phương thức đó thuộc về dữ liệu riêng của đối tượng đó.
- Giúp trình biên dịch tối ưu hóa việc gọi trực tiếp qua tên Lớp (ClassName.StaticMember) mà không cần thông qua con trỏ tham chiếu của đối tượng.

  
