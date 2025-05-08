using UnityEngine;

public class enemscript : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {

            Destroy(transform.parent.gameObject);
        }
    }
}
