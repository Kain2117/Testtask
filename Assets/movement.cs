using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private List<Vector3> _moveList = new();
    private bool _istand = false;
    [SerializeField] private float _spid = 2f;
    private LineRenderer _lineRenderer;
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.sortingOrder = -1;
    }
   
   private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _moveList.Add((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
            DrawLines(_moveList);
        }
        if (_istand)
        {
            if  ((Vector2)transform.position ==(Vector2) _moveList[0])
            {
                _istand = false;
                _moveList.RemoveAt(0);
                DrawLines(_moveList);
            }
            else
            {
                transform.position = Vector2.MoveTowards((Vector2)transform.position, _moveList[0], _spid * Time.deltaTime);
            }
        }
        else
        {
            if (_moveList.Count > 0)
            {
                _istand = true;
            }
        }
    }
    private void DrawLines(List<Vector3> list)
    {
        list.Insert(0, transform.position);
        _lineRenderer.positionCount = list.Count;
        _lineRenderer.SetPositions(list.ToArray());
        list.RemoveAt(0);
    }
}
