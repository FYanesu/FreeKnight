using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCreate : MonoBehaviour
{
    public enum Direction { up, down, left, right};
    public Direction direction;

    public GameObject miniRoomPrefab;
    private GameObject endroom;
    public GameObject miniSafePrefab;
    public GameObject miniEndPrefab;
    public GameObject miniSpecialPrefab;
    public GameObject safePrefab;
    public GameObject endPrefab;
    public GameObject freePrefab;
    public GameObject specialPrefab;
    public List<GameObject> normalPrefabs = new List<GameObject>();

    public List<GameObject> monsterPrefabs = new List<GameObject>();
    private float randomMonsterN;
    public int monsterNumber;
    public float[] rMWeight = {4000,4000,4000,4000,1000,1000,1000,1000};
    private float randomX;
    private float randomY;
    private Vector3 randomBorn;

    public int roomNumber;
    public int endRoomN;
    public int specialRoomN;
    public int freeRoomN;
    private int randomNormalN;

    
    public Transform generatorPoint;
    public float XOffset;
    public float YOffset;
    public LayerMask roomLayer;
    public LayerMask frontLayer;

    private string miniRoom;
    private bool miniU,miniL,miniD,miniR;
    private Sprite miniSprite;

    public List<GameObject> rooms = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < roomNumber; i++)
        {
            rooms.Add(Instantiate(miniRoomPrefab, generatorPoint.position, Quaternion.identity));
            ChangePoint();
        }
        //生成小地图-初始房间
        Instantiate(miniSafePrefab, rooms[0].transform.position, Quaternion.identity);
        //生成小地图-最终房间 并标记
        endroom = rooms[0]; 
        for(int i = 0; i < roomNumber; i++)
        {
            if(rooms[i].transform.position.sqrMagnitude > endroom.transform.position.sqrMagnitude)
            {
                endroom = rooms[i];
                endRoomN = i;
            }
            SetupDoor(rooms[i],rooms[i].transform.position);
            miniRoom = "minimap";
            if(miniU)
            miniRoom += "U";
            if(miniL)
            miniRoom += "L";
            if(miniD)
            miniRoom += "D";
            if(miniR)
            miniRoom += "R";
            miniSprite = Resources.Load<Sprite>(miniRoom);
            rooms[i].transform.GetChild(7).gameObject.GetComponent<SpriteRenderer>().sprite = miniSprite;
            if(i == roomNumber-1)
            {
                rooms[endRoomN].transform.GetChild(5).GetComponent<DoorOpenCheck>().isEndroom = true;
            }
        }
        //Instantiate(miniEndPrefab, endroom.transform.position, Quaternion.identity);

        //生成小地图-特殊房间 并标记
        do{specialRoomN = Random.Range(1, roomNumber);}while(specialRoomN==endRoomN);
        do{freeRoomN = Random.Range(1, roomNumber);}while(freeRoomN==specialRoomN || freeRoomN==endRoomN);
        rooms[specialRoomN].transform.GetChild(5).GetComponent<DoorOpenCheck>().isSpecial = true;
        rooms[freeRoomN].transform.GetChild(5).GetComponent<DoorOpenCheck>().isSpecial = true;        
        //Instantiate(miniSpecialPrefab, rooms[specialRoomN].transform.position, Quaternion.identity);
        //Instantiate(miniSpecialPrefab, rooms[freeRoomN].transform.position, Quaternion.identity);
        //生成实体地图
        Instantiate(safePrefab, rooms[0].transform.position, Quaternion.identity);
        Instantiate(endPrefab, rooms[endRoomN].transform.position, Quaternion.identity);
        Instantiate(freePrefab, rooms[freeRoomN].transform.position, Quaternion.identity);
        Instantiate(specialPrefab, rooms[specialRoomN].transform.position, Quaternion.identity);
        for(int i = 1; i < roomNumber; i++)
        {
            if(i!=endRoomN && i!=freeRoomN && i!=specialRoomN)
            {
                randomNormalN = Random.Range(0, 6);
                Instantiate(normalPrefabs[randomNormalN], rooms[i].transform.position, Quaternion.identity);
            }
        }
        //生成怪物
        for(int i = 1; i < roomNumber; i++)
        {
            if(i!=endRoomN && i!=freeRoomN && i!=specialRoomN)
            {
                for (int j = 0; j < monsterNumber; j++)
                {
                    do{
                    randomX = Random.Range(-3f, 3f);
                    randomY = Random.Range(-1f, 1f);
                    randomBorn = new Vector3(randomX,randomY,0f);
                    randomBorn = rooms[i].transform.position + randomBorn;
                    }while (Physics2D.OverlapCircle(randomBorn, 0.6f, frontLayer));

                    randomMonsterN = Random.Range(0,rMWeight[0] + rMWeight[1] + rMWeight[2] + rMWeight[3] + rMWeight[4] + rMWeight[5] + rMWeight[6] + rMWeight[7]);
                    Instantiate(monsterPrefabs[RandomMonster(randomMonsterN)], randomBorn, Quaternion.identity);
                }
                
            }
        }
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePoint()
    {
        do{
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    generatorPoint.position += new Vector3(0, YOffset, 0);
                    break;
                case Direction.down:
                    generatorPoint.position += new Vector3(0, -YOffset, 0);
                    break;
                case Direction.left:
                    generatorPoint.position += new Vector3(-XOffset, 0, 0);
                    break;
                case Direction.right:
                    generatorPoint.position += new Vector3(XOffset, 0, 0);
                    break;
            }
        }while (Physics2D.OverlapCircle(generatorPoint.position, 2f, roomLayer));
        
    }

    public void SetupDoor(GameObject newRoom, Vector3 roomPosition)
    {
        miniU = Physics2D.OverlapCircle(roomPosition + new Vector3(0,YOffset,0), 0.2f ,roomLayer);
        newRoom.GetComponent<DoorCreat>().RoomUp = miniU;

        miniD = Physics2D.OverlapCircle(roomPosition + new Vector3(0,-YOffset,0), 0.2f ,roomLayer);
        newRoom.GetComponent<DoorCreat>().RoomDown = miniD;

        miniL = Physics2D.OverlapCircle(roomPosition + new Vector3(-XOffset,0,0), 0.2f ,roomLayer);
        newRoom.GetComponent<DoorCreat>().RoomLeft = miniL;

        miniR = Physics2D.OverlapCircle(roomPosition + new Vector3(XOffset,0,0), 0.2f ,roomLayer);
        newRoom.GetComponent<DoorCreat>().RoomRight = miniR;
    }

    public int RandomMonster(float randomWeight)
    {
        float L=0,R=rMWeight[0];
        for(int i=0;i<8;i++)
        {
            if(randomWeight < R && randomWeight >= L)
            return i;
            L += rMWeight[i];
            R += rMWeight[i+1];
        }
        return 0;
    }
}
