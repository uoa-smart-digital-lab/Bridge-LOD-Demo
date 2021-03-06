# Bridge LOD Demo

### Introduction
Blender LOD processing demo of 3D-scanned data for Unity 3D.  The purpose is to be able to move around in and interact with 3D scanned data (which often tends to have a very high polygon count), using a standalone VR headset such as the Oculus Quest 2.  One way to achieve this with Unity is to create Levels of Detail (LODs) that are activated by distance to the camera.  Given that the scanned data is divided up into sections, it makes sense to create unity LODs from those sections.  One tool that you can use for that is Blender, but to do it by hand is tedious - hence this script.

* The Blender script assumes that the point cloud data has already been processed into obj files with attached materials, placed into a folder called RawData, with each section in its own subfolder.
* Output goes to a folder inside the unity Assets folder called ProcessedData.
* To use this, first open the Blender file in the blender folder.  This contains a script that loads obj files from the RawData directory, and processes them to create Levels of Detail (LOD) for Unity as per the instructions here: https://docs.unity3d.com/Manual/LevelOfDetail.html and here: https://docs.unity3d.com/Manual/importing-lod-meshes.html
* This is not a generic script - you will need to modify it for your needs, but hopefully you can see and understand what it is doing if you are familiar with Blender.
* You can set different Levels of Detail (and add more) by modifying the LODLevels Array at the top of the script.  Keep the levels in the order of highest to lowest.
* In Unity, you can modify at what distances the Levels of Detail activate in the Quality Settings.

### To run
* To use with the included set of data, just press the 'run' button in Blender Scripting.
* Before doing that, it is a good idea to open the Blender System Console (under the Window menu), so you can see what is going on.
* BE WARNED: This takes ages to run, both at the Blender stage, then in Unity Importing the updated fbx files, then building and running on the Oculus Quest (or whatever device you are using).

### The unity project
* This example also includes optimisations from the OpenXR-UX system, which in this case means that when moving, the image quality drops a little to allow smoother movement, but when standing still, increases to allow the finer details of the 3D model to be seen.
* You can find out more about the OpenXR UX base system here: https://github.com/uoa-smart-digital-lab/openxr_ux_unity_base.git

### Moving around
* Use the right hand thumbstick to move back and forward, and to rotate left and right.  Use the left hand thumbstick to go up and down.
* There are no barriers to flying right off the bridge, but this is intentional so you can inspect the outer edges and underneath as well.

### Improvements
* Feel free to take this and use it in your own projects - and if you make improvements to the script, please send this over so we can improve this for all.

### Caveats
* I am neither a Python programmer nor a Blender user, so this code has been written in a rather blunt way with a heavy reliance on what I could find on the internet.  There could easily be better, faster, more performant ways to achieve the same outcomes.  If there are, please do let me know - that's the great thing about opensource code... (roy.davies@auckland.ac.nz) 
