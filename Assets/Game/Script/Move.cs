using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Transform playerModel;
    [SerializeField] private Score score;
    private float diem = 0;

    private GameObject go;
    private Vector3 startPos;
    private Vector3 direction;
    private Vector3 endPos;
    private bool directionChosen;
    public LayerMask layer;
    public float raycastDistance = 1f;
    private float height;

    void Update()
    {
        Moving();
    }

    private void Moving()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            directionChosen = false;
        }
        else if (Input.GetMouseButtonUp(0) && !directionChosen)
        {
            endPos = Input.mousePosition;
            direction = endPos - startPos;
            direction.Normalize();
            ConvertMoving();
        }
    }

    private void ConvertMoving()
    {
        float angle = Vector2.Angle(direction, Vector2.up);
        float angle1 = Vector2.Angle(direction, Vector2.right);

        // Detect swipe direction
        if (direction.y > 0 && angle < 45)
        {
            Debug.Log("Swipe Up");
            MoveDirection(Vector3.forward);
        }
        else if (direction.x > 0 && angle1 < 45)
        {
            Debug.Log("Swipe Right");
            MoveDirection(Vector3.right);
        }
        else if (direction.x < 0 && angle1 > 135)
        {
            Debug.Log("Swipe Left ");
            MoveDirection(Vector3.left);
        }
        else if (direction.y < 0 && (angle > 135))
        {
            Debug.Log("Swipe Down");
            MoveDirection(Vector3.back);
        }

        directionChosen = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        EatBrick(other);
        ReleaseBrick(other);
    }

    private void MoveDirection(Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, raycastDistance, layer))
        {
            Debug.Log("Stoppppppppppppppppppppp");
            rb.velocity = Vector3.zero;
        }
        else
        {
            rb.velocity = direction.normalized * speed;
        }
    }


    private void EatBrick(Collider other)
    {
        if (other.gameObject.tag == "Gach")
        {
            height += 0.2f;
            playerModel.position = new Vector3(playerModel.position.x, playerModel.position.y + 0.2f, playerModel.position.z);
            go = Instantiate(brickPrefab, transform);
            go.transform.position = new(transform.position.x, transform.position.y + height, transform.position.z);
            other.GetComponent<BoxCollider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
            diem++;
            score.UpdateScore(diem);
        }
    }

    private void ReleaseBrick(Collider other)
    {
        if(other.gameObject.tag == "Cau")
        {
            other.tag = "Untagged";
            go = Instantiate(brickPrefab);
            go.transform.localRotation = Quaternion.identity;  
            go.transform.localEulerAngles = new Vector3(-90, 0, 0);
            go.transform.position = new(other.transform.position.x, other.transform.position.y + 0.3f, other.transform.position.z);
            GameObject x = transform.GetChild(2).gameObject;
            Destroy(x);
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        }
    }
}
