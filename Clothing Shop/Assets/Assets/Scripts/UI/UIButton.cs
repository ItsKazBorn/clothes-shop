using UnityEngine;
using Zenject;
using Button = UnityEngine.UI.Button;

public class UIButton : UITextPanel
{
    [SerializeField] protected Button m_button;

    public Button Button => m_button;
    
    void Start()
    {
        m_button.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        Debug.Log(m_text.text + " Button Clicked");
    }
    
    public class Pool : MonoMemoryPool<string, UIButton>
    {
        protected override void Reinitialize(string text, UIButton uiButton)
        {
            base.Reinitialize(text, uiButton);
            uiButton.SetUp(text);
        }
    }
    
    
}
