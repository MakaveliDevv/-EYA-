using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string Name;
    public Color color;

    void Start() 
    {
        gameObject.name = Name;
    }
 
    void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            if(!GameManager.GetInstance().colors.Contains(gameObject))
            {
                GameManager.GetInstance().colors.Add(gameObject);
            }
        }
    }
}
