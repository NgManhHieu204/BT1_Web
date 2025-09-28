# BÀI TẬP 1 PHÁT TRIỂN ỨNG DỤNG TRÊN NỀN WEB
# NGUYỄN MẠNH HIẾU - K225480106020
## Tạo SOLUTION gồm các Project sau:
1. DLL đa năng, keyword: c# window library -> Class Library (.NET Framework) bắt buộc sử dụng .NET Framework 2.0: giải bài toán bất kỳ, độc lạ càng tốt, phải có dấu ấn cá nhân trong kết quả, biên dịch ra DLL. DLL độc lập vì nó ko nhập, ko xuất, nó nhận input truyền vào thuộc tính của nó, và trả về dữ liệu thông qua thuộc tính khác, hoặc thông qua giá trị trả về của hàm. Nó độc lập thì sẽ sử dụng được trên app dạng console (giao diện dòng lệnh - đen sì), cũng sử dụng được trên app desktop (dạng cửa sổ), và cũng sử dụng được trên web form (web chạy qua iis).
2. Console app, bắt buộc sử dụng .NET Framework 2.0, sử dụng được DLL trên: nhập được input, gọi DLL, hiển thị kết quả, phải có dấu án cá nhân. keyword: c# window Console => Console App (.NET Framework), biên dịch ra EXE
3. Windows Form Application, bắt buộc sử dụng .NET Framework 2.0**, sử dụng được DLL đa năng trên, kéo các control vào để có thể lấy đc input, gọi DLL truyền input để lấy đc kq, hiển thị kq ra window form, phải có dấu án cá nhân; keyword: c# window Desktop => Windows Form Application (.NET Framework), biên dịch ra EXE
4. Web đơn giản, bắt buộc sử dụng .NET Framework 2.0, sử dụng web server là IIS, dùng file hosts để tự tạo domain, gắn domain này vào iis, file index.html có sử dụng html css js để xây dựng giao diện nhập được các input cho bài toán, dùng mã js để tiền xử lý dữ liệu, js để gửi lên backend. backend là api.aspx, trong code của api.aspx.cs thì lấy được các input mà js gửi lên, rồi sử dụng được DLL đa năng trên. kết quả gửi lại json cho client, js phía client sẽ nhận được json này hậu xử lý để thay đổi giao diện theo dữ liệu nhận dược, phải có dấu án cá nhân. keyword: c# window web => ASP.NET Web Application (.NET Framework) + tham khảo link chatgpt thầy gửi. project web này biên dịch ra DLL, phải kết hợp với IIS mới chạy được.
_____
## BÀI TOÁN ĐẶT RA: XÂY DỰNG GAME CỜ CARO

### Do sử dụng Visual Studio 2022 (không có sẵn .Net Framework 2.0) nên phải cài đặt

- B1: Vào Control Panel --> Chọn Programs and Features --> Chọn Turn Windows features on or off

- B2: Chọn mục .NET Framework 3.5 (bao gồm .NET 2.0 and 3.0)

<img width="520" height="43" alt="image" src="https://github.com/user-attachments/assets/251ae485-631a-4c60-825f-2135f6e7d2cd" />

- Sau khi cài xong vào Visual Studio 2022 để tạo project

1. Tạo project DLL với loại Class Library (.Net Framework) ==> Next

   <img width="691" height="687" alt="image" src="https://github.com/user-attachments/assets/4ab2795a-6b19-4c0b-b5fb-558527dfcd81" />

   - Tại cửa sổ tiếp theo đặt tên Project, Solution Name và lựa chọn Location và phiên bản Framework ==> Create

   <img width="1106" height="845" alt="image" src="https://github.com/user-attachments/assets/55fb451b-45db-4419-a6dc-814b99532efe" />

   - Sau khi tạo xong DLL sẽ xuất hiện cửa sổ class1.cs

   <img width="1025" height="500" alt="image" src="https://github.com/user-attachments/assets/a6e420d6-2b51-4874-9257-0f12c0d28df5" />

   - Tại đây thêm code vào và build

   <img width="1891" height="883" alt="image" src="https://github.com/user-attachments/assets/cfa69bba-14b1-48ab-a03f-49dda9f381bd" />

   - Sau khi build sẽ tạo ra 1 file đuôi là ".dll"
  
   <img width="809" height="214" alt="image" src="https://github.com/user-attachments/assets/5d015c08-5969-4015-ba71-5cc0cafdaf2e" />

