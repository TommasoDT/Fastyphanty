using UnityEngine;
using UnityEngine.U2D;

public class FloorGenerator : MonoBehaviour
{
    private GameObject Line;
    private GameObject Wall;
    private GameObject Terrain;
    private GameObject SpawnedObjects;

    private SpriteShapeController lineSpriteShapeController;
    private SpriteShapeController terrainSpriteShapeController;
    
    //Interfaccia per altri script (difficoltà) e regolazioni
    public float MinWidthStep = 1f;
    public float MaxWidthStep = 1f;
    public float MinHeightStep = 0f;
    public float MaxHeightStep = 1f;

    //Valori per ottimizzazioni
    public int RenderDistance = 5; //Misurato in segmenti/curve
    public int KeepDistance = 5; //Misurato in segmenti/curve

    public int FoliagePerSegment = 5;
    private int FoliageDistance = 3; //Misurato in edges, da un effetto di spawn random

    public int SegmentPerRock = 2;
    private int CurrentSegmentPerRock = 0;
    
    public int PeanutPerSegment = 2;
    private int PeanutDistance = 2;

    //Valori generici
    private int TerrainHeight = 50;

    //Prefab necessari
    public GameObject PrefabErba;
    public GameObject PrefabSasso;
    public GameObject PrefabPeanut;

    // Awake is called when object is constructed
    void Awake()
    {
        Line = this.transform.GetChild(0).gameObject;
        Wall = this.transform.GetChild(1).gameObject;
        Terrain = this.transform.GetChild(2).gameObject;
        SpawnedObjects = this.transform.GetChild(3).gameObject;

        lineSpriteShapeController = Line.GetComponent<SpriteShapeController>();
        lineSpriteShapeController.spline.Clear();

        terrainSpriteShapeController = Terrain.GetComponent<SpriteShapeController>();
        terrainSpriteShapeController.spline.Clear();
    }

    void Start()
    {
        MakeCurve(0, new Vector2(-100, 0), new Vector2(-80, 0), new Vector2(80, 0), new Vector2(100, 0));
        CreateTerrainCorners();
    }

    // Update is called once per frame
    void Update()
    {
        //Creazione segmenti/curve
        if (Tools.xDistanceFromCamera(Camera.main, lineSpriteShapeController.spline.GetPosition(lineSpriteShapeController.spline.GetPointCount() - 1) / 5) - RenderDistance < 0)
        {
            ContinueCurve();
            UpdateTerrainCorners();
            GenerateGrass();
            GenerateRocks();
            GeneratePeanut();
        }

        //Scaricamento segmenti fuori dalla visione della telecamera
        if (Tools.xDistanceFromCamera(Camera.main, lineSpriteShapeController.spline.GetPosition(0) / 5) + KeepDistance < 0)
        {
            UnloadFirstCurve();
            UpdateTerrainCorners();
            RemoveSpawnedObjectsInUnloadedArea();
            UpdateWall();
        }
    }

    //METODI PUBBLICI
    public Vector2 Spawn(GameObject objectToSpawn, bool angled, int offsetIndex, float offsetY) //Spawn at length - offsetIndex position plus eventual Y offset, returns spawnPoint
    {
        int spawnPointIndex = lineSpriteShapeController.edgeCollider.pointCount - offsetIndex;

        Vector2 spawnPoint = lineSpriteShapeController.edgeCollider.points[spawnPointIndex] / 5;

        GameObject oggettoCreato = Instantiate(objectToSpawn, new Vector2(spawnPoint.x, spawnPoint.y + offsetY), new Quaternion(), SpawnedObjects.transform);

        if(angled)
        {
            Vector2 rightPoint = lineSpriteShapeController.edgeCollider.points[spawnPointIndex + 1] / 5;
            Vector2 leftPoint = lineSpriteShapeController.edgeCollider.points[spawnPointIndex - 1] / 5;

            if(rightPoint.y > leftPoint.y)
                oggettoCreato.transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan((rightPoint.y - leftPoint.y) / (rightPoint.x - leftPoint.x)));
            else
                oggettoCreato.transform.eulerAngles = new Vector3(0, 0, -1 * Mathf.Rad2Deg * Mathf.Atan((leftPoint.y - rightPoint.y) / (rightPoint.x - leftPoint.x)));
        }

