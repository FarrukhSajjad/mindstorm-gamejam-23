using System.Collections;
using UnityEngine;

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
        if (instance == null)
        {
            instance = this;
            return;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (Level.Instance.isTutorialLevel)
        {
            this.enabled = false;
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
        if (other.gameObject.GetComponent<Balloon>() != null)
        {
            AudioManager.instance.PlayEggCollectSound();

            GameManager.Instance.inventory.Add(other.gameObject);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;

            var inventoryCount = GameManager.Instance.inventory.Count;
            inventoryCount--;

            other.gameObject.transform.SetParent(UIManager.Instance.eggGridContent.GetChild(inventoryCount));

            other.gameObject.GetComponent<Animation>().enabled = false;

            other.gameObject.transform.localPosition = new Vector3(0, 0, 0);

            StartCoroutine(LerpScaling(other.gameObject.transform.localScale, new Vector3(130, 130, 130), other.gameObject, false));

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
        if(collision.gameObject.tag == "Death")
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
