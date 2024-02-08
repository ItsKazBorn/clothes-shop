using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public GameInstaller.Settings GameInstaller;
    public PlayerWallet.Settings PlayerWallet;
    public SpriteSheetManager.Settings SpriteSheetManager;
    public GameItemManager.Settings GameItemManager;
    
    public override void InstallBindings()
    {
        // Bind Instances here
        Container.BindInstance(GameInstaller);
        Container.BindInstance(PlayerWallet);
        Container.BindInstance(GameItemManager);
        Container.BindInstance(SpriteSheetManager);
        
    }
}