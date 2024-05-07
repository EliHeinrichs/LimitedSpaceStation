using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public class Count
    {
        public int minimum;// minimum value for count class
        public int maximum;//maximum value for count class

        //count assignment constructor,can declare and change min/max in one line
        public Count ( int min , int max )
        {
            minimum = min; // change minimum and maximum based on parameters
            maximum = max;
        }

    }

    public int columns = 12;// number of columns
    public int rows = 12;// number of rows
    public Count wallCount = new Count(7, 14); // generated random number of walls
    public Count foodCount = new Count(0,4); // generated random number of food in level
   
    public GameObject exit; // declare what the exit gameObject is in editor
    public GameObject[] floorTiles; // array for declaring floorTiles gameObjects
    public GameObject[] wallTiles; // array for declaring walltiles
    public GameObject[] foodTiles; // array for declaring food tiles
    public GameObject[] enemyTiles; // array for declaring enemy tiles
    public GameObject[] outerwallTiles; // array for declaring outerwalls
 
    public GameObject playerSpawn; //Player  

    private Transform boardHolder; //Empty that will contain all board objects
    private List<Vector3> gridPositions = new List<Vector3>();






    public void SetUpScene ( int level )
    {
        // determines enemies based on current level with logarithmic progression
        int enemyCount = Random.Range( 0 , 6);

        BoardSetup ();
        InitialiseList ();
        //instantiate random number of enemies based min max at randomized positions


        if ( GameManager . instance . room >= GameManager . instance . finalLvl )
        {
            //resests player position to random tile on the first row and nothing more
            playerSpawn . transform . position = new Vector3 (Random . Range (columns - 9 , columns - 1) , rows - rows , 0);


        }
        else
        {
            //resests player position to random tile on the first row
            playerSpawn . transform . position = new Vector3 (Random . Range (columns - 9 , columns - 1) , rows - rows , 0);


            if ( GameManager . instance . uniqueRoom == true )
            {

                //Spawns the exit in the last row at a random column between -9 and -1
                Instantiate (exit , new Vector3 (Random . Range (columns - 9 , columns - 1) , rows - 1 , 0f) , Quaternion . identity);

                //Performs func that spawns all the wall and food tiles aroudn the room based on the randomly determined max and min
                LayoutObjectAtRandom (wallTiles , wallCount . minimum , wallCount . maximum);
                LayoutObjectAtRandom (foodTiles , foodCount . minimum , foodCount . maximum);

                //This will not spawn enemies as it is a unique room which means their will be a different threat

            }
            else
            {
                //Spawns the exit in the last row at a random column between -9 and -1
                Instantiate (exit , new Vector3 (Random . Range (columns - 9 , columns - 1) , rows - 1 , 0f) , Quaternion . identity);

                //Performs func that spawns all the wall and food tiles aroudn the room based on the randomly determined max and min
                LayoutObjectAtRandom (wallTiles , wallCount . minimum , wallCount . maximum);
                LayoutObjectAtRandom (foodTiles , foodCount . minimum , foodCount . maximum);

                // Spawns enemies based on randomly determined amount in start
                LayoutObjectAtRandom (enemyTiles , enemyCount , enemyCount);

            }






        }
    }
    void InitialiseList ( )
    {
        
        gridPositions . Clear (); // clears previous list positions 

        // loop the x axis columns stops at 6 times (columns - 1) 0 doesnt count as that is the border of the board
        for ( int x = 1 ; x < columns - 1 ; x++ ) // declares x value and if the x value is less than the columns it adds to x and proceeds
        {

            // loop the x axis columns stops at 6 times (columns - 1) 0 doesnt count as that is the border of the board
            for ( int y = 1 ; y < rows - 1 ; y++ )   // declares y value and if the y value is less than the amount of required rows it adds to the y value and proceeds
            {

                gridPositions . Add (new Vector3 (x , y , 0f)); // adds new grid positions to be filled based on the x and y values at each index of the positions
            }
        }
    }

    void BoardSetup ( )
    {
    
        boardHolder = new GameObject ("Board") . transform;

        // finds outer edge of x
        for ( int x = -1 ; x < columns + 1 ; x++ )
        {
            // determine if tile is an outer wall or inside as a floor
            for ( int y = -1 ; y < rows + 1 ; y++ )
            {// choose random tile from array index to prep and instantiate
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                // check if we have a board edge position, if so choose a random outer wall to instantiate
                if ( x == -1 || x == columns || y == -1 || y == rows )
                {// choose random tile to instantiate in the outerwall index
                    toInstantiate = outerwallTiles [ Random . Range (0 , outerwallTiles . Length) ];
                    
                }
                // instantiates game objects
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),  Quaternion . Euler ( 0, 0 , Random . Range (0 , 4) * 90)) as GameObject;
                // sets new parent
                instance . transform . SetParent (boardHolder);
            }
        }
    }




    Vector3 RandomPosition ( )
    {
        //declare and in and set it to a random spot between 0 and the max grid positions
        int randomIndex = Random.Range(0, gridPositions.Count);
        //declare a vector3 var and assign it one the gridpositions List values randomly
        Vector3 randomPosition = gridPositions[randomIndex];
        // Remove the entry at RandomIndex from the list gridpositions so that it cant be reused
        gridPositions . RemoveAt (randomIndex);
        return randomPosition;
    }

    // accepts an array of gameobjects and then chooses along between the min and max number
    void LayoutObjectAtRandom ( GameObject [ ] tileArray , int minimum , int maximum )
    {
        // chooses number of objects to spawn between min and max
        int objectCount = Random.Range(minimum, maximum + 1);

        for ( int i = 0 ; i < objectCount ; i++ )
        {
            // choose a position to be a randm position
            Vector3 randomPosition = RandomPosition();
            // choose a random tile to be placed
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            //instantiate the random tile we chose to the random position we chose
            Instantiate (tileChoice , randomPosition , Quaternion . identity);
        }

    }

}
