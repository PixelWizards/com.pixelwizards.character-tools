using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTools
{
    
    /// <summary>
    /// Given 2 meshes that are skinned to the same hierarchy of bones, this will copy the bone info from the Target Mesh onto the Source mesh.
    /// 
    /// Useful for example to unify skinned clothing meshes onto a target character for dynamic character creation 
    /// </summary>
    /// <param name="meshTarget">The Mesh that contains the target list of bones that we want to copy</param>
    /// <param name="meshSource">The source mesh that we are looking to merge bones onto</param>
    public static void MergeBoneHierarchy(SkinnedMeshRenderer meshTarget, SkinnedMeshRenderer meshSource)
    {
        var bones = new Dictionary<string, Transform>(); //main body bones
        foreach (Transform bone in meshTarget.bones) //make a dict for easy transform lookup
        {
            bones[bone.name] = bone;
        }
        var replacementBones = new List<Transform>(); //this will hold
        foreach (Transform bone in meshSource.bones)
        {
            if (bones.ContainsKey(bone.name))
            {
                replacementBones.Add(bones[bone.name]);
            }
        }
        meshSource.bones = replacementBones.ToArray();                    //convert the list to an array and assign
        meshSource.transform.SetParent(meshTarget.transform, false);  //move the clothing over to the body GO
        meshSource.rootBone = meshTarget.rootBone;                    //point it to the new root bone.
    }
}
