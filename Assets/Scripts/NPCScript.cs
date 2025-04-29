using UnityEngine;

public class NPCScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool Talking = false;

    public GameObject Yap;

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.gameObject.CompareTag("Player")) 
        {
           Yap.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Yap.SetActive(false);
        }
    }


}
