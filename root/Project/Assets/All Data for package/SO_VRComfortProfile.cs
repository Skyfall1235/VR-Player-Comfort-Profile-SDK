using UnityEngine;
using static VRPlayerComfortProfile;
using System;


public class SO_VRComfortProfile : ScriptableObject
{
    /// <summary>
    /// (Private) The profile version (major, minor, patch). Defaults to (1.0, 0.0, 0.0) in case of failure.
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private (float, float, float) m_profileVersion = (1f, 0f, 0f);
    public (float, float, float) ProfileVersion
    {
        get => m_profileVersion;
    }

    /// <summary>
    /// A unique identifier for the profile.
    /// </summary>
    [SerializeField]
    private Guid m_profileID;
    public Guid ProfileID
    {
        get => m_profileID;
    }

    /// <summary>
    /// A custom Name for the Profile.
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private string m_profileName;
    public string ProfileName
    {
        get => m_profileName;
    }

    /// <summary>
    /// An instance of the <see cref="Movement"/> class, containing settings related to movement and turning.
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private Movement m_movement;
    public Movement movement
    {
        get => m_movement;
    }

    /// <summary>
    /// An instance of the <see cref="Visuals"/> class, containing settings related to visual appearance.
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private Visuals m_visuals;
    public Visuals visuals
    {
        get => m_visuals;
    }

    /// <summary>
    /// An instance of the <see cref="Other"/> class, containing miscellaneous settings.
    /// </summary>
    [SerializeField]
    [ReadOnly]
    private Other m_other;
    public Other other
    {
        get => m_other;
    }
}
