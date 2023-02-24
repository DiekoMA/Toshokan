namespace Toshokan.Utils;

public sealed class AppSettingsHelper
{
    private SharpConfig.Configuration config;
    private AppSettingsHelper()
    {
        
    }    
    private static readonly Lazy<AppSettingsHelper> lazy = new Lazy<AppSettingsHelper>(() => new AppSettingsHelper());    
    public static AppSettingsHelper Instance    
    {    
        get    
        {    
            return lazy.Value;    
        }    
    }

    public void SaveStringItem(string section,string key, string value)
    {
        config[section][key].StringValue = value; 
        config.SaveToFile("appconfig.cfg");
    }
    
    public void SaveBoolItem(string section, string key, bool value)
    {
        config[section][key].BoolValue = value; 
        config.SaveToFile("appconfig.cfg");
    }

    public string GetStringItem(string section, string key)
    {
        config = SharpConfig.Configuration.LoadFromFile("appconfig.cfg");
        string result = config[section][key].StringValue;
        return result;
    }
    
    public bool GetBoolItem(string section, string key)
    {
        config = SharpConfig.Configuration.LoadFromFile("appconfig.cfg");
        bool result = config[section][key].BoolValue;
        return result;
    }
}    
