using EnumUsed;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner3D_1_Axis : MonoBehaviour
{
    [SerializeField]
    private GameObject newMapToSpawn, oldMapToDelete, player;
    [SerializeField]
    private float timeToDelete;
    [SerializeField]
    private List<GameObject> allOldMapsToDelete = new List<GameObject>();
    private Vector3 whereToSpawn;
    private Bounds mapBounds;
    private float mapStartPos, mapEndPos, mapLength,mapMidPos, positionToChangeY, positionToChangeZ;
    private bool CanSpawn = true;

    void Start()
    {
        // DELETE THE OLD MAPS FOR BETTER PERFORMANCE
        InvokeRepeating(names.deleteOldMaps.ToString(), timeToDelete, timeToDelete);
        mapMidPos = oldMapToDelete.gameObject.GetComponent<Transform>().localPosition.z;
        mapBounds = oldMapToDelete.GetComponent<MeshFilter>().mesh.bounds;
        mapLength = mapBounds.extents.z;
        mapStartPos = (mapLength / 2) - mapMidPos;
        mapEndPos = (mapLength / 2) + mapMidPos;
        positionToChangeZ = mapEndPos;
    }
    void Update()
    {
        if (player.gameObject.GetComponent<Transform>().transform.position.z >= mapMidPos && CanSpawn)
        {
            CanSpawn = false;
            spawnNewMap();
        }
    }
    public void spawnNewMap()
    {
        allOldMapsToDelete.Add(oldMapToDelete);
        whereToSpawn = new Vector3(oldMapToDelete.transform.position.x, oldMapToDelete.transform.position.y, oldMapToDelete.transform.position.z + positionToChangeZ);
        oldMapToDelete = Instantiate(newMapToSpawn, whereToSpawn, Quaternion.identity);
        transform.position = new Vector3(transform.position.x, transform.position.y + positionToChangeY, transform.position.z + positionToChangeZ);
        mapMidPos = oldMapToDelete.gameObject.GetComponent<Transform>().position.z;
        CanSpawn = true;

    }

    private void deleteOldMaps()
    {
        int i = allOldMapsToDelete.Count;
        for (int j = 0; j < i - 1; j++)
        {
            Destroy(allOldMapsToDelete[j]);
        }
        allOldMapsToDelete.RemoveRange(0, i - 1);
    }
}
