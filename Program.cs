using System;
namespace Battleship.Cli
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            try
            {
                Settings.LoadSettingsJson(@"Config/Settings.json");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Error: ", ex);
                Settings.ListAddShips();
                Settings.SaveSettingsJson();
            }
            Menus.StartGameMenu();
        }

    }
}
