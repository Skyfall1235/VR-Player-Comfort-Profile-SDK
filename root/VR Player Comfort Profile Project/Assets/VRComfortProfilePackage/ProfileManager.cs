using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[System.Serializable]
public class ProfileManager
{
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
            //check for existing file name before continuing. if it exists, create a new name by tacking on a (#)
            string fileName;
            Debug.Log(nameOfProfile);
            if (File.Exists(ProfileSetup.ProfileFolderPath + "/" + nameOfProfile + ".json"))
            {
                fileName = GenericSerialization.GetNextFilename(nameOfProfile, ProfileSetup.ProfileFolderPath);
                Debug.Log(fileName);
            }
            else
            {
                fileName = nameOfProfile + ".json";
            }
            //use custom class to serialize to file :)
            GenericSerialization.SerializeToJson(newProfile, ProfileSetup.ProfileFolderPath, fileName);
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
        ProfileFilePaths = GenericSerialization.GetFilesInFolder(ProfileSetup.ProfileFolderPath);
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
        bool confirmation = GenericSerialization.DeSerializeJsonToType(ProfilePath, out profile);
        output = profile;
        return confirmation;
    }

    

    #endregion
}

