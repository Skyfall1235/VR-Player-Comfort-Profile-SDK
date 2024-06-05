using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public ProfileManager manager;

    private void OnGUI()
    {
        if(GUILayout.Button("Create profile"))
        {
            manager.CreateProfile("Test1");
        }
    }
}
