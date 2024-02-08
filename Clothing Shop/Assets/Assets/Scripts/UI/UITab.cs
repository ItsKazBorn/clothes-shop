using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UITab : UIButton
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnClick()
    {
        Debug.Log("Tab Clicked");
    }

    public void SetTabActive(bool active)
    {
        m_button.interactable = !active;
    }

    public class Pool : MonoMemoryPool<string, UITab>
    {
        protected override void Reinitialize(string text, UITab uiTab)
        {
            base.Reinitialize(text, uiTab);
            uiTab.SetUp(text);
        }
    }
}
