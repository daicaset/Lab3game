using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private int lastPrefabIndex = 0;
    private Transform player;
    private float spawnZ = 0.0f;
    private List<GameObject> activeTiles;
    private float safeZone = 15.0f;
    private float tileLength = 10.0f;
    private int tilesOnScreen = 3;
    void Start()
    {
        activeTiles = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        for(int i = 0; i < tilesOnScreen; i++){
            if(i < 2){
                SinhDiaHinh(0);
            } else{
                SinhDiaHinh();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.position.z - safeZone > (spawnZ - tilesOnScreen * tileLength)){
            SinhDiaHinh();
            XoaDiaHinh();
        }
    }
    private void XoaDiaHinh(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private void SinhDiaHinh(int prefabIndex = -1){
        GameObject gameObject;
        if(prefabIndex == -1)
            gameObject = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            gameObject = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        gameObject.transform.SetParent(transform);        
        gameObject.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(gameObject);   
    }

    private int RandomPrefabIndex(){
        if(tilePrefabs.Length <= 1){
            return 0;
        } 
        int randomIndex = lastPrefabIndex;
        while(randomIndex == lastPrefabIndex){
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
    
}
