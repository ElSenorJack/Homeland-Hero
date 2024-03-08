using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.red;
    [SerializeField] Color exploredColor = Color.yellow;
    [SerializeField] Color pathColor = Color.blue;
   
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;

    void Awake() //awake è il primissimo ad attivarsi, prima di Start
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates();
    }
    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }
        SetLabelColor();
        ToggleLabels();
    }
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }

    void SetLabelColor()
    {
        if(gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if(!node.isWalkable)
        {
            label.color = blockedColor;
        }
        else if(node.isPath)
        {
            label.color = pathColor;
        }
        else if(node.isExplored)
        {
            label.color = exploredColor;
        }
        else
        {
            label.color = defaultColor;
        }
    }
    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        // vvv richiama le impostazioni selezionate della griglia 
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);
        //RoundtoInt converte il dato per renderlo leggibile
        label.text = coordinates.x + "," + coordinates.y;
    }
    void UpdateObjectName() //per cambiare il nome dell'oggetto e avere la hierarchy più in ordine
    {
        transform.parent.name = coordinates.ToString();
    }
}
