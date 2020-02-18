using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleGun : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject obj;
    public float speed = 100f;

    public float range = 100f;
    // Update is called once per frame
    void Update() { 

        if(Input.GetButtonDown("Fire1")){
                Shoot();
        }

    }

    public void Shoot()
    {
        if (obj!=null)
        {
            RaycastHit hit;
            obj.transform.rotation = fpsCam.transform.rotation;

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Debug.Log(fpsCam.transform.rotation);
            }

            Rigidbody rg = obj.GetComponent<Rigidbody>();
            rg.AddForce(Vector3.forward * speed);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.ExplodeBubble();
            }
        }
    }

    public void getObject(GameObject gobj)
    {
        this.obj = gobj;
        Debug.Log(gobj.transform.name);
    }
}
