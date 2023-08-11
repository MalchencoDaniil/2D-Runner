using UnityEngine;

public class FrameSwitch : MonoBehaviour
{
    [SerializeField] private GameObject activeFrame;
    [SerializeField] private GameObject camera;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            activeFrame.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMove>())
        {
            camera.SetActive(false);
            activeFrame.SetActive(false);
        }
    }
}