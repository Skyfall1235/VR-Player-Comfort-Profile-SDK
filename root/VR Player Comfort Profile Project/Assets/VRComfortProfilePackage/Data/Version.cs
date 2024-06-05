using System;
using Unity;
using UnityEngine;
/// <summary>
/// Represents a version with major, minor, and subMinor components.
/// </summary>
[System.Serializable]
public struct Version
{
    // Represents a zero-initialized Version.
    internal static Version zero = new Version(0, 0, 0);

    // Components of the version.
    [SerializeField]
    private short m_major;
    public short Major
    { get => m_major; }

    [SerializeField]
    private short m_minor;
    public short Minor
    { get => m_minor; }

    [SerializeField]
    private short m_subMinor;
    public short SubMinor
    { get => m_subMinor; }

    /// <summary>
    /// Initializes a Version with specified components.
    /// </summary>
    public Version(short _major, short _minor, short _subMinor)
    {
        m_major = _major;
        m_minor = _minor;
        m_subMinor = _subMinor;
    }

    /// <summary>
    /// Parses a version string in the format "major.minor.subMinor". If the format is invalid, initializes the version to zero.
    /// </summary>
    internal Version(string _version)
    {
        string[] _versionStrings = _version.Split('.');
        if (_versionStrings.Length != 3)
        {
            m_major = 0;
            m_minor = 0;
            m_subMinor = 0;
            return;
        }
        m_major = short.Parse(_versionStrings[0]);
        m_minor = short.Parse(_versionStrings[1]);
        m_subMinor = short.Parse(_versionStrings[2]);
    }

    /// <summary>
    /// Checks if the current version is different from another version.
    /// </summary>
    /// <returns> True if any component differs, otherwise returns false.</returns>
    internal bool IsDifferentThan(Version _otherVersion)
    {
        if (m_major != _otherVersion.Major || m_minor != _otherVersion.Minor || m_subMinor != _otherVersion.SubMinor)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Overrides the ToString() method to represent the version as a string.
    /// </summary>
    public override string ToString()
    {
        return $"{m_major}.{m_minor}.{m_subMinor}";
    }
}

