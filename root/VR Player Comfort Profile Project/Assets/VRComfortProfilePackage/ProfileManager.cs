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

    #region Create Profile Overrides

    /// <summary>
    /// This method appears to be redundant and calls the overloaded version with the same parameter.
    /// Consider removing this overload.
    /// </summary>
    /// <param name="nameOfProfile">The name of the profile to be created.</param>
    public void CreateProfile(string nameOfProfile)
    {
        CreateProfile(nameOfProfile, null, null, null);
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
            string fileName;
            Debug.Log(nameOfProfile);
            if (File.Exists(m_profileFolderPath + "/" + nameOfProfile + ".json"))
            {
                fileName = GetNextFilename(nameOfProfile, m_profileFolderPath);
                Debug.Log(fileName);
            }
            else
            {
                fileName = nameOfProfile + ".json";
            }
            string filePath = Path.Combine(m_profileFolderPath, fileName);
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

    #endregion

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

    public VRPlayerComfortProfile TryParseProfile(string ProfilePath)
    {
        VRPlayerComfortProfile parsedProfile;
        bool confirmation = ParseProfile(ProfilePath, out parsedProfile);
        if(confirmation)
        {
            return parsedProfile;
        }
        else
        {
            return null;
        }
    }

    #region Internal execution

    private bool ParseProfile(string ProfilePath, out VRPlayerComfortProfile output)
    {
        VRPlayerComfortProfile profile;//profile we will store the output in
        try
        {
            //read text
            string jsonData = File.ReadAllText(ProfilePath);//we know this works
            Debug.Log(jsonData);
            try
            {
                profile = JsonConvert.DeserializeObject<VRPlayerComfortProfile>(jsonData)!;

                //for debugging serialization and to confirm process
                if (profile != null)
                {
                    Debug.Log("Profile deserialized successfully!");
                }
                else
                {
                    Debug.Log("Deserialization failed!");

                }

                //output
                output = profile;
                return true;
            }
            catch (JsonException e)
            {
                Debug.LogError("Error deserializing profile: " + e.Message);
                output = null;
                return false;
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Error creating profile: " + e.Message);
            output = null;
            return false;
        }
    }

    private static List<string> GetFilesInFolder(string folderPath)
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

    private string GetNextFilename(string filename, string filePath)
    {
        int counter = 1;
        string newFilename = filename + " ({0})";  // Use newFilename to avoid confusion

        while (File.Exists(Path.Combine(filePath, string.Format(newFilename, counter) + ".json")))
        {
            counter++;
            Debug.Log(newFilename);
        }

        return Path.Combine(filePath, string.Format(newFilename, counter) + ".json");
    }

    #endregion
}

