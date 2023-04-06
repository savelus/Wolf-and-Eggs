using MiniGames.WolfAndEggs.Eggs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

[CreateAssetMenu(fileName = "EggSettingsInstaller", menuName = "Installers/EggSettingsInstaller")]
public class EggSettingsInstaller : ScriptableObjectInstaller<EggSettingsInstaller>
{
    public EggSetting eggSetting;

    public EggSpawnTimeSettings eggSpawnTimeSettings;
    public override void InstallBindings()
    {
        Container
            .BindInstance(eggSetting);

        Container
            .BindInstance(eggSpawnTimeSettings);
    }
}