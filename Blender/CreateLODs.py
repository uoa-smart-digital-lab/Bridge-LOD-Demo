# **************************************************************************************************************
# * Created by Roy Davies at the University of Auckland Smart Digital Lab.  (sdl.blogs.auckland.ac.nz)
# * 
# * There are no restrictions on using this script.  Take it, modify it, improve it, make it your own...
# * However, I would appreciate being told about any improvements you make as I am neither a python programmer
# * nor a Blender user - so this has been cobbled together, and undoubtably could be done in a better way.
# * roy.davies@auckland.ac.nz or sdl@auckland.ac.nz
# **************************************************************************************************************

import bpy
import os
import sys
import glob
import shutil

# --------------------------------------------------------------------------------------------------------------
# Settings
# --------------------------------------------------------------------------------------------------------------
current_file_dir = os.path.dirname(__file__)
print (current_file_dir)
input_directory = current_file_dir + '\\..\\..\\RawData'
output_directory = current_file_dir + '\\..\\..\\Assets\\ProcessedData'

# LOD levels
LODLevels = [0.6, 0.4, 0.1]
# --------------------------------------------------------------------------------------------------------------



# --------------------------------------------------------------------------------------------------------------
# Clear the Scene
# --------------------------------------------------------------------------------------------------------------
print ("Clearing the Scene")

bpy.ops.object.select_by_type(type='MESH')
bpy.ops.object.delete(use_global=False)

for item in bpy.data.meshes:
    bpy.data.meshes.remove(item)

# Get a list of all the sub directories
obj_directories = glob.glob(input_directory + '\\*')

# --------------------------------------------------------------------------------------------------------------
# import the original objects
# --------------------------------------------------------------------------------------------------------------
print ("Importing Original Meshes")

for dir in obj_directories:
    # Get obj file names
    obj_file = glob.glob(dir + '\\*.obj')
    jpg_file = glob.glob(dir + '\\*.jpg')
    mtl_file = glob.glob(dir + '\\*.mtl')
    print (obj_file)
        
    # Import obj file
    bpy.ops.import_scene.obj(filepath = obj_file[0])
    
    # Copy mtl and jpg files
    shutil.copy(jpg_file[0], output_directory)
    shutil.copy(mtl_file[0], output_directory)

# Select all the recently imported object meshes
for obj in bpy.context.scene.objects:
    obj.select_set(obj.type == "MESH")

# --------------------------------------------------------------------------------------------------------------
# Create the LODs
# --------------------------------------------------------------------------------------------------------------
print ("Creating the LODs")    

basenames = []
for obj in bpy.context.selected_objects:
    basename = obj.name
    basenames.append(basename)
    
    print("    " + basename)
    print("        LOD0")
    
    # Rename the first LOD
    obj.name = basename + '_LOD0'
    
    # Create copies of the original object for each LOD
    LODCounter = 1
    for LODLevel in LODLevels:
        print("        LOD" + str(LODCounter))
        
        # Copy the object and name as per the Unity convention
        new_obj = obj.copy()
        new_obj.data = obj.data.copy()
        new_obj.name = basename + '_LOD' + str(LODCounter)
        
        # Add the modifier
        modifier = new_obj.modifiers.new(type = 'DECIMATE', name = new_obj.name)
        modifier.ratio = LODLevels[LODCounter-1]
        
        # Make sure it is in the current collection and this one is active
        bpy.context.collection.objects.link(new_obj)
        bpy.context.view_layer.objects.active = new_obj
        
        # Apply the modifier
        bpy.ops.object.modifier_apply(modifier = new_obj.name)
        LODCounter = LODCounter + 1;    
    
# --------------------------------------------------------------------------------------------------------------
# Export the fbx files for each object, with the LODs
# --------------------------------------------------------------------------------------------------------------
print ("Exporting the FBX files")

for basename in basenames:
    print("    " + basename)
    
    # Deselect all objects
    for obj in bpy.context.scene.objects:
        obj.select_set(obj.type == "NONE")
    
    # Select only the objects for this section
    for obj in bpy.context.scene.objects:
        if obj.name.startswith(basename):
            obj.select_set (True)
    
    # Export the FBX file with the LODs
    bpy.ops.export_scene.fbx(filepath = output_directory + '\\' + basename + '.fbx', use_selection = True, mesh_smooth_type = 'FACE', use_tspace = True, object_types = {'MESH'})
    
# --------------------------------------------------------------------------------------------------------------
# Clear the Scene again so as not to leave behind a big set of files
# --------------------------------------------------------------------------------------------------------------
print ("Cleaning up")

bpy.ops.object.select_by_type(type='MESH')
bpy.ops.object.delete(use_global=False)

for item in bpy.data.meshes:
    bpy.data.meshes.remove(item)
    
print("ALL DONE !!")
