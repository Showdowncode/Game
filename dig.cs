using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class dig : MonoBehaviour
{
   public Camera myEyes;

    // Update is called once per frame
    [System.Obsolete]
    void Update() {
        if (Input.GetMouseButtonUp (0)) {
            AddCube ();
        }

         if (Input.GetMouseButtonUp (1)) {
            RemoveCube ();
        }

    }


    void AddCube(){

        Ray ray = myEyes.ScreenPointToRay 
            (new Vector3(Screen.width/2, Screen.height/2, 0f));
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 5f)) {
            Vector3 newPos = hit.transform.position;
            
            GameObject newB = Instantiate (hit.collider.gameObject);

            newB.transform.position = newPos;
            newB.transform.Translate (hit.normal * 1f);

            newB.gameObject.tag = "imadethis";

        }

    }

    [System.Obsolete]
    void RemoveCube(){

        Ray ray = myEyes.ScreenPointToRay 
            (new Vector3(Screen.width/2, Screen.height/2, 0f));
        RaycastHit hit;

        if (Physics.Raycast (ray, out hit, 5f)) {
            //Vector3 newPos = hit.transform.position;
            
            //GameObject newB = Instantiate(hit.collider.gameObject);

            //newB.transform.position = newPos;
            //newB.transform.Translate(hit.normal * 1f);

            if (hit.collider.gameObject.CompareTag ("imadethis")) {
                DestroyObject (hit.collider.gameObject);
            }
            else if (!hit.collider.gameObject.CompareTag ("imadethis")) {
                hit.transform.Translate (Vector3.down * 1f);
            }
            
        }


    }

}
