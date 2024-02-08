using UnityEngine;
using TMPro;

public class UITextPanel : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI m_text;
    
    public virtual void SetUp(string text)
    {
        m_text.text = text;
    }
}
