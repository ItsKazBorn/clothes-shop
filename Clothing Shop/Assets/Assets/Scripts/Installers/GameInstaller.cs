using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Camera m_mainCamera;
    
    [Inject] private readonly Settings m_settings;
    
    public override void InstallBindings()
    {
        // Install Interfaces & Instances here
        Container.BindInterfacesAndSelfTo<SpriteSheetManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerInventory>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerWallet>().AsSingle();

        Container.BindInstance(m_mainCamera).AsSingle();
        
        InstallPools();
        InstallSignals();
    }

    private void InstallPools()
    {
        // Bind Memory Pools here
        
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        
        // Declare Signals Here
        Container.DeclareSignal<OnGameStartedSignal>();
        
        Container.DeclareSignal<OnAddCurrencySignal>();
        Container.DeclareSignal<OnCurrencyAmountChangedSignal>();
        
        Container.DeclareSignal<OnClothingShopOpenedSignal>();
        Container.DeclareSignal<OnClothingShopClosedSignal>();
        
        Container.DeclareSignal<OnGameItemPurchasedSignal>();
        Container.DeclareSignal<OnGameItemSoldSignal>();
        Container.DeclareSignal<OnGameItemEquipedSignal>();
        Container.DeclareSignal<OnShopItemSelectedSignal>();
        
        
    }
    
    [Serializable]
    public class Settings
    {
        // Prefabs Here
        public GameObject CharacterPrefab;
        public GameObject PlayerPrefab;
        public GameObject ShopkeeperPrefab;
    }

    
}