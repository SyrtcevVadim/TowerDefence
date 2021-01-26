using UnityEngine;

public class ObserverZoom : MonoBehaviour
{
    public enum ZoomType: int { None, ZoomIn,ZoomOut, Sliding};
    public int MaxZoomIn;           // ������������ ������� �����������
    public int MaxZoomOut;          // ������������ ������� ���������

    public float ZoomSensetivity;   // ���������������� ����������� ������
    public Rigidbody rb;

    public ZoomType zoomType;       // ��� ����������� ������

    public float SlidingTime;       // ����� ��������������� ���������� ������ ����� �����������/���������
    public float StopSlidingTime;
    private void Awake()
    {
        zoomType = ZoomType.None;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float zooming = Input.GetAxis("Mouse ScrollWheel");
        if(zooming > 0.0f)      // �������� �������� ������ -> ���������� ������
        {
            if(zoomType == ZoomType.ZoomOut)
            {
                rb.velocity = Vector3.zero;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, -1, 1) * ZoomSensetivity, Time.deltaTime*3 );
            zoomType = ZoomType.ZoomIn;
        }
        else if(zooming < 0.0f)
        {
            if(zoomType == ZoomType.ZoomIn)
            {
                rb.velocity = Vector3.zero;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, 1, -1) * ZoomSensetivity, Time.deltaTime*3 );
            zoomType = ZoomType.ZoomOut;
        }
        else
        {
            if(zoomType == ZoomType.Sliding && Time.time > StopSlidingTime)
            {
                rb.velocity = Vector3.zero;
            }
            if (zoomType == ZoomType.ZoomIn)
            {
                rb.AddForce(new Vector3(0, -7, 7));
                StopSlidingTime = Time.time + SlidingTime;
                zoomType = ZoomType.Sliding;
            }
            else if(zoomType == ZoomType.ZoomOut)
            {
                rb.AddForce(new Vector3(0, 7, -7));
                StopSlidingTime = Time.time + SlidingTime;
                zoomType = ZoomType.Sliding;
            }
        }
    }
}
