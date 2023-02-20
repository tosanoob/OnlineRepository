namespace QLSV
{
    internal class Note
    {
        /*
         * Bài tập:
Form1: Mainform:
Combobox: chọn lớp sinh hoạt
Search: tìm theo tên/lớp sinh hoạt (tìm kiếm tương đối theo substring)
Textbox
Datagridview => phải hiển thị theo combobox và lớp sinh hoạt
Button: Add Edit Delete Sort
Gọi xóa => phải xóa trong DataTable, sau đó phải reload lại Datatable

Gọi Add và Edit phải phân biệt:
	+ Edit: phải chọn 1 dòng và bấm edit => ứng dụng cơ sở dữ liệu, lấy thuộc tính khóa chính truyền sang Form Detail, dùng khóa chính check lại DataTable
	+ Add: khi add thì truyền khóa chính = null, đòi hỏi nhập khóa chính trong Detail Form
Sau khi OK đóng detail Form
=> Dò khóa chính với DataTable => nếu có trùng =>edit, nếu ko trùng => add

Tạo tầng trung gian: class SINHVIEN, để thao tác chính với frontend
class SINHVIEN đồng bộ ngược về DataTable

*Mỗi trình xử lí sự kiện chỉ có hai nhiệm vụ: nhận/hiện dữ liệu, và gọi tới các hàm xử lí dữ liệu

Form2: Detail Form
*DateTimePicker (lấy thuộc tính value, trả về dữ liệu DateTime)
MSSV, Name, Lớp sinh hoạt, Ngày sinh, điểm trung bình, Gender (radiobutton), Hồ sơ (CMT, CCCD)

Khai báo lớp lưu trữ dữ liệu (kiểu DataTable)
Design Pattern Singleton
         */

        /*
         Class SV:
        String LSH
        String MSSV (private key)
        String Name 
        DateTime NS
        Double DTB
        Bool Gender
        Bool Anh
        Bool Hocba
        Bool CCCD
        */

        /*
         * Kieu QLSV Singleton _table
         * Kieu SVList la tang trung gian _svlist
         */

        /*
        GUI layer:
        MainForm
        - Show data
        - Call Search (by Name & Class) 
        - Call Filter by Class
            => Load (substring, filterOption)
        - Call Add 
        - Call Edit
            => Launch DetailForm
        - Call Delete (DeleteRange)
            => Delete (string[] targetMSSV)
        - Call Sort (by Name, by DTB, by Class)
            => Sort(sortOption)

        DetailForm:
        - Call OK/Cancel
            => Update(SV)
         */

        /*
         Business layer:
        - Receive call Search,Filter => Load => return a sublist of suitable
        - Receive submission from DetailForm => check to Add or Edit => reload & sync to _table
        - Receive call Delete => remove from _svlist => reload & sync to _table
        - Receive call Sort => sort on _svlist only => reload
        - Receive data from Data layer
         */

        /*
         Data layer
        - Receive sync (new, update, delete)
        - Provide data to Business layer
         */
    }
}
