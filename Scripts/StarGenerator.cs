using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;
    [SerializeField] uint numStars;

    [SerializeField] float minSize = 1;
    [SerializeField] float maxSize = 1;

    [SerializeField] float distance = 200;

    [SerializeField] Material starMat;

    [SerializeField] Transform sun;

    void Start()
    {


        for(int i = 0; i < numStars; i++)
        {
            Vector3 spawnPosition = Random.onUnitSphere * distance;
            Transform star = Instantiate(starPrefab, spawnPosition, Quaternion.identity, sun).transform;


            float size = Random.Range(minSize, maxSize);

            star.localScale = new Vector3(size, size, size);
        }

        starMat.SetFloat("_Opacity", 0f);

    }


}
