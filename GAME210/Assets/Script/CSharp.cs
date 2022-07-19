using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharp : MonoBehaviour
{
    int[] myIntegerArray = { 1, 2, 2, 4, 5 };
    int[,] myIdentity = new int[3, 3];

    int[,] matrixOne;
    int[,] matrixTwo;

    private void Start()
    {

        myIdentity = new int[3, 3]{ { 1,0,0},
                                   { 0,1,0},
                                   { 0,0,1}};

        matrixOne = new int[2, 3] { { 1, 2, 3 },
                                    { 4, 5, 6 } };

        matrixTwo = new int[3, 2] { { 10, 11 },
                                    { 20, 21 },
                                    { 30, 31 } };

        //isIdentityMatrix(myIdentity);

        PerformMatrixMultiplication(matrixOne, matrixTwo);

    }

    int SumArray(int[] Array)
    {
        int sum = 0;
        int i = 0;

        while (i < Array.Length)
        {
            sum += Array[i];
            i++;
        }

        return sum;
    }

    int SumArrayRecursively(int i = 0)
    {
        if (i == myIntegerArray.Length)
        {
            return 0;
        }
        return SumArrayRecursively(i++) + myIntegerArray[i + 1];
    }

    int CountOdd(int[] array, int sum, int odd)
    {

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] % 2 == 0)
            {
                sum++;
            }
            else
            {
                odd++;
            }
        }
        Debug.Log("total evens: " + sum + "\n" + "total odds: " + odd);

        return 0;
    }

    bool CheckDuplicate(int[] array)
    {
        bool condition = false;
        int[] storedArray = new int[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            int count = 0;
            storedArray[i] = array[i];

            for (int c = 0; c < array.Length; c++)
            {
                if (array[i] == storedArray[c])
                {
                    count++;
                }
                if (count == 2)
                {
                    condition = true;
                }
            }
        }
        return condition;
    }

    private bool isIdentityMatrix(int[,] matrix)
    {
        bool condition = false;

        if (matrix.GetLength(0) != matrix.GetLength(1))
        {
            //if matrix is not a square matrix stop here
            return false;
        }
        else
        {
            for (int i = 0; matrix.GetLength(0) > i; i++)
            {
                for (int c = 0; matrix.GetLength(1) > c; c++)
                {
                    if (i == c && matrix[i, c] != 1 || i != c && matrix[i, c] != 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    //check if function is scaling or translating matrix.

    private void PerformMatrixMultiplication(int[,] matOne, int[,] matTwo)
    {
        int matOneRows = matOne.GetLength(0);
        int matOneCol = matOne.GetLength(1);
        int matTwoRows = matTwo.GetLength(0);
        int matTwoCols = matTwo.GetLength(1);

        if (matOneCol != matTwoRows)
        {
            Debug.Log("Matrix Inner dimenesions not matching");
            return;
        }

        int[,] matResult = new int[matOneRows, matTwoCols];

        for (int r = 0; r < matOneRows; r++)
        {
            for (int c = 0; c < matTwoCols; c++)
            {

                int sumSum = 0;

                for (int k = 0; k < matTwoRows; k++)
                {
                    sumSum += matOne[r, k] * matTwo[k, c];

                    matResult[r, c] = sumSum;
                }
            }
        }
        Debug.Log("The Matrix is");
        for (int r = 0; r < matOneRows; r++)
        {
            string matrix = "";

            for (int c = 0; c < matTwoCols; c++)
            {
                matrix = matResult[r, c] + ",";
            }
            Debug.Log(matrix);
        }
    }
}
