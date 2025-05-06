using UnityEngine;

public class ks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public static bool Activate;
    // Update is called once per frame
    void Update()
    {
        if(Activate)
        {
                Destroy(gameObject);
                Activate = false;
        }
    }
}
