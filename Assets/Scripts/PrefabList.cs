using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabList : MonoBehaviour {
    
    // Lookup dictionaries, for quickly finding the prefab, given a name.

    // List of all the prefabs that contain menu screens for UI.  Populated
    // via the Unity inspector.  Similar to the prefab list, these get
    // processed into a dictionary at runtime to make lookups easier.
    public MenuEntry[] menuScreens;
    
    [HideInInspector]
    public Dictionary<string, GameObject> menuLookup;
    [HideInInspector]
    public Dictionary<string, GameObject> menuGameObject;
    public MaterialEntry[] objectMaterials;
    public AnimatorEntry[] objectAnimators;
    public GameObjectEntry[] objectGame;
    void Awake()
    {
        menuLookup = new Dictionary<string, GameObject>();
        foreach (MenuEntry entry in menuScreens)
        {
            menuLookup[entry.name] = entry.prefab;
        }
        Debug.Log("menuLookUp Count=" + menuLookup.Count);
        menuGameObject = new Dictionary<string, GameObject>();
        foreach (GameObjectEntry entry in objectGame)
        {
            menuGameObject[entry.name] = entry.prefab;
        }
    }
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public struct MenuEntry
{
    public MenuEntry(string name, GameObject prefab)
    {
        this.name = name;
        this.prefab = prefab;
    }
    public string name;
    public GameObject prefab;
}
[System.Serializable]
public struct GameObjectEntry
{
    public GameObjectEntry(string name, GameObject prefab)
    {
        this.name = name;
        this.prefab = prefab;
    }
    public string name;
    public GameObject prefab;
}
[System.Serializable]
public struct MaterialEntry
{
    public MaterialEntry(string nameAnimation)
    {
        this.nameAnimation = nameAnimation;
        Materials = new List<Material>();
    }
    public string nameAnimation;
    public List<Material> Materials;
}
[System.Serializable]
public struct LabellEntry
{
    public LabellEntry(string nameAnimation, GameObject label)
    {

        this.Labels = label;
        this.nameAnimation = nameAnimation;
    }
    public string nameAnimation;
    public GameObject Labels;
}
[System.Serializable]
public struct AnimatorEntry
{
    public AnimatorEntry(int number, Animator anim)
    {

        this.Anim = anim;
        this.nameAnimation = number;
    }
    public int nameAnimation;
    public Animator Anim;
}

