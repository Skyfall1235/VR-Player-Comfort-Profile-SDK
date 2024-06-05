using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

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

public class VRPlayerComfortProfile
{
    /// <summary>
    /// (Private) The profile version (major, minor, patch). Defaults to (1.0, 0.0, 0.0) in case of failure.
    /// </summary>
    private (float, float, float) m_profileVersion = (1f, 0f, 0f);

    /// <summary>
    /// A unique identifier for the profile.
    /// </summary>
    public Guid profileID;

    /// <summary>
    /// A custom Name for the Profile.
    /// </summary>
    public string ProfileName;

    /// <summary>
    /// An instance of the <see cref="Movement"/> class, containing settings related to movement and turning.
    /// </summary>
    public Movement movement;

    /// <summary>
    /// An instance of the <see cref="Visuals"/> class, containing settings related to visual appearance.
    /// </summary>
    public Visuals visuals;

    /// <summary>
    /// An instance of the <see cref="Other"/> class, containing miscellaneous settings.
    /// </summary>
    public Other other;

    /// <summary>
    /// Nested class representing movement and turning settings within a VR Player Comfort Profile.
    /// </summary>
    public class Movement
    {
        /// <summary>
        /// Defines the turning style (smooth or snap increments).
        /// </summary>
        public TurnStyle turnStyle;

        /// <summary>
        /// Applicable to "snap" turning, specifies the angle of each turn increment (in degrees).
        /// </summary>
        public float turnDegrees;

        /// <summary>
        /// Applicable to "smooth" turning, specifies the degrees of rotation per second.
        /// </summary>
        public float turnDegreePerSecond;

        /// <summary>
        /// Defines the locomotion style (smooth movement or teleportation).
        /// </summary>
        public LocomotionStyle locomotionStyle;

        /// <summary>
        /// Defines the source used to determine movement direction (hand, head, or hand average).
        /// </summary>
        public MovementDirectionSource movementDirectionSource;

        /// <summary>
        /// The color of the arc displayed during teleportation stored as a Vector4 (accessibility option).
        /// </summary>
        public (float, float, float, float) TeleportationArcColor;

        /// <summary>
        /// Constructor for the Movement class, allowing specific settings for movement and turning.
        /// </summary>
        /// <param name="turnStyle">The desired turning style (smooth or snap).</param>
        /// <param name="turnDegrees">Angle of each turn increment (degrees) - applicable to snap turning only.</param>
        /// <param name="turnDegreePerSecond">Degrees of rotation per second - applicable to smooth turning only.</param>
        /// <param name="locomotionStyle">The desired locomotion style (smooth movement or teleportation).</param>
        /// <param name="movementDirectionSource">The source used to determine movement direction (hand, head, or hand average).</param>
        /// <param name="teleportationArcColor">The color of the teleportation arc.</param>
        public Movement(TurnStyle turnStyle, float turnDegrees, float turnDegreePerSecond, LocomotionStyle locomotionStyle, MovementDirectionSource movementDirectionSource, (float,float,float,float) teleportationArcColor)
        {
            this.turnStyle = turnStyle;
            this.turnDegrees = turnDegrees;
            this.turnDegreePerSecond = turnDegreePerSecond;
            this.locomotionStyle = locomotionStyle;
            this.movementDirectionSource = movementDirectionSource;
            this.TeleportationArcColor = teleportationArcColor;
        }

        /// <summary>
        /// Default constructor for the Movement class, using default values for all settings.
        /// </summary>
        public Movement()
        {
            this.turnStyle = TurnStyle.Snap;
            this.turnDegrees = 15;
            this.turnDegreePerSecond = 30;
            this.locomotionStyle = LocomotionStyle.Teleport;
            this.movementDirectionSource = MovementDirectionSource.Head;
            this.TeleportationArcColor = (1f, 1f, 1f, 1f);
        }
    }

    /// <summary>
    /// Nested class representing visual appearance settings within a VR Player Comfort Profile.
    /// </summary>
    public class Visuals
    {
        /// <summary>
        /// Enables or disables a vignette effect (can improve focus).
        /// </summary>
        public bool UseVignette;

        /// <summary>
        /// Controls the intensity of the vignette effect (0.0 to 1.0).
        /// </summary>
        public float VignetteIntensity;

        /// <summary>
        /// Minimum allowed font size for UI elements.
        /// </summary>
        public float minimumSizeFont;

        /// <summary>
        /// Maximum allowed font size for UI elements.
        /// </summary>
        public float maximumSizeFont;

        /// <summary>
        /// Constructor for the Visuals class, allowing specific settings for visual appearance.
        /// </summary>
        /// <param name="useVignette">Enable (true) or disable (false) the vignette effect.</param>
        /// <param name="vignetteIntensity">Intensity of the vignette effect (0.0 to 1.0).</param>
        /// <param name="minimumSizeFont">Minimum allowed font size for UI elements.</param>
        /// <param name="maximumSizeFont">Maximum allowed font size for UI elements.</param>
        public Visuals(bool useVignette, float vignetteIntensity, float minimumSizeFont, float maximumSizeFont)
        {
            UseVignette = useVignette;
            VignetteIntensity = vignetteIntensity;
            this.minimumSizeFont = minimumSizeFont;
            this.maximumSizeFont = maximumSizeFont;
        }

