using System.IO;
using System.Net;

namespace ExclusiveFullscreen;

public static class Program
{
    [STAThread]
    public static void logc(string message)
    {
        Console.WriteLine(message);
    }
    static void Main()
    {
        logc("Roblox ClientAppSettings.json setup by | termizzle (@terminite)");

        var client = new HttpClient();
        var response = client.GetAsync("http://setup.roblox.com/version").Result;
        var content = response.Content.ReadAsStringAsync().Result;
        logc("Current Roblox version: "+content+"\n");

        if (Directory.Exists(@"C:\Program Files (x86)\Roblox\"))
        {
            logc("Found Roblox in Program Files (x86). Please reinstall Roblox without Administrator permissions.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        if (Directory.Exists(@"C:\Users\"+Environment.UserName+@"\AppData\Local\Roblox\"))
        {
            string roblox_dir = @"C:\Users\"+Environment.UserName+@"\AppData\Local\Roblox\Versions\" + content;
            logc("Found Roblox Version || " + roblox_dir);

            if (!Directory.Exists(roblox_dir + @"\ClientSettings"))
            {
                Directory.CreateDirectory(roblox_dir + @"\ClientSettings");
                logc("Created clientsettings folder");
            }

            using (var client2 = new WebClient())
            {
                client2.DownloadFile("https://cdn.discordapp.com/attachments/786964016546709514/1132097819969343558/ClientAppSettings.json", roblox_dir + @"\ClientSettings\ClientAppSettings.json");
                logc("Downloaded ClientAppSettings.json");
            }
        }
        logc("\nOk so, we done bruh. Press any key to exit.");
        Console.ReadKey();
        Environment.Exit(0);
    }
}