using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PathManager : MonoBehaviour {

    public static PathManager instance;
    
    /// <summary>
    /// Transforms that contain on their children all the checkpoints an entity can traverse.
    /// </summary>
    [SerializeField] private List<PathGroup> paths;
    
    [SerializeField] private List<Transform> exits;

    private Dictionary<string, PathGroup> _pathGroupsById;

    private void Awake() {
        if (instance) {
            Destroy(instance.gameObject);
        }

        instance = this;
        _pathGroupsById = paths.ToDictionary(x => x.id);
    }

    public EntityPath GetRandomPathFromGroup(string id) {
        if (_pathGroupsById.TryGetValue(id, out PathGroup result)) {
            return result.GetRandomPath();
        }
        return null;
    }

    public Transform GetRandomExit() {
        return exits[Random.Range(0, exits.Count)];
    }
}

[Serializable]
public class PathGroup {

    public string id;
    
    public List<EntityPath> paths;
    
    public EntityPath GetRandomPath() {
        return paths[Random.Range(0, paths.Count)];
    }
}