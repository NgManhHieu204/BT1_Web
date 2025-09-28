using System;
using System.Web;
using HieuCaro;

public partial class api : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.ContentType = "application/json";

        string p1 = Request.Form["player1"];
        string p2 = Request.Form["player2"];

        GameEngine game = new GameEngine();

        // Không có hàm StartGame, hãy dùng các hàm có sẵn của GameEngine
        game.InitializeBoard(15); // Khởi tạo bàn cờ với kích thước mong muốn
        string kq = game.ExportBoardString(); // Xuất trạng thái bàn cờ ra chuỗi

        // Escape chuỗi để an toàn khi trả về JSON
        string json = "{\"Result\":\"" + SafeJson(kq) + "\"}";
        Response.Write(json);
        Response.End();
    }

    private string SafeJson(string s)
    {
        if (s == null) return "";
        return s.Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\r", "")
                .Replace("\n", "");
    }
}