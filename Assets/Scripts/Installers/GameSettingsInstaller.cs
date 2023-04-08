using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstallerr", menuName = "Installers/GameSettingsInstallerr")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    [SerializeField] private Prefabs _prefabs;
    [SerializeField] private GameSettings _gameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_prefabs);
        Container.BindInstance(_gameSettings);
    }
}
