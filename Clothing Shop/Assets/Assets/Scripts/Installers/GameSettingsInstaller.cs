using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInstaller.Settings GameInstaller;
    public SpriteSheetManager.Settings SpriteSheetManager;
    
    public override void InstallBindings()
    {
        // Bind Instances here
        Container.BindInstance(GameInstaller);
        Container.BindInstance(SpriteSheetManager);

    }
}