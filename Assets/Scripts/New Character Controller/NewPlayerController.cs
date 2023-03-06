using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    private const string StartMoving = "isMoving";

    private bool _isMoving;
    [Header("Elements")]
    public CharacterController characterController;

    public VariableJoystick variableJoystick;

    public float moveSpeed;
    public float rotationSpeed = 10.0f;

    public Animator anim;

    public static NewPlayerController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        variableJoystick = UIManager.Instance.variableJoystick;
    }

    private void Update()
    {
        //MovePlayer();

        NewMovePlayer();
        RotatePlayer();
    }


    private void MovePlayer()
    {
        if (anim)
        {
            anim.SetBool(StartMoving, _isMoving);
        }

        //var hr = variableJoystick.Horizontal;
        float horizontal = variableJoystick.Horizontal;
        float vertical = variableJoystick.Vertical;

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        movement = transform.TransformDirection(movement);
        movement *= moveSpeed;

        movement.Normalize();

        characterController.Move(movement * Time.deltaTime);
        

        if (movement.magnitude > 0.1f)
        {
            _isMoving = true;
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            _isMoving = false;
        }
    }

    private void NewMovePlayer()
    {
        if (anim)
        {
            anim.SetBool(StartMoving, _isMoving);
        }

        Vector3 movement = new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical);
        movement *= moveSpeed;

        if(movement.magnitude > 0.001f)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }



        characterController.Move(movement);
    }

    private void RotatePlayer()
    {
        Vector3 movement = new Vector3(variableJoystick.Horizontal, 0f, variableJoystick.Vertical);

        movement.Normalize();

        //Debug.Log("Movement magnitude: " + movement.magnitude);

        transform.forward = movement;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Balloon>() != null)
        {
            AudioManager.instance.PlayEggCollectSound();

            GameManager.Instance.inventory.Add(other.gameObject);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;

            var inventoryCount = GameManager.Instance.inventory.Count;
            inventoryCount--;

            other.gameObject.transform.SetParent(UIManager.Instance.eggGridContent.GetChild(inventoryCount));

            other.gameObject.GetComponent<Animator>().enabled = false;

            other.gameObject.transform.localPosition = new Vector3(0, 0, 0);

            StartCoroutine(LerpScaling(other.gameObject.transform.localScale, new Vector3(415, 415, 415), other.gameObject, false));

            other.gameObject.GetComponent<Balloon>().particlesToEnable.SetActive(true);

            //Remove balloon from the level list
            Level.Instance.UpdateBalloonInhisLevel(other.gameObject);

            GameManager.Instance.TestIfThereAreThreeSameObjectsConsecutively();
        }


        if (other.gameObject.tag == "Death")
        {
            Debug.Log("Level Failed");
            Instantiate(LevelManager.Instance.blastPrefab, this.gameObject.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            AudioManager.instance.playerDeathSound.Play();
            Invoke(nameof(DelayInLevelFailed), 0.1f);
        }
    }

    private float elapsedTime;
    private float timeToLerp = 0.5f;

    private IEnumerator LerpScaling(Vector3 scale, Vector3 scaleTo, GameObject obj, bool isReverse)
    {

        Vector3 offset = new Vector3(0, 0, 2.5f);

        Transform baseObj = obj.transform;

        elapsedTime = 0;

        while (elapsedTime < timeToLerp)
        {

            baseObj.localScale = Vector3.Lerp(scale, scaleTo, elapsedTime / timeToLerp);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Debug.Log("Level Failed");
            Instantiate(LevelManager.Instance.blastPrefab, this.gameObject.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
            AudioManager.instance.playerDeathSound.Play();
            Invoke(nameof(DelayInLevelFailed), 0.5f);
        }
    }



    private void DelayInLevelFailed()
    {
        LevelManager.Instance.OnLevelFailedEvent();
    }


}
