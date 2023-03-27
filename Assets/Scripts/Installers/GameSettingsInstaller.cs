using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstallerr", menuName = "Installers/GameSettingsInstallerr")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private Prefabs _prefabs;

    public override void InstallBindings()
    {
        Container.BindInstance(_prefabs);
    }
}