using GdProject.Logger;
using GdProject.Root.Scripts;
using Godot;
using Newtonsoft.Json;
using System.IO;

public partial class ConfigManager : Node
{

    private readonly string ConfigFilePath = Path.Combine(OS.GetUserDataDir(), "config.json");

    public ConfigData ConfigData { get; private set; }

    public void SaveConfig()
    {
        var json = JsonConvert.SerializeObject(ConfigData);
        File.WriteAllText(ConfigFilePath, json);

        ExternalLogger.Print("Config saved");
    }

    public void LoadConfig()
    {
        // Imprimir o caminho do arquivo
        GD.Print("Config file path: " + ConfigFilePath);

        if (File.Exists(ConfigFilePath))
        {
            var json = File.ReadAllText(ConfigFilePath);
            ConfigData = JsonConvert.DeserializeObject<ConfigData>(json);

            ExternalLogger.Print($"Config loaded");
        }
        else
        {
            ConfigData = new ConfigData(); // Return default config if file doesn't exist
        }
    }
}
