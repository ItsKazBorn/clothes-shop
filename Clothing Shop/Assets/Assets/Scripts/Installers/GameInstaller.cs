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
        Container.BindInterfacesAndSelfTo<GameItemManager>().AsSingle();

        Container.BindInstance(m_mainCamera).AsSingle();
        
        InstallPools();
        InstallSignals();
    }

    private void InstallPools()
    {
        // Bind Memory Pools here
        Container.BindMemoryPool<UIShopItem, UIShopItem.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(m_settings.UIShopItemPrefab)
            .WithGameObjectName("UIShopItem");
        
        Container.BindMemoryPool<UIButton, UIButton.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(m_settings.UIButtonPrefab)
            .WithGameObjectName("UIButton");
        
        Container.BindMemoryPool<UITab, UITab.Pool>()
            .WithInitialSize(10)
            .FromComponentInNewPrefab(m_settings.UITabPrefab)
            .WithGameObjectName("UITab");
        
        
        
        
    }

    private void InstallSignals()
    {
        SignalBusInstaller.Install(Container);
        
        // Declare Signals Here
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
        // Pooled Prefabs Here
        public GameObject UIShopItemPrefab;
        public GameObject UIButtonPrefab;
        public GameObject UITabPrefab;
        public GameObject ShopkeeperPrefab;
        
        
    }

    
}