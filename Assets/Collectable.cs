using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string Name;
    public Color color;

    void Start() 
    {
        gameObject.name = Name;
    }
 
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!GameManager.GetInstance().colorOrbs.Contains(gameObject))
            {
                GameManager.GetInstance().colorOrbs.Add(gameObject);
                gameObject.SetActive(false);
            }
        }
    }
}
