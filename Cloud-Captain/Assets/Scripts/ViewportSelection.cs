using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewportSelection : MonoBehaviour
{
    public static ViewportSelection Instance { get; private set; }
    private static Texture2D pixelTexture;


    public Color SelectionBoxColor;

    private Vector3 startMousePos;
    private bool isDragging = false;
    private Bounds mouseBounds;

    private Dictionary<GameObject, GameObject> selectedGameObjects = new Dictionary<GameObject, GameObject>();
    
    void Start ()
    {
        pixelTexture = new Texture2D(1, 1);
        pixelTexture.SetPixel(0, 0, Color.white);

        Instance = this;
    }
	

    void Update()
    {
        if (isDragging)
        {
            if (Input.GetMouseButtonUp(0))
                EndSelection();
       
            else
                mouseBounds = GetViewportBounds(Camera.main, Input.mousePosition, startMousePos);
        }

        else if (Input.GetMouseButtonDown(0))
            StartSelection();
    }

    void OnGUI()
    {
        if (isDragging)
        {
            Vector3 curMouse = Input.mousePosition;
            DrawScreenRectBorder(GetScreenRect(curMouse, startMousePos), 2.5f, SelectionBoxColor);
        }
    }

    void StartSelection()
    {
        selectedGameObjects.Clear();
        startMousePos = Input.mousePosition;
        isDragging = true;
    }

    void EndSelection()
    {
        isDragging = false;
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isDragging)
            return false;

        return mouseBounds.Contains(Camera.main.WorldToViewportPoint(gameObject.transform.position));
    }


    public void Notify(GameObject obj)
    {
        if (IsWithinSelectionBounds(obj) && !selectedGameObjects.ContainsKey(obj))
        {
            selectedGameObjects.Add(obj, obj);           
        }
    }


    #region

    public static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
    {
        // Move origin from bottom left to top left
        screenPosition1.y = Screen.height - screenPosition1.y;
        screenPosition2.y = Screen.height - screenPosition2.y;
        // Calculate corners
        var topLeft = Vector3.Min(screenPosition1, screenPosition2);
        var bottomRight = Vector3.Max(screenPosition1, screenPosition2);
        // Create Rect
        return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
    }

    public static void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, pixelTexture);
        GUI.color = Color.white;
    }

    public static void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // Top
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
        // Left
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
        // Right
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
        // Bottom
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
    }

    public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
    {
        var v1 = Camera.main.ScreenToViewportPoint(screenPosition1);
        var v2 = Camera.main.ScreenToViewportPoint(screenPosition2);
        var min = Vector3.Min(v1, v2);
        var max = Vector3.Max(v1, v2);
        min.z = camera.nearClipPlane;
        max.z = camera.farClipPlane;

        var bounds = new Bounds();
        bounds.SetMinMax(min, max);
        return bounds;
    }


    #endregion
}
