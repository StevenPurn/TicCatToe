using UnityEngine;
using System.Collections;

public class PinchToZoom : MonoBehaviour {

    private float panSpeed = 0.75f;
    private float zoomSpeed = 0.2f;
    private float minX, maxX, maxY, minY;
    public float boardWidth = 20.0f;
    public float boardHeight = 50.0f;

    void Update()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            Camera camera = GetComponent<Camera>();

            // ... change the orthographic size based on the change in distance between the touches.
            camera.orthographicSize += deltaMagnitudeDiff * zoomSpeed;

            // Make sure the orthographic size never drops below zero.
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, 3.0f, 6.4f);
            CalcMinMax(camera);
        }else if(Input.touchCount == 1)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            Touch touchZero = Input.GetTouch(0);
            if (touchZero.phase == TouchPhase.Moved)
            {
                Vector3 pos = transform.position;
                pos.x -= touchDeltaPosition.x * panSpeed * Time.deltaTime;
                pos.y -= touchDeltaPosition.y * panSpeed * Time.deltaTime;
                pos.x = Mathf.Clamp(pos.x, minX, maxX);
                pos.y = Mathf.Clamp(pos.y, minY, maxY);
                transform.position = pos;
                CalcMinMax(GetComponent<Camera>());
            }
        }
    }

    void CalcMinMax(Camera camera)
    {
        float height = camera.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        maxX = (boardWidth - width) / 2.0f;
        minX = -maxX;
        maxY = (boardHeight - height) / 2.0f;
        minY = -maxY;
    }
}
