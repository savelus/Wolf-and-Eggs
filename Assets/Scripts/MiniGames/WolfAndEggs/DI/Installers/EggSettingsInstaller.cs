using MiniGames.WolfAndEggs.Eggs;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EggSettingsInstaller", menuName = "Installers/EggSettingsInstaller")]
public class EggSettingsInstaller : ScriptableObjectInstaller<EggSettingsInstaller>
{
    public EggSetting EggSetting;
    public override void InstallBindings()
    {
        Container
            .BindInstance(EggSetting);
    }
}