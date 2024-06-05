# VR Player Comfort Profile SDK
 Welcome to the VR Player Comfort Profile SDK!

This project provides a shared configuration system for VR applications, allowing users to define their preferred settings in one place and have them automatically applied across different VR games and experiences. This eliminates the need to constantly adjust settings within each individual application.

## Features:

 - Centralized VR Comfort Profiles: Define and manage personal VR comfort settings, including locomotion methods, visuals, and haptic feedback preferences.
 - Cross-Application Compatibility: Profiles are designed to be portable across different VR applications that integrate the SDK.
 - Simplified User Experience: Users can configure their comfort preferences once and have them applied seamlessly across various VR experiences.
Getting Started (Unity Only - For Now!)

This initial release focuses on Unity integration. Future releases will expand to include support for Godot and Unreal Engine.

## Installation:

Download the latest Unity package from the [Releases](link to releases) section.
In your Unity project, navigate to Assets -> Import Package -> Custom Package.
Select the downloaded VR-Player-Comfort-Profile-SDK.unitypackage file and click Import.

## Why This Matters:

VR experiences can be incredibly immersive, but not everyone has the same level of tolerance for certain movement styles or visual effects. Constantly adjusting settings within each VR application can be tedious and disrupts the flow of the experience.
The VR-Player-Comfort-Profile-SDK aims to solve this problem by providing a centralized platform for managing personal VR comfort preferences.

## Current Profile Functionality:
### Movement
 - Turn Style: Choice between "snap turning" (discrete rotations) or "smooth turning" (analog stick control).
 - Turn Increment (degrees for snap turning): When using snap turning, this defines the amount of rotation in degrees for each snap.
 - Turn Increment (degrees per Second for smooth turning): When using smooth turning, this sets the rotation speed in degrees per second.
 - Locomotion Style: Preference for "snap movement" (teleportation) or "smooth movement" (continuous walking/flying).
 - Teleport Fade: Enables or disables a fade effect during teleportation.
 - Teleport Arc Color: A set of four floating-point values (RGBA) defining the color of the arc displayed during teleportation.

### Visuals
 - Use Vignette: Enables or disables a darkening effect applied to the periphery of the view.
 - Vignette Intensity: Controls the strength of the vignette effect (if enabled).
 - UI Scale: Adjusts the overall scale of the user interface elements.
 - UI Font Min and Max: Defines the minimum and maximum font sizes for UI elements.
 - UI Transparency: Sets the transparency level of UI elements.

### Other
 - Haptic Feedback Intensity Scaler: Scales the intensity of haptic feedback received through VR controllers.

## Call to Action:
VR Developers: Integrate the VR-Player-Comfort-Profile-SDK into your VR experiences to enhance user comfort and accessibility. This can lead to a more positive user experience and potentially wider adoption of your VR applications.
Community Contributors: We welcome your contributions! Report issues, suggest improvements, or submit pull requests to help us develop a robust and versatile VR comfort profile solution.

## Contributing:
We welcome contributions to the VR-Player-Comfort-Profile-SDK! Here are some ways you can get involved:

 - Report Issues: If you encounter any bugs or problems, please report them on our GitHub repository [VR Player Comfort Profile SDK](https://github.com/Skyfall1235/VR-Player-Comfort-Profile-SDK/).
 - Suggest Enhancements: Do you have ideas for new features or improvements? We'd love to hear them! Create an issue on GitHub or submit a pull request with your proposed changes.
 - Pull Requests: We encourage developers to contribute code improvements and fixes. Please follow the contribution guidelines outlined in the CONTRIBUTING.md file (once created).

## License
This project is licensed under the MIT License. See the LICENSE file for details.
