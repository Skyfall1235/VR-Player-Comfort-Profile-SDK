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



    // 6/5/24 : New Version, bumping to 1.1.0
    private Version m_profileVersion = new Version(1, 1, 0);
    public Version ProfileVersion
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

    public void ParseProfile(string ProfilePath)
    {
        try
        {
            //read text
            string jsonData = File.ReadAllText(ProfilePath);
            //deserialize
            VRPlayerComfortProfile profile = JsonConvert.DeserializeObject<VRPlayerComfortProfile>(jsonData);
            //save in the instanced profile for access  
            InstancedProfile instance = GameObject.FindObjectOfType<InstancedProfile>();
            if (instance != null)
            {
                instance.SelectedProfile = profile;
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error creating profile: " + e.Message);
        }
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

