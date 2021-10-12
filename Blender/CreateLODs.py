import bpy
import os
import sys
import glob
import shutil

# Input and Output Directories
current_file_dir = os.path.dirname(__file__)
print (current_file_dir)
input_directory = current_file_dir + '\\..\\..\\RawData'
output_directory = current_file_dir + '\\..\\..\\Assets\\ProcessedData'

# LOD levels
LODLevels = [0.6, 0.4, 0.1]

# Clear the Scene
bpy.ops.object.select_by_type(type='MESH')
bpy.ops.object.delete(use_global=False)

for item in bpy.data.meshes:
    bpy.data.meshes.remove(item)

# Get a list of all the sub directories
obj_directories = glob.glob(input_directory + '\\*')
print ("Importing Original Meshes")

# import the original meshes
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

print ("Creating the LODs")    
# Create the LODs
basenames = []
for obj in bpy.context.selected_objects:
    basename = obj.name
    basenames.append(basename)
    
    print("    " + basename)
    print("        LOD0")
    
    # Rename the first LOD
    obj.name = basename + '_LOD0'
    
    LODCounter = 1
    for LODLevel in LODLevels:
        print("        LOD" + str(LODCounter))
        new_obj = obj.copy()
        new_obj.data = obj.data.copy()
        new_obj.name = basename + '_LOD' + str(LODCounter)
        modifier = new_obj.modifiers.new(type = 'DECIMATE', name = new_obj.name)
        modifier.ratio = LODLevels[LODCounter-1]
        bpy.context.collection.objects.link(new_obj)
        bpy.context.view_layer.objects.active = new_obj
        bpy.ops.object.modifier_apply(modifier = new_obj.name)
        LODCounter = LODCounter + 1;    
    
# Export the fbx files for each object, with the LODs
print ("Exporting the FBX files") 
for basename in basenames:
    print("    " + basename)
    for obj in bpy.context.scene.objects:
        obj.select_set(obj.type == "NONE")
        
    for obj in bpy.context.scene.objects:
        if obj.name.startswith(basename):
            obj.select_set (True)
                
    bpy.ops.export_scene.fbx(filepath = output_directory + '\\' + basename + '.fbx', use_selection = True, mesh_smooth_type = 'FACE', use_tspace = True, object_types = {'MESH'})
    
# Clear the Scene
bpy.ops.object.select_by_type(type='MESH')
bpy.ops.object.delete(use_global=False)

for item in bpy.data.meshes:
    bpy.data.meshes.remove(item)
    
print("ALL DONE !!")
