using UnityEngine;

    
public class LineTriggerTest : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Test");
    }
}
