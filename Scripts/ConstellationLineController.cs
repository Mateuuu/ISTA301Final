using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstellationLineController : MonoBehaviour
{
    [SerializeField] LayerMask starLayer;

    GameObject selectedStar;


    Color32 selectedColor = new Color32(255, 102, 173, 255);


    [SerializeField] LineRenderer constellationLinePrefab;
    [SerializeField] Transform sun;

    List<LineRenderer> constellationLines = new();


    [SerializeField] GameObject selectedStarIndicator;


    bool day = true;
    bool night = false;

    private void OnEnable()
    {
        TimeController.Daytime += Daytime;
        TimeController.Nighttime += Nighttime;
    }

    private void OnDisable()
    {
        TimeController.Daytime -= Daytime;
        TimeController.Nighttime -= Nighttime;
    }
    private void Daytime()
    {
        selectedStarIndicator.SetActive(false);
        selectedStarIndicator.transform.localPosition = new Vector3(5000, 0, 0);
        selectedStar = null;
        day = true;
        night = false;

        foreach (LineRenderer line in constellationLines)
        {
            Destroy(line.gameObject);
        }

        constellationLines.Clear();
    }


    private void Nighttime()
    {
        selectedStarIndicator.SetActive(true);
        night = true;
        day = false;
    }


    void Update()
    {

        if (night && Input.GetKeyDown(KeyCode.Mouse0))
        {
            SelectStar();
        }

    }

    void SelectStar()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000, starLayer))
        {
            if(hit.transform.gameObject == selectedStar)
            {
                selectedStarIndicator.SetActive(false);

                selectedStar = null;

                return;
            }
            selectedStarIndicator.SetActive(true);

            LineRenderer constellationLine = Instantiate(constellationLinePrefab, sun);
            constellationLines.Add(constellationLine);
            constellationLine.positionCount = 2;

            Vector3[] vertexPositions = new Vector3[2];


            if(selectedStar != null)
            {
                vertexPositions[0] = selectedStar.transform.localPosition;
            }

            selectedStar = hit.transform.gameObject;

            if(vertexPositions[0] != Vector3.zero)
            {
                vertexPositions[1] = selectedStar.transform.localPosition;
            }


            constellationLine.SetPositions(vertexPositions);
            selectedStarIndicator.transform.localPosition = selectedStar.transform.localPosition;

        }
    }


}
