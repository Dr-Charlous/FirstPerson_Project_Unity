using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Parameters :")]
    [SerializeField] float _distance;
    [SerializeField] LayerMask _interactions;
    [SerializeField] GameObject _uiText;
    [SerializeField] Transform _obj;

    [Header("Hands :")]
    public Hands Hands;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGamePause)
            return;

        if (PlayerComponentManager.Instance.Stats.IsDead)
            return;

        RaycastHit hit;

        if (PlayerComponentManager.Instance.PlayerInputs.Player.Eject.triggered && Hands.GetObjectInHand(0) != null)
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

                if (PlayerComponentManager.Instance.PlayerInputs.Player.Interact.triggered)
                {
                    //Interactibles/Radio/Panel
                    var inter = hit.transform.GetComponent<Interactible>();
                    //var radio = hit.transform.GetComponent<Radio>();
                    //var panel = hit.transform.GetComponent<ControlPanel>();
                    //var bed = hit.transform.GetComponent<Bed>();

                    if (inter != null)
                        inter.ChangeTarget();
                    //else if (radio != null)
                    //    radio.ChangeTarget();
                    //else if (panel != null)
                    //    panel.PanelInt.OnAction(panel, Hands);
                    //else if (bed != null)
                    //    bed.PanelInt.OnAction(bed, Hands);
                }
            }

            if (_uiText != null)
                _uiText.SetActive(false);
        }
        else
        {
            _obj.gameObject.SetActive(false);

            //Ray
            if (Physics.Raycast(origin, transform.forward, out hit, _distance, _interactions) && PlayerComponentManager.Instance.Look.IsOnHead)
            {
                if (_uiText != null)
                    _uiText.SetActive(true);

                if (PlayerComponentManager.Instance.PlayerInputs.Player.Interact.triggered)
                {
                    //Doors/Objects/Objects placement/Furnase/Panel
                    //var door = hit.transform.GetComponent<Doors>();
                    var obj = hit.transform.GetComponent<ObjectsComponents>();
                    var place = hit.transform.GetComponent<ObjectPlacement>();
                    //var furnase = hit.transform.GetComponent<Furnase>();
                    //var panel = hit.transform.GetComponent<ControlPanel>();
                    //var bed = hit.transform.GetComponent<Bed>();
                    var interact = hit.transform.GetComponent<Interactible>();

                    //if (door != null)
                    //    door.DoorInt.OnAction(door, Hands);
                    if (obj != null)
                        obj.ObjInt.OnAction(obj, Hands);
                    else if (place != null)
                        place.PlacementInt.OnAction(place, Hands);
                    //else if (furnase != null)
                    //    furnase.FurnaseInt.OnAction(furnase, Hands);
                    //else if (panel != null)
                    //    panel.PanelInt.OnAction(panel, Hands);
                    //else if (bed != null)
                    //    bed.PanelInt.OnAction(bed, Hands);
                    else if (interact != null && interact.IsActivableOut)
                        interact.ChangeTarget();
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