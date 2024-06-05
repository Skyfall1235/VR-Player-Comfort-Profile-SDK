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

    /// <summary>
    /// This method appears to be redundant and calls the overloaded version with the same parameter.
    /// Consider removing this overload.
    /// </summary>
    /// <param name="nameOfProfile">The name of the profile to be created.</param>
    public void CreateProfile(string nameOfProfile)
    {
        CreateProfile(nameOfProfile);
    }

    /// <summary>
    /// Creates a new VR player comfort profile with the specified name and settings.
    /// </summary>
    /// <param name="nameOfProfile">The name of the profile to be created.</param>
    /// <param name="profile">A VRPlayerComfortProfile object containing the desired settings.</param>
    public void CreateProfile(string nameOfProfile, VRPlayerComfortProfile profile)
    {
        CreateProfile(nameOfProfile, profile.MovementData, profile.VisualData, profile.OtherData);
    }

    /// <summary>
    /// Creates a new VR player comfort profile with the specified name and optional settings.
    /// </summary>
    /// <param name="nameOfProfile">The name of the profile to be created.</param>
    /// <param name="savedMovement">Optional movement settings for the profile (VRPlayerComfortProfile.Movement).</param>
    /// <param name="savedVisuals">Optional visual settings for the profile (VRPlayerComfortProfile.Visuals).</param>
    /// <param name="savedOther">Optional other settings for the profile (VRPlayerComfortProfile.Other).</param>
    public void CreateProfile(string nameOfProfile, VRPlayerComfortProfile.Movement savedMovement = null, VRPlayerComfortProfile.Visuals savedVisuals = null, VRPlayerComfortProfile.Other savedOther = null)
    {
        VRPlayerComfortProfile newProfile = new(m_profileVersion, nameOfProfile, savedMovement, savedVisuals, savedOther);
        try
        {

            string filePath = Path.Combine(m_profileFolderPath, nameOfProfile + ".json");

            //if we try to generate a profile with the same name, create a new version add add a (#) next to it to denote a new version
            if (System.IO.File.Exists(filePath))
            {
                int counter = 1;
                while (System.IO.File.Exists(filePath))
                {
                    nameOfProfile = $"{nameOfProfile}({counter})";
                    filePath = Path.Combine(m_profileFolderPath, nameOfProfile + ".json");
                    counter++;
                }
            }

            //now, write the data to a json file
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                //write data to json
                string jsonData = JsonConvert.SerializeObject(newProfile, Formatting.Indented);
                writer.Write(jsonData);
                writer.Close();
                //logging just in case
                Debug.Log($"Created profile at {filePath}");
            }
        }
        //gotta have error handling
        catch (IOException e)
        {
            Debug.LogError("Error creating profile: " + e.Message);
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

