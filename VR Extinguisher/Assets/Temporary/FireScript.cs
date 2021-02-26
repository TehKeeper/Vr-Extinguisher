using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    // Start is called before the first frame update
    float endTime = 0;
    [Range(1, 10)]
    public float lifeTime = 3;
    public bool extTrig = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (extTrig && Time.time > endTime)
        {
            destroyed();
        }
    }

    public void extinguish(bool value)
    {
        if (extTrig==value)
            return;

        extTrig = value;

        print("Fire Extinguished: " + value);


        if(extTrig)
            endTime = Time.time+lifeTime;



    }

    private void destroyed()
    {
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<ParticleSystem>().Stop();
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExtStream")
            extinguish(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ExtStream")
            extinguish(false);
    }
}
