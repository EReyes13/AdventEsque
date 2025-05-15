using UnityEngine;

public class enemscript : MonoBehaviour
{

    
    // Update is called once per frame
    public AudioClip Deathsfx;
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(Deathsfx,1);
            Destroy(transform.parent.gameObject);

        }
    }
}
