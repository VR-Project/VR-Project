using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menuprincipale : MonoBehaviour
{
    public GUIStyle stileBottoni;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 250, 500, 500));
        GUILayout.Button("Inizia Gioco", stileBottoni);
        GUILayout.Button("Opzioni", stileBottoni);
        GUILayout.Button("Esci", stileBottoni);
        GUILayout.EndArea();
    }
}
