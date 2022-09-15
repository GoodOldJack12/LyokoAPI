using System;
using System.Collections.Generic;
using System.IO;
using LyokoAPI.API;
using LyokoAPI.Events;

namespace LyokoAPI.Plugin
{
    public class ConfigManager
    {
        public static bool ConfigPathAvailable => Info.HasConfigPath();
        public string PluginConfigDirectory { get; }
        private LyokoAPIPlugin _plugin;
        private List<PluginConfig> Configs = new List<PluginConfig>();
        protected internal ConfigManager(LyokoAPIPlugin plugin)
        {
            if (ConfigPathAvailable)
            {
                PluginConfigDirectory = Path.Combine(Info.GetConfigPath(), plugin.Name);
                if (Directory.Exists(PluginConfigDirectory))
                {
                    foreach (var file in Directory.GetFiles(PluginConfigDirectory, "*.yaml", SearchOption.TopDirectoryOnly))
                    {
                        Configs.Add(new PluginConfig(plugin,file));
                    } 
                }
                else
                {
                    CreateConfigDirectory();
                }
                
            }

            _plugin = plugin;
        }

        public PluginConfig CreateConfig(string name)
        {
            Configs.Add(new PluginConfig(_plugin, Path.Combine(PluginConfigDirectory, name + ".yaml")));
            return GetConfig(name);
        }

        
        
        
        
        
        
        private bool CreateConfigDirectory()
        {
            if (ConfigPathAvailable)
            {
                Directory.CreateDirectory(PluginConfigDirectory);
            }
            else
            {
                LyokoLogger.Log(_plugin.Name,"No config directory specified.");
                return false;
            }

            if (Directory.Exists(PluginConfigDirectory))
            {
                return true;
            }
            else
            {
                LyokoLogger.Log(_plugin.Name,$"Couldn't create config directory at {PluginConfigDirectory}");
                return false;
            }
        }

        public PluginConfig GetMainConfig()
        {
            if (!ConfigPathAvailable)
            {
                return null;
            }
            PluginConfig config = GetConfig("main");
            /*if (config == null)
            {
                CreateConfigDirectory();
                CreateConfig("main");
            }*/

            return config;
        }

        public PluginConfig GetConfig(string name)
        {
            if (!ConfigPathAvailable)
            {
                return null;
            }

            PluginConfig pluginConfig = Configs.Find(config => config.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (pluginConfig == null)
                pluginConfig=CreateConfig(name);
            return pluginConfig;
        }

        public void SaveAllConfigs()
        {
            if (!ConfigPathAvailable)
            {
                return;
            }

            CreateConfigDirectory();
            Configs.ForEach(config => config.Write());
        }
    }
}