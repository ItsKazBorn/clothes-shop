using System;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    
    
    [Inject] private readonly Settings m_settings;
    
    public override void InstallBindings()
    {
        // Install Interfaces & Instances here
        Container.BindInterfacesAndSelfTo<CurrencyManager>().AsSingle();
        
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
        
        
    }
    
    [Serializable]
    public class Settings
    {
        // Prefabs Here
        public GameObject CharacterPrefab;
    }

    
}