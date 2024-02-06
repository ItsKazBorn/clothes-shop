
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private CharacterSpriteLayerController[] m_spriteLayerControllers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovementParameters(Vector2 movement, float speed)
    {
        foreach (CharacterSpriteLayerController spriteLayer in m_spriteLayerControllers)
        {
            spriteLayer.SetMovementParameters(movement, speed);
        }
    }
}
