using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    int[] myArray;

    // Start is called before the first frame update
    void Start()
    {
        myArray = new int[5] { 55, 92, 33, 674, 55 };
        FindSecondLargestInArray(myArray);
    }

    private void FindSecondLargestInArray(int[] array)
    {
        int largestNumber = 0;
        int secondLargestNumber = 0;

        for(int i = 0; i < myArray.Length; i++)
        {
            for (int x = 0; x < myArray.Length; x++)
            {
                if (myArray[x] > largestNumber)
                {
                    largestNumber = myArray[x];
                }

                if(myArray[i] > myArray[x] && myArray[i] < largestNumber && i != x && myArray[i] > secondLargestNumber)
                {
                    secondLargestNumber = myArray[i];
                }
            }
        }
        Debug.Log(secondLargestNumber);
    }
}
