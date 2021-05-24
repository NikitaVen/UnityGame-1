using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{

    [SerializeField]
    private Tile groundTile;
    [SerializeField]
    private Tile pitTile;
    [SerializeField]
    private Tile topWallTile;
    [SerializeField]
    private Tile botWallTile;
    [SerializeField]
    private Tile shadowTile;
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;


    public Tile exit;
    bool exitspawned = false;
    public GameObject exitPortalPref;
    public GameObject enemyPref;
    public GameObject ammo;
    public float ammoChance = 20f;
    public GameObject points;

    private int routeCount = 0;

    private void Start()
    {
        int x = 0;
        int y = 0;
        int routeLength = 0;
        GenerateSquare(x, y, 1);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;
        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        FillWalls();
    }


    private void GenerateExit(int x, int y)
    {
        Vector3Int tilePos = new Vector3Int(x, y, 0);
        Instantiate(exitPortalPref, tilePos, Quaternion.identity);
        groundMap.SetTile(tilePos, topWallTile);
    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;
        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posLeft = new Vector3Int(xMap - 1, yMap, 0);
                Vector3Int posRight = new Vector3Int(xMap + 1, yMap, 0);
                TileBase tile = groundMap.GetTile(pos);
                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileAbove = groundMap.GetTile(posAbove);

                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);
                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, topWallTile);
                        groundMap.SetTile(posBelow, shadowTile);


                        if (!exitspawned)
                        {
                            wallMap.SetTile(pos, topWallTile);
                            GenerateSquare(pos.x, pos.y, 1);
                            GenerateExit(pos.x, pos.y);
                            exitspawned = true;
                        }


                    }
                    else if (tileAbove != null)
                    {

                        wallMap.SetTile(pos, botWallTile);
                    }
                }
            }
        }
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 1; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                {
                    roomSize = Random.Range(3, 6);

                    var point = new Vector3(previousPos.x, previousPos.y, 0);

                    if (Mathf.Sqrt(Mathf.Pow((point.x + player.transform.position.x), 2) + Mathf.Pow((point.y + player.transform.position.y), 2)) >= 10)
                        Instantiate(enemyPref, point, Quaternion.identity);
                    if (Random.Range(1, 100) <= ammoChance)
                        Instantiate(ammo, new Vector3(previousPos.x, previousPos.y, 0), Quaternion.identity);
                }

                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate)
                {


                    Instantiate(points, new Vector3(previousPos.x, previousPos.y, 0), Quaternion.identity);

                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {


        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {

            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, groundTile);
            }
        }
    }
}