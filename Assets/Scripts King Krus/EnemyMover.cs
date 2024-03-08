using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTag))]
public class EnemyMover : MonoBehaviour
{    
    [SerializeField] [Range (0f, 5f)] float speed = 1f; //Limitiamo la velocità perchè valori negativi darebbero problemi all'equazione e troppo veloce sarebbe brutto

    List<Node> path = new List<Node>();

    PathFinder pathFinder;
    GridManager gridManager;
    EnemyTag enemy;

    void OnEnable()
    {
        ReturntoStart();
        RecalculatePath(true);
    }
    void Awake()
    {
        enemy = GetComponent<EnemyTag>();
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath)
        {
            coordinates = pathFinder.StartCoord;
        }
        else 
        {
         coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturntoStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoord);
    }

    void FinishPath()
    {
        enemy.Steal();
        gameObject.SetActive(false);
    }
    IEnumerator FollowPath() //IEnum per ottenere risultati conteggiabili, ma ha bisogno di un Return per avere il risultato
    {
        for(int i = 1; i < path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition); //per ruotare la pedina in direzione del movimento

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame(); //con Yield viene fatta riniziare questa fase in modo ciclico fino alla risoluzione del movimento
            }  
        }
        FinishPath();
    }
}
