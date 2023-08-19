using System.IO;
using System.Net;
using System.Text.Json;
using System.Diagnostics;

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
        if (Debugger.IsAttached)
        {
            logc("Debug mode enabled. Press any key to continue.");
            Console.ReadKey();
            logc("\n");
            Debug();
        }
        logc("Roblox ClientAppSettings.json setup by | termizzle (@terminite)");
        var client = new HttpClient();
        // issue 1 fix:::::
        var response = client.GetAsync("https://clientsettingscdn.roblox.com/v1/client-version/WindowsPlayer").Result;
        var content = response.Content.ReadAsStringAsync().Result;
        var jsons = JsonDocument.Parse(content);
        content = jsons.RootElement.GetProperty("clientVersionUpload").GetString();
        logc("Current Roblox version: "+content+"\n");
        
        if (Directory.Exists(@"C:\Program Files (x86)\Roblox\"))
        {
            logc("Found Roblox in Program Files (x86). Please reinstall Roblox without Administrator permissions.\nOr just delete the folder if you're not using that one.");
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
                logc("Created ClientSettings folder");
            }

            var clientObj = new
            {
                FFlagDebugGraphicsPreferD3D11 = "True",
                FFlagFixGraphicsQuality = "True",
                FFlagHandleAltEnterFullscreenManually = "False"
            };
            string jsonString = JsonSerializer.Serialize(clientObj);
            logc("Would you like to use the default ClientAppSettings.json or the RCO one?");
            logc("1. Default");
            logc("2. RCO");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                logc("Using default ClientAppSettings.json");
                File.WriteAllText(roblox_dir + @"\ClientSettings\ClientAppSettings.json", jsonString);
            }
            else if (choice == "2")
            {
                logc("Using RCO ClientAppSettings.json");
                logc("Downloading ClientAppSettings.json");
                using (WebClient client2 = new WebClient())
                {
                    client2.DownloadFile("https://raw.githubusercontent.com/L8X/Roblox-Client-Optimizer/main/ClientAppSettings.json", roblox_dir + @"\ClientSettings\ClientAppSettings.json");
                }
            }
            else
            {
                logc("Invalid choice. You should blow up. By the way, press any key...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        logc("\nAll done!!!!! Press any key to exit.");
        Console.ReadKey();
        Environment.Exit(0);
    }
    public static void Debug()
    {
        logc("[DEBUG] Roblox ClientAppSettings.json setup by | termizzle (@terminite)");
        var client = new HttpClient();
        var response = client.GetAsync("https://clientsettingscdn.roblox.com/v1/client-version/WindowsPlayer").Result;
        logc("Sending GET to https://clientsettingscdn.roblox.com/v1/client-version/WindowsPlayer");
        var content = response.Content.ReadAsStringAsync().Result;
        logc("Response: " + content);
        var jsons = JsonDocument.Parse(content);
        logc("Parsing JSON");
        content = jsons.RootElement.GetProperty("clientVersionUpload").GetString();
        logc("Extracted Version");
        logc("Current Roblox version: " + content + "\n");

        if (Directory.Exists(@"C:\Program Files (x86)\Roblox\"))
        {
            logc("Found Roblox in Program Files (x86). Please reinstall Roblox without Administrator permissions. Or just delete the folder if you're not using that one.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        if (Directory.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Roblox\"))
        {
            string roblox_dir = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Roblox\Versions\" + content;
            logc("Found Roblox Version || " + roblox_dir);

            if (!Directory.Exists(roblox_dir + @"\ClientSettings"))
            {
                Directory.CreateDirectory(roblox_dir + @"\ClientSettings");
                logc("Created ClientSettings folder");
            }

            var clientObj = new
            {
                FFlagDebugGraphicsPreferD3D11 = "True",
                FFlagFixGraphicsQuality = "True",
                FFlagHandleAltEnterFullscreenManually = "False"
            };
            string jsonString = JsonSerializer.Serialize(clientObj);
            logc("Would you like to use the default ClientAppSettings.json or the RCO one?");
            logc("1. Default");
            logc("2. RCO");
            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                logc("Using default ClientAppSettings.json");
                File.WriteAllText(roblox_dir + @"\ClientSettings\ClientAppSettings.json", jsonString);
            }
            else if (choice == "2")
            {
                logc("Using RCO ClientAppSettings.json");
                logc("Downloading ClientAppSettings.json");
                using (WebClient client2 = new WebClient())
                {
                    client2.DownloadFile("https://raw.githubusercontent.com/L8X/Roblox-Client-Optimizer/main/ClientAppSettings.json", roblox_dir + @"\ClientSettings\ClientAppSettings.json");
                }
            }
            else
            {
                logc("Invalid choice. You should blow up. By the way, press any key...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        logc("\nAll done!!!!! Press any key to exit.");
        Console.ReadKey();
        Environment.Exit(0);
    }
}