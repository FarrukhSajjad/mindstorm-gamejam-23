using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private const string StartMoving = "isMoving";
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float rotationSpeed = 500;

    public Animator anim;

    private Touch _touch;

    private Vector3 _touchDown;
    private Vector3 _touchUp;

    private bool _dragStarted;
    private bool _isMoving;

    public static PlayerMovement instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (anim)
        {
            anim.SetBool(StartMoving, _isMoving);
        }
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            if (_touch.phase == TouchPhase.Began)
            {
                _dragStarted = true;
                _isMoving = true;
                _touchUp = _touch.position;
                _touchDown = _touch.position;
            }
        }
        if (_dragStarted)
        {
            if (_touch.phase == TouchPhase.Moved)
            {
                _touchDown = _touch.position;
            }

            if (_touch.phase == TouchPhase.Ended)
            {
                _touchDown = _touch.position;
                _isMoving = false;
                _dragStarted = false;
            }
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, CalculateRotation(), rotationSpeed * Time.deltaTime);
            gameObject.transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
    }

    public bool IsMoving()
    {
        return _isMoving;
    }

    private Quaternion CalculateRotation()
    {
        Quaternion temp = Quaternion.LookRotation(CalculateDirection(), Vector3.up);
        return temp;
    }
    private Vector3 CalculateDirection()
    {
        Vector3 temp = (_touchDown - _touchUp).normalized;
        temp.z = temp.y;
        temp.y = 0;
        return temp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Balloon>() != null)
        {
            GameManager.Instance.inventory.Add(other.gameObject);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;

            //fly the balloon up
            var rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(Vector3.up * 20, ForceMode.Impulse);

            //other.gameObject.SetActive(false);

            // Create a new GameObject
            GameObject newImageObject = new GameObject("Image");

            // Add an Image component to the new GameObject
            Image newImage = newImageObject.AddComponent<Image>();

            newImage.sprite = other.gameObject.GetComponent<Balloon>().gemSprite;

            newImage.gameObject.transform.SetParent(UIManager.Instance.gridContent);

            //Sprite newSprite;
            //// Set the sprite of the new Image component
            //newImage.sprite = newSprite;

            //Remove balloon from the level list
            Level.Instance.UpdateBalloonInhisLevel(other.gameObject);

            //Add image in the canvas
            UIManager.Instance.gridImages.Add(newImage);

            GameManager.Instance.TestIfThereAreThreeSameObjectsConsecutively();
        }
    }
}
