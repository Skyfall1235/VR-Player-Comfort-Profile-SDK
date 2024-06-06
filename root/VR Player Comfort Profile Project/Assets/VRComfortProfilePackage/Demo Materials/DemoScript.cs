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
        if (GUILayout.Button("Load Profile"))
        {
            Debug.Log("loading profile");
            manager.TryParseProfile(ProfileSetup.ProfileFolderPath + "/Test1.json");
        }
    }
}
