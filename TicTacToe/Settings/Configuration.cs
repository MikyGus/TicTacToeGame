using TicTacToe.Models;

namespace TicTacToe.Settings;

internal class Configuration
{
    private ConfigData _configData;
    public Configuration()
    {
        _configData = new ConfigData();
    }
    public void Configure(Action<ConfigData> config) => config(_configData);

    public ConfigData Data() => _configData.Duplicate();
    public GridData GridData() => _configData.Grid.Duplicate();

}
