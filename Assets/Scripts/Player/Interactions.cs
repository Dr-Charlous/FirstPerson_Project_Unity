using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Parameters :")]
    [SerializeField] float _distance;
    [SerializeField] LayerMask _interactions;
    [SerializeField] GameObject _uiText;
    [SerializeField] Transform _obj;

    [Header("Inputs :")]
    [SerializeField] KeyCode _interact;
    [SerializeField] KeyCode _eject;

    [Header("Hands :")]
    public Hands Hands;

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(_eject) && Hands.GetObjectInHand(0) != null)
        {
            Hands.GetObjectInHand(0).GetComponent<ObjectsComponents>().Grab(null);
            Hands.LoseObjectInHand(0);
        }

        //Origin point of ray
        Vector3 origin = transform.position;
        Debug.DrawLine(origin, origin + transform.TransformDirection(Vector3.forward) * _distance);

        if (!PlayerComponentManager.Instance.Look.IsOnHead)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                _obj.gameObject.SetActive(true);
                //Debug.Log(hit.point);
                _obj.position = hit.point;

                if (Input.GetKeyDown(_interact))
                {
                    //Interactibles
                    var inter = hit.transform.GetComponent<Interactible>();

                    if (inter != null)
                        inter.ChangeTarget();

                    //Radio
                    var radio = hit.transform.GetComponent<Radio>();

                    if (radio != null)
                        radio.ChangeTarget();
                }
            }
        }
        else
        {
            _obj.gameObject.SetActive(false);

            //Ray
            if (Physics.Raycast(origin, transform.forward, out hit, _distance, _interactions) && GameManager.Instance.Player.Look.IsOnHead)
            {
                if (_uiText != null)
                    _uiText.SetActive(true);

                if (Input.GetKeyDown(_interact))
                {
                    //Doors
                    var door = hit.transform.GetComponent<Doors>();
                    if (door != null)
                        door.DoorInt.OnAction(door, Hands);

                    //Objects
                    var obj = hit.transform.GetComponent<ObjectsComponents>();
                    if (obj != null)
                        obj.ObjInt.OnAction(obj, Hands);

                    //Objects placement
                    var place = hit.transform.GetComponent<ObjectPlacement>();
                    if (place != null)
                        place.PlacementInt.OnAction(place, Hands);

                    //Furnase
                    var furnase = hit.transform.GetComponent<Furnase>();
                    if (furnase != null)
                        furnase.FurnaseInt.OnAction(furnase, Hands);
                }
            }
            else
            {
                if (_uiText != null)
                    _uiText.SetActive(false);
            }
        }

        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;

    //    Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(Vector3.forward) * _distance);
    //}
}

public interface IInteraction
{
    public void OnAction(MonoBehaviour script, Hands hands);
}