        return spawnPoint;
    }

    public Vector2 Spawn(GameObject objectToSpawn, bool angled) //Spawn at last position available
    {
        return Spawn(objectToSpawn, angled, 2, 0); //Length - 2 for spawnpoint, +1 for right point and -1 for left point
    }

    //METODI PRIVATI
    Vector2 GenerateNextPoint(Vector2 previousPoint)
    {
        return new Vector2(
            Random.Range(previousPoint.x + MinWidthStep, previousPoint.x + MaxWidthStep),
            Random.Range(MinHeightStep, MaxHeightStep)
        );
    }

    void MakeCurve(int insertAt, Vector2 startPoint, Vector2 midPoint1, Vector2 midPoint2, Vector2 endPoint)
    {
        //Generate line
        lineSpriteShapeController.spline.InsertPointAt(insertAt, startPoint);
        lineSpriteShapeController.spline.SetTangentMode(insertAt, ShapeTangentMode.Broken);
        lineSpriteShapeController.spline.SetRightTangent(insertAt, midPoint1 - startPoint);

        lineSpriteShapeController.spline.InsertPointAt(insertAt + 1, endPoint);
        lineSpriteShapeController.spline.SetTangentMode(insertAt + 1, ShapeTangentMode.Broken);
        lineSpriteShapeController.spline.SetLeftTangent(insertAt + 1, midPoint2 - endPoint);

        //Generate terrain
        terrainSpriteShapeController.spline.InsertPointAt(insertAt, startPoint);
        terrainSpriteShapeController.spline.SetTangentMode(insertAt, ShapeTangentMode.Broken);
        terrainSpriteShapeController.spline.SetRightTangent(insertAt, midPoint1 - startPoint);

        terrainSpriteShapeController.spline.InsertPointAt(insertAt + 1, endPoint);
        terrainSpriteShapeController.spline.SetTangentMode(insertAt + 1, ShapeTangentMode.Broken);
        terrainSpriteShapeController.spline.SetLeftTangent(insertAt + 1, midPoint2 - endPoint);
    }

    void ContinueCurve()
    {
        //Generate line
        //Generate terrain: Inserisce la stessa curva creata nella linea, nel terreno partendo dall'indice della linea così da tenere sempre alla fine le ultime due posizioni che chiudono il terreno.
        Vector2 previousEndPoint = lineSpriteShapeController.spline.GetPosition(lineSpriteShapeController.spline.GetPointCount() - 1);
        Vector2 midPoint1 = (previousEndPoint * 2) - ((Vector2)lineSpriteShapeController.spline.GetLeftTangent(lineSpriteShapeController.spline.GetPointCount() - 1) + previousEndPoint);
        Vector2 midPoint2 = GenerateNextPoint(previousEndPoint);
        Vector2 endPoint = GenerateNextPoint(midPoint2);

        lineSpriteShapeController.spline.SetTangentMode(lineSpriteShapeController.spline.GetPointCount() - 1, ShapeTangentMode.Continuous);
        lineSpriteShapeController.spline.SetRightTangent(lineSpriteShapeController.spline.GetPointCount() - 1, midPoint1 - previousEndPoint);

        terrainSpriteShapeController.spline.SetTangentMode(lineSpriteShapeController.spline.GetPointCount() - 1, ShapeTangentMode.Continuous);
        terrainSpriteShapeController.spline.SetRightTangent(lineSpriteShapeController.spline.GetPointCount() - 1, midPoint1 - previousEndPoint);

        lineSpriteShapeController.spline.InsertPointAt(lineSpriteShapeController.spline.GetPointCount(), endPoint);
        lineSpriteShapeController.spline.SetTangentMode(lineSpriteShapeController.spline.GetPointCount() - 1, ShapeTangentMode.Broken);
        lineSpriteShapeController.spline.SetLeftTangent(lineSpriteShapeController.spline.GetPointCount() - 1, midPoint2 - endPoint);
        
        terrainSpriteShapeController.spline.InsertPointAt(lineSpriteShapeController.spline.GetPointCount() - 1, endPoint);
        terrainSpriteShapeController.spline.SetTangentMode(lineSpriteShapeController.spline.GetPointCount() - 1, ShapeTangentMode.Broken);
        terrainSpriteShapeController.spline.SetLeftTangent(lineSpriteShapeController.spline.GetPointCount() - 1, midPoint2 - endPoint);
    }

    void GenerateGrass()
    {
        for(int i = 0; i < FoliagePerSegment; i++)
            Spawn(PrefabErba, true, (i * FoliageDistance) + 2, 0.4f);
    }

    void GenerateRocks()
    {
        if(CurrentSegmentPerRock >= SegmentPerRock)
        {
            Spawn(PrefabSasso, true, 2, 0.2f);
            CurrentSegmentPerRock = 0;
        }
        else
            CurrentSegmentPerRock++;
    }

    void GeneratePeanut()
    {
        for(int i = 0; i < Random.Range(1, PeanutPerSegment); i++)
            Spawn(PrefabPeanut, false, (i * PeanutDistance) + 6, 1.2f);
    }

    void UnloadFirstCurve()
    {
        //Unload line
        lineSpriteShapeController.spline.RemovePointAt(0);
        lineSpriteShapeController.spline.SetTangentMode(1, ShapeTangentMode.Broken);

        //Unload terrain
        terrainSpriteShapeController.spline.RemovePointAt(0);
        terrainSpriteShapeController.spline.SetTangentMode(1, ShapeTangentMode.Broken);
        UpdateTerrainCorners();
    }

    void RemoveSpawnedObjectsInUnloadedArea()
    {
        for(int i = 0; i < SpawnedObjects.transform.childCount; i++)
            if(SpawnedObjects.transform.GetChild(i).transform.position.x < lineSpriteShapeController.spline.GetPosition(0).x / 5 || SpawnedObjects.transform.GetChild(i).transform.position.y < -TerrainHeight)
                Destroy(SpawnedObjects.transform.GetChild(i).gameObject);
    }

    void CreateTerrainCorners()
    {
        terrainSpriteShapeController.spline.InsertPointAt(terrainSpriteShapeController.spline.GetPointCount(), new Vector2(lineSpriteShapeController.spline.GetPosition(lineSpriteShapeController.spline.GetPointCount() - 1).x, -TerrainHeight)); //Aggiunge un punto nell'ultima posizione uguale al punto in basso a destra della telecamera
        terrainSpriteShapeController.spline.InsertPointAt(terrainSpriteShapeController.spline.GetPointCount(), new Vector2(lineSpriteShapeController.spline.GetPosition(0).x, -TerrainHeight)); //Aggiunge un altro punto nella nuova ultima posizione uguale al punto in basso a sinistra della telecamera
    }

    void UpdateTerrainCorners()
    {
        terrainSpriteShapeController.spline.SetPosition(terrainSpriteShapeController.spline.GetPointCount() - 2, new Vector2(terrainSpriteShapeController.spline.GetPosition(terrainSpriteShapeController.spline.GetPointCount() - 3).x, -TerrainHeight)); //Aggiorna il penultimo punto con la poszione attuale del punto in basso a destra della telecamera
        terrainSpriteShapeController.spline.SetPosition(terrainSpriteShapeController.spline.GetPointCount() - 1, new Vector2(terrainSpriteShapeController.spline.GetPosition(0).x, -TerrainHeight)); //Aggiorna l'ultimo punto con la poszione attuale del punto in basso a sinistra della telecamera
    }

    void UpdateWall()
    {
        Wall.transform.position = lineSpriteShapeController.spline.GetPosition(KeepDistance - 1) / 5;
    }
}