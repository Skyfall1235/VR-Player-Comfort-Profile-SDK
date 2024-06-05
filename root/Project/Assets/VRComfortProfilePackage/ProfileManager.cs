using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class ProfileManager
{
    [SerializeField]
    private string m_profileFolderPath = Application.streamingAssetsPath;
    public string ProfileFolderPath
    {
        get
        {
            return m_profileFolderPath;
        }
    }

    private (float, float, float) m_profileVersion = (1f, 0f, 0f);
    public (float, float, float) ProfileVersion
    {
        get => m_profileVersion;
    }

    public void CreateProfile(string nameOfProfile)
    {
        try
        {
            // Existing code for creating the VRPlayerComfortProfile object

            string filePath = Path.Combine(m_profileFolderPath, nameOfProfile + ".json");

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                VRPlayerComfortProfile newProfile = new(m_profileVersion, nameOfProfile);
                string jsonData = JsonConvert.SerializeObject(newProfile, Formatting.Indented);
                writer.Write(jsonData);
                writer.Close();
                Debug.Log($"Created profile at {filePath}");
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error creating profile: " + e.Message);
            // Handle the error gracefully (e.g., display a message to the user)
        }
    }
    public List<string> RetrieveAllProfilesOnDevice()
    {
        List<string> ProfileFilePaths = new List<string>();
        ProfileFilePaths = GetFilesInFolder(m_profileFolderPath);
        //come back to this later and confirm it
        return ProfileFilePaths;
    }


    public string RetrieveSpecifiedProfile(string nameOfProfile)
    {
        return null;
    }

    public ScriptableObject ParseProfileToScriptableObject(string ProfilePath)
    {
        return null;
    }

    public static List<string> GetFilesInFolder(string folderPath)
    {
        if (Directory.Exists(folderPath))
        {
            return Directory.GetFiles(folderPath).ToList();
        }
        else
        {
            Console.WriteLine($"Error: Folder not found at {folderPath}");
            return new List<string>();
        }
    }
}

