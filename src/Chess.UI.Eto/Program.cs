using Eto;
using Eto.Forms;

namespace Chess.UI.Eto;
class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        try
        {
            var app = new Application(Platforms.Gtk);
            app.Run(new MainForm());
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eto startup failed");
            Console.WriteLine(ex.ToString());
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
        }
    }
}