        /// <summary>
        /// Default constructor for the Visuals class, using default values for all settings.
        /// </summary>
        public Visuals()
        {
            UseVignette = true;
            VignetteIntensity = 1f;
            this.minimumSizeFont = 10;
            this.maximumSizeFont = 24;
        }
    }

    /// <summary>
    /// Nested class representing miscellaneous settings within a VR Player Comfort Profile.
    /// </summary>
    public class Other
    {
        /// <summary>
        /// Enables or disables subtitles.
        /// </summary>
        public bool ShowSubtitles;

        /// <summary>
        /// Controls the intensity of haptic feedback in VR controllers (0.0 to 1.0).
        /// </summary>
        public float HapticFeedbackIntensity;

        /// <summary>
        /// Constructor for the Other class, allowing specific settings for miscellaneous options.
        /// </summary>
        /// <param name="showSubtitles">Enable (true) or disable (false) subtitles.</param>
        /// <param name="hapticFeedbackIntensity">Intensity of haptic feedback in VR controllers (0.0 to 1.0).</param>
        public Other(bool showSubtitles, float hapticFeedbackIntensity)
        {
            ShowSubtitles = showSubtitles;
            HapticFeedbackIntensity = hapticFeedbackIntensity;
        }

        /// <summary>
        /// Default constructor for the Other class, using default values for all settings.
        /// </summary>
        public Other()
        {
            ShowSubtitles = false;
            HapticFeedbackIntensity = 1f;
        }
    }

    /// <summary>
    /// Constructor for the VRPlayerComfortProfile class, allowing specific settings for all profile options.
    /// </summary>
    /// <param name="profileVersion">The profile version (major, minor, patch) as a tuple of floats.</param>
    /// <param name="profileID">A unique identifier for the profile (optional, a new Guid is generated if not specified).</param>
    /// <param name="movement">An instance of the Movement class containing movement and turning settings.</param>
    /// <param name="visuals">An instance of the Visuals class containing visual appearance settings.</param>
    /// <param name="other">An instance of the Other class containing miscellaneous settings.</param>
    public VRPlayerComfortProfile((float, float, float) profileVersion, string ProfileName, Guid profileID = default, Movement movement = null, Visuals visuals = null, Other other = null)
    {
        this.m_profileVersion = profileVersion;
        this.profileID = profileID == Guid.Empty ? Guid.NewGuid() : profileID; // Generate a new Guid if not provided
        this.ProfileName = ProfileName; //name is required
        this.movement = movement ?? new Movement(); // Use default Movement if not provided
        this.visuals = visuals ?? new Visuals(); // Use default Visuals if not provided
        this.other = other ?? new Other(); // Use default Other if not provided
    }

    /// <summary>
    /// Constructor for the VRPlayerComfortProfile class, allowing only the profile version to be specified.
    /// All other settings will use their default values.
    /// </summary>
    /// <param name="profileVersion">The profile version (major, minor, patch) as a tuple of floats.</param>
    /// <param name="ProfileName">The name of the Profile as a string.</param>
    public VRPlayerComfortProfile((float, float, float) profileVersion, string ProfileName)
    {
        this.m_profileVersion = profileVersion;
        this.ProfileName = ProfileName;
        this.profileID = Guid.NewGuid();
        this.movement = new Movement();
        this.visuals = new Visuals();
        this.other = new Other();
    }
}

/// <summary>
/// Defines options for the user's turning style in VR.
/// </summary>
public enum TurnStyle
{
    /// <summary>
    /// Enables smooth, continuous turning.
    /// </summary>
    Smooth,

    /// <summary>
    /// Enables turning in defined increments (like snapping to specific angles).
    /// </summary>
    Snap
}

/// <summary>
/// Defines options for the user's locomotion style in VR.
/// </summary>
public enum LocomotionStyle
{
    /// <summary>
    /// Enables smooth, continuous movement.
    /// </summary>
    Smooth,

    /// <summary>
    /// Enables teleportation for movement (瞬間移動 [shunkan idō] - Japanese for "instantaneous movement").
    /// </summary>
    Teleport
}

/// <summary>
/// Defines the source used to determine the user's movement direction in VR.
/// </summary>
public enum MovementDirectionSource
{
    /// <summary>
    /// Uses the user's left hand to determine movement direction.
    /// </summary>
    LeftHand,

    /// <summary>
    /// Uses the user's right hand to determine movement direction.
    /// </summary>
    RightHand,

    /// <summary>
    /// Uses the user's head orientation to determine movement direction.
    /// </summary>
    Head,

    /// <summary>
    /// Calculates the movement direction by averaging the directions from both hands.
    /// </summary>
    HandAverage
}
