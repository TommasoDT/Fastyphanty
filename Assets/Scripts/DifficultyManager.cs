using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private Transform car;
    private FloorGenerator floorGenerator;
    private CollectObSpawner spawnerFuel;
    public float Difficulty = 0;

    public int MaxHeightStart = 100;
    public int FuelOffsetXStart = 100;
    public int FuelDifficultyDivider = 5;
    public int PositionDivider = 5000;
    

    void Awake()
    {
        spawnerFuel = GameObject.Find("Spawner").GetComponent<CollectObSpawner>();
        floorGenerator = GetComponent<FloorGenerator>();
        car = GameObject.Find("Car").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        floorGenerator.MaxHeightStep = MaxHeightStart;
        spawnerFuel.spawnoffsetx = FuelOffsetXStart;
    }

    // Update is called once per frame
    void Update()
    {
        Difficulty = car.position.x / PositionDivider;

        floorGenerator.MaxHeightStep = MaxHeightStart * (Difficulty + 1);
        spawnerFuel.spawnoffsetx = FuelOffsetXStart * ((Difficulty / FuelDifficultyDivider) + 1);
    }
}
