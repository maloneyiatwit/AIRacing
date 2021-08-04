using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int straightMax;
    public int leftMax;
    public int rightMax;
    public int maxTiles;
    public float tileDist;



    public GameObject[] tiles;
    public GameObject start;
    public GameObject end;
    private GameObject last;



    private int straightCount;
    private int leftCount;
    private int rightCount;

    private int leftRun;

    private int rightRun;


    private float currTileX;
    private float currTileZ;
    private float rotation;


    private int currTileType = 0;
    private int tileCount = 0;
    private int tileToGenerate = 0;
    private bool lastT = false;

    private int turnMax = 2;

    private int[] validChoices = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        currTileX = 0;
        currTileZ = 0;
        rotation = 0;

        validChoices[0] = 0;
        validChoices[1] = 2;

        LoadMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadMap(){
      
        last = Instantiate(start, new Vector3(currTileX, 0.0f, currTileZ), Quaternion.identity);
        tileCount = 1;

        straightCount = 0;
        leftCount = 0;
        rightCount = 0;

        leftRun = 0;
        rightRun = 0;

        GenerateStraight();
        
        while(tileCount != maxTiles){
            GenerateTile();
        }
        
        tileToGenerate =4;
        RotationCheck();

        last = Instantiate(end, new Vector3(currTileX, 0.0f, currTileZ), Quaternion.Euler(0.0f,rotation,0.0f));
        
    }

    void GenerateTile(){

        if(lastT == true){
            
            tileToGenerate = 0;

            GenerateStraight();

        }

        else{

            if(straightCount == straightMax){

                if(leftRun == turnMax){
                    GenerateRight();
                }
                else if(rightRun == turnMax){
                    GenerateLeft();
                }
                else{
                    tileToGenerate = Random.Range(1,3);

                    if(tileToGenerate == 1){

                        GenerateLeft();

                    }

                    else{

                        GenerateRight();

                    }
                }

            }

            else if (rightRun == turnMax){

                tileToGenerate = Random.Range(0,2);

                if(tileToGenerate == 0){

                    GenerateStraight();

                }
                
                else{

                    GenerateLeft();

                }


            }

            else if (leftRun == turnMax){

                tileToGenerate = GetRandom();

                if(tileToGenerate == 0){

                    GenerateStraight();

                }
                
                else {

                    GenerateRight();

                }

            }

            else{

                tileToGenerate = Random.Range(0,3);

                if(tileToGenerate == 0){

                    GenerateStraight();

                }
                
                else if(tileToGenerate == 1){

                    GenerateLeft();

                }

                else{

                    GenerateRight();

                }
            }

        }
        
    }

    void GenerateStraight(){
    
        for(int i = 0; i <= Random.Range(1,(straightMax-straightCount)+1);i++){

            if(tileCount == maxTiles){
                break;
            }
            RotationCheck();
            
            
            last = Instantiate(tiles[tileToGenerate], new Vector3(currTileX, 0.0f, currTileZ), Quaternion.Euler(0.0f,rotation,0.0f));
            straightCount += 1;
                
            Debug.Log("S: " + i);
            tileCount += 1;

        }
    
        lastT = false;
        currTileType = 0;

        
    }

    void GenerateLeft(){

        if(leftCount == leftMax){

            GenerateTile();

        }
        else{
            
            RotationCheck();

            last = Instantiate(tiles[tileToGenerate], new Vector3(currTileX, 0.0f, currTileZ), Quaternion.Euler(0.0f,rotation,0.0f));
            
            Debug.Log("L");
            tileCount += 1;

            leftRun += 1;
            rightRun = 0;
            straightCount = 0;

            lastT = true;

            currTileType = 0;

        }


    }

    void GenerateRight(){

        if(rightCount == rightMax){

            GenerateTile();

        }

        else{


            RotationCheck();

            last = Instantiate(tiles[tileToGenerate], new Vector3(currTileX, 0.0f, currTileZ), Quaternion.Euler(0.0f,rotation,0.0f));
            Debug.Log("R");
            tileCount += 1;

            rightRun += 1;
            leftRun = 0;

            straightCount = 0;

            lastT = true;

            currTileType = 0;

        }
    }

    void RotationCheck(){
        
        if(tileToGenerate == 0 || tileToGenerate == 4){

            if ( last.tag == "Left"){

                    rotation =  rotation + 270;
                    Debug.Log(last.transform.rotation.y);
                    Debug.Log(rotation);

                    if(rotation > 360){
                        rotation = rotation % 360;
                    }

                    if(rotation % 360 == 0 || rotation == 0){
                        currTileX += 0;
                        currTileZ += tileDist;
                    }
                    else if(rotation % 270 == 0){
                        currTileX -= tileDist;
                        currTileZ += 0;
                    }
                    else if(rotation % 180 == 0){
                        currTileX += 0;
                        currTileZ -= tileDist;
                    }
                    else {
                        currTileX += tileDist;
                        currTileZ += 0;
                    }

                    
                }
            else if( last.tag == "Right"){
                    
                    rotation = rotation + 90;
                    Debug.Log(last.transform.rotation.y);
                    Debug.Log(rotation);

                    if(rotation > 360){
                        rotation = rotation % 360;
                    }

                    if(rotation % 360 == 0  ||rotation == 0){
                        currTileX += 0;
                        currTileZ += tileDist;
                    }
                    else if(rotation % 270 == 0){
                        currTileX -= tileDist;
                        currTileZ += 0;
                    }
                    else if(rotation % 180 == 0){
                        currTileX += 0;
                        currTileZ -= tileDist;
                    }
                    else {
                        currTileX += tileDist;
                        currTileZ += 0;
                    }
                    
                }
            else{

                 if(rotation % 360 == 0 || rotation == 0){
                        currTileX += 0;
                        currTileZ += tileDist;
                    }
                    else if(rotation % 270 == 0){
                        currTileX -= tileDist;
                        currTileZ += 0;
                    }
                    else if(rotation % 180 == 0){
                        currTileX += 0;
                        currTileZ -= tileDist;
                    }
                    else {
                        currTileX += tileDist;
                        currTileZ += 0;
                    }
                    
                    

                }

           

        }

        else if(tileToGenerate == 1){

            if(rotation % 360 == 0 || rotation == 0){

                currTileX += 0;
                currTileZ += tileDist;

            }
            else if(rotation % 270 == 0){

                currTileX -= tileDist;
                currTileZ += 0;

            }
            else if(rotation % 180 == 0){

                currTileX += 0;
                currTileZ -= tileDist;

            }
            else {

                currTileX += tileDist;
                currTileZ += 0;

            }


        }

        else{

            if(rotation % 360 == 0 || rotation == 0){

                currTileX += 0;
                currTileZ += tileDist;

            }
            else if(rotation % 270 == 0){

                currTileX -= tileDist;
                currTileZ += 0;

            }
            else if(rotation % 180 == 0){

                currTileX += 0;
                currTileZ -= tileDist;

            }
            else {

                currTileX += tileDist;
                currTileZ += 0;
                
            }


        }

    
        

        
    }

    private int GetRandom(){
        return validChoices[Random.Range(0, validChoices.Length)];
    }
}
