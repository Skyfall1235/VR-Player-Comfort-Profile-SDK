using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class ProfileSetup
{
    [SerializeField]
    private static string m_profileFolderPath; // exposes the location of profiles for THIS game
    public static string ProfileFolderPath
    {
        get
        {
            if(m_profileFolderPath == null)
            {
                //deserialize and set
                ProfileLocation datumLocation;//profile we will store the output in
                bool confirmation = GenericSerialization.DeSerializeJsonToType(m_streamingAssetPath + "/ProfileJsonLocation.json", out datumLocation);

                //confirmation requirement to ensure path exists before attempting to retrieve it.
                if(confirmation)
                {
                    m_profileFolderPath = datumLocation.ProfileJsonFolderLocation;
                }
                else
                {
                    //error notice stating something in the chain went wrong
                    m_profileFolderPath = null;
                    Debug.LogError("ProfileJsonLocation JSON not created before accessing, failing profile folder path retrieval.");
                }
                
                //return after setting
                return m_profileFolderPath;
            }
            else
            {
                //refer to the profile location
                return m_profileFolderPath;
            }
        }
    }

    [SerializeField]
    private static ProfileLocation m_profileLocation;//exposes the location of the profile location

    private static string m_streamingAssetPath = Application.streamingAssetsPath + "/VRComfortProfileData"; // where the profile pointer is stored

    /// <summary>
    /// Creates the asset path needed for storage of profile locations
    /// </summary>
    public static void CreateAssetPathDirectory()
    {
        //creat the folder for profile data locations
        Directory.CreateDirectory(m_streamingAssetPath);
    }

    /// <summary>
    /// Sets up the location for storing VR player comfort profiles.
    /// </summary>
    /// <param name="profileFolderPath">The path to the folder where VR player comfort profiles will be stored.</param>
    /// <remarks>
    /// This method saves the provided path to a file in the StreamingAssets folder using serialization.
    /// The saved data is used to determine the location of VR player comfort profiles later.
    /// This should be called after a user opens a game for the first time. 
    /// have them select a folder for profiles to be stored in. this will let THIS game know where to look for profiles
    /// </remarks>
    public static void Setup(string profileFolderPath)
    {
        //we save the location to the streaming assets folder
        SerializeProfileLocations(profileFolderPath);
    }

    //serialize where the profile locations are
    private static void SerializeProfileLocations(string profileFolderPath)
    {
        ProfileLocation profileLocation = new ProfileLocation();
        m_profileLocation.ProfileJsonFolderLocation = profileFolderPath;
        if(!File.Exists(m_streamingAssetPath))
        {
            GenericSerialization.SerializeToJson(profileLocation, m_streamingAssetPath, "ProfileJsonLocation");
        }
    }

    

    /// <summary>
    /// Used to store the location of profiles, whether created from this game or any other.
    /// </summary>
    class ProfileLocation
    {
        [JsonProperty("ProfileJsonFolderLocation")]
        public string ProfileJsonFolderLocation { get; set; }
    }

}