2. Tạo Project Console.app (Console App (.NET Framework)) 

   <img width="1919" height="903" alt="image" src="https://github.com/user-attachments/assets/34ffa5c8-1376-4c28-a245-39293a5cbad6" />

    - Ở của sổ này đặt tên Project

   <img width="1107" height="847" alt="image" src="https://github.com/user-attachments/assets/e878424b-8a55-437a-84db-435ecfa13731" />

    - Chuột phải vào console_Caro ở cột bên phải -> add -> reference -> tìm đến HieuCaro.dll và add nó

    <img width="1918" height="964" alt="image" src="https://github.com/user-attachments/assets/c84ace5d-c3c8-4246-aee3-a067ba12ad14" />

    <img width="958" height="627" alt="image" src="https://github.com/user-attachments/assets/ffc57afe-f596-458e-9c64-196a2279dd6f" />

    <img width="986" height="684" alt="image" src="https://github.com/user-attachments/assets/513b4886-6c25-4ca6-8033-67c962d4ca78" />

    - Kết quả chạy program.cs

    <img width="902" height="741" alt="image" src="https://github.com/user-attachments/assets/89817866-7c01-461f-a5ee-d703161fe2e1" />

3. Tạo Project Windows Form Application (Windows Forms App (.NET Framework) giống tạo console

    - Add -> reference

   <img width="1919" height="955" alt="image" src="https://github.com/user-attachments/assets/805f76b1-ebee-4e66-a904-d42273acada9" />

    - Thêm Code vào Form1.cs và build

     <img width="766" height="879" alt="image" src="https://github.com/user-attachments/assets/9271ffa7-e286-4f72-832a-bf853867bcf7" />
 
    - Thêm Code vào Form1.Designer.cs và build
  
     <img width="999" height="555" alt="image" src="https://github.com/user-attachments/assets/cb676120-baeb-4cc3-8e0b-57e03b882be6" />

    - Thêm Code vào Program.cs và build
      
    <img width="969" height="388" alt="image" src="https://github.com/user-attachments/assets/e8c21d18-8b5c-4285-acf6-b96195c2f78d" />

    - Kết quả khi chạy:

    <img width="998" height="789" alt="image" src="https://github.com/user-attachments/assets/87d92092-3903-4b7f-afd3-9108a159ac88" />

4. Web đơn giản, sử dụng web server IIS

- Tạo backend api.aspx: Tạo project loại asp.net web application (.net framework)

<img width="1103" height="848" alt="image" src="https://github.com/user-attachments/assets/5f8bd0dc-40b7-4347-be11-0f22d3c1c33d" />

- Add reference

   - Tạo backend: Chuột phải vào WebApp -> Add -> New item
 
    <img width="871" height="1072" alt="image" src="https://github.com/user-attachments/assets/e156a524-6b5b-4de1-9753-596be871547a" />

   - Đặt tên là api.aspx --> sau khi tạo xong tiến hành thêm code ở api.aspx.cs --> build

    <img width="1448" height="619" alt="image" src="https://github.com/user-attachments/assets/26218034-6291-4668-b4fa-1101fe525ae2" />

   - Tạo file index.html: Chuột phải vào WebApp --> Add --> HTML Page
 
    <img width="842" height="1071" alt="image" src="https://github.com/user-attachments/assets/65ff3826-ed9c-40f7-b585-f1dba292540b" />

   - Thêm code và build:
 
    <img width="623" height="355" alt="image" src="https://github.com/user-attachments/assets/e7b0cc20-c114-4fc1-905d-04d44c11973c" />

5. TẠO DOMAIN + IIS

- Publish Web từ Visua studio

<img width="433" height="921" alt="image" src="https://github.com/user-attachments/assets/72e4ce1e-e508-476d-a8fc-87c326ecac51" />

Chọn Folder

<img width="1004" height="703" alt="image" src="https://github.com/user-attachments/assets/f3916e25-3bbd-46e7-8964-c0802c9eb5d4" />

Tạo 1 folder bên ngoài và trỏ đến nó trong visual -> sau đó tiến hành publish

<img width="1148" height="345" alt="image" src="https://github.com/user-attachments/assets/3c34d002-b7e1-4625-9ec0-8e57464d19a0" />

- Cấu hình IIS, Domain, Localhost

Cài đặt IIS: Vào Control Painel -> Programs -> Turn Windows features on or off -> Tích chọn Internet Information Services (IIS) và cài đặt

<img width="1421" height="755" alt="image" src="https://github.com/user-attachments/assets/8a5cddef-7aef-4f6f-ae88-1d7d72af055a" />

Tạo Website trên IIS: Mở IIS Manager (inetmgr) -> Chuột phải vào Sites -> chọn Add Website...

<img width="1421" height="755" alt="image" src="https://github.com/user-attachments/assets/65519aa5-4d50-4714-b466-a15b965cb240" />

Điền thông tin ở cửa sổ add website

<img width="718" height="825" alt="image" src="https://github.com/user-attachments/assets/1327cbaa-4b07-49b0-b898-0098b80b0c57" />
