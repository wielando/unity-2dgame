using System.Collections;
using UnityEngine;

public class AIDetection : MonoBehaviour
{
    private bool isPlayerDetected = false;
    public float movementSpeed = 4f;

    public Rigidbody2D rb;

    public GameObject playerObject;
    private GameObject lastPlayerSeenLocation;

    private AIFoundPlayerIcon AIFoundPlayerIcon;
    private AIMissingPlayerIcon AIMissingPlayerIcon;

    public LayerMask collisionMask;

    private BoxCollider2D boxCollider2D;

    private float _defaultPositionX;
    private float _defaultPositionY;
    private Vector2 _defaultPosition;

    private bool _crIsRunning = false;
    private bool _enemyAlreadyRotatet = false;

    //TODO: STATUS

    private Coroutine _rotationCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        this.playerObject = GameObject.FindGameObjectWithTag("Player");
        _defaultPositionX = gameObject.transform.position.x;
        _defaultPositionY = gameObject.transform.position.y;
        _defaultPosition = new Vector2(_defaultPositionX, _defaultPositionY);

        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "AIFoundPlayer")
                this.AIFoundPlayerIcon = gameObject.AddComponent<AIFoundPlayerIcon>();
            if (child.name == "AIMissingPlayer")
                this.AIMissingPlayerIcon = gameObject.AddComponent<AIMissingPlayerIcon>();
        }
    }

    private void OnDrawGizmos()
    {
        if (boxCollider2D)
        {
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxCollider2D.transform.position, boxCollider2D.transform.rotation, boxCollider2D.transform.lossyScale);
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(boxCollider2D.offset, boxCollider2D.size);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (this.isPlayerDetected)
        {
            if (_rotationCoroutine != null)
            {
                StopCoroutine(_rotationCoroutine);
                _crIsRunning = false;
            }

            _enemyAlreadyRotatet = false;
            
            transform.position = Vector2.MoveTowards(transform.position, this.playerObject.transform.position, this.movementSpeed * Time.deltaTime);
        }


        if (!this.isPlayerDetected && this.lastPlayerSeenLocation != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, this.lastPlayerSeenLocation.transform.position, this.movementSpeed * Time.deltaTime);

            if (transform.position.Equals(this.lastPlayerSeenLocation.transform.position))
            {
                if (!_crIsRunning && !_enemyAlreadyRotatet)
                    _rotationCoroutine = StartCoroutine(RotateEnemy());


                Destroy(this.lastPlayerSeenLocation);
                this.lastPlayerSeenLocation = null;

            }
        }

    }

    private IEnumerator RotateEnemy()
    {
        _crIsRunning = true;

        const int maxRotationCount = 1;
        int rotationAngle = (int) transform.rotation.z;

        for (int rotations = 0; rotations <= maxRotationCount; rotations++)
        {
            this.AIFoundPlayerIcon.savePosition();
            this.AIMissingPlayerIcon.savePosition();

            gameObject.transform.eulerAngles = Vector3.forward * rotationAngle;

            Debug.Log(rotations);

            this.AIFoundPlayerIcon.setIconToDefaultPosition();
            this.AIMissingPlayerIcon.setIconToDefaultPosition();

            rotationAngle += 90;
            Debug.Log("Rotation: " + rotationAngle);

            yield return new WaitForSeconds(2f);
        }

        _crIsRunning = false;
        _enemyAlreadyRotatet = true;
        yield return null;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.attachedRigidbody.tag.Equals("Player"))
        {
            Vector2 direction = (this.playerObject.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, this.movementSpeed, collisionMask);

            if (hit.collider == null)
            {
                if (this.lastPlayerSeenLocation != null)  
                    Destroy(this.lastPlayerSeenLocation);
              

                this.isPlayerDetected = true;
                this.AIFoundPlayerIcon.SetAIFoundPlayerIconStatus(true);
                this.AIMissingPlayerIcon.SetAIMissingPlayerIconStatus(false);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody.tag.Equals("Player") && this.isPlayerDetected)
        {

            this.isPlayerDetected = false;
             
            if (GameObject.Find("LastPlayerLocationCube"))
            {
                Destroy(GameObject.Find("LastPlayerLocationCube"));
            }

            this.lastPlayerSeenLocation = GameObject.CreatePrimitive(PrimitiveType.Cube);
            lastPlayerSeenLocation.SetActive(false);
            lastPlayerSeenLocation.name = "LastPlayerLocationCube";
            lastPlayerSeenLocation.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y, 0);

            this.AIFoundPlayerIcon.SetAIFoundPlayerIconStatus(false);
            this.AIMissingPlayerIcon.SetAIMissingPlayerIconStatus(true);
        }
    }

}
