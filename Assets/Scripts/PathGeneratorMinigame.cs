using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public GameObject blockedPathPrefab;  // Prefab for blocked path
    public GameObject walkthroughPathPrefab;  // Prefab for walkthrough path
    public GameObject jumpPathPrefab;  // Prefab for jump-through path

    public int numberOfPaths = 10;  // Number of paths to generate

    void Start()
    {
        GeneratePaths();
    }

    void GeneratePaths()
    {
        for (int i = 0; i < numberOfPaths; i++)
        {
            // Create a list to represent the current path
            var currentPath = new int[3];

            // Decide whether to have one or two elements
            int numTotal = Random.Range(1, 3);  // Either 1 or 2 total elements

            // Determine the type of element (walkthrough or jump-through)
            int elementType = Random.Range(2, 4);  // 2 represents walkthrough, 3 represents jump-through

            for (int j = 0; j < numTotal; j++)
            {
                int randomIndex = Random.Range(0, 3);

                // Only place the specified element if the position is not already occupied
                if (currentPath[randomIndex] == 0)
                {
                    currentPath[randomIndex] = elementType;
                }
            }

            // If no specific element is chosen, place a blocked path
            for (int j = 0; j < 3; j++)
            {
                if (currentPath[j] == 0)
                {
                    currentPath[j] = 1;  // 1 represents blocked path
                }
            }

            // Instantiate the path based on the generated pattern
            InstantiatePath(currentPath, i);
        }
    }

    void InstantiatePath(int[] pathPattern, int pathIndex)
    {
        for (int i = 0; i < pathPattern.Length; i++)
        {
            Vector3 position = new Vector3(i * 5f, 1, pathIndex * 18);  // Adjust the positions as needed
            Quaternion rotation = Quaternion.identity;

            switch (pathPattern[i])
            {
                case 1:
                    Instantiate(blockedPathPrefab, position, rotation);
                    break;
                case 2:
                    Instantiate(walkthroughPathPrefab, position, rotation);
                    break;
                case 3:
                    Instantiate(jumpPathPrefab, position, rotation);
                    break;
            }
        }
    }
}



