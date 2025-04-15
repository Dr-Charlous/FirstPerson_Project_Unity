using UnityEngine;

public class Interactions : MonoBehaviour
{
    [Header("Parameters :")]
    [SerializeField] float _distance;
    [SerializeField] LayerMask _interactions;
    [SerializeField] GameObject _visualFeedBack;
    [SerializeField] Transform _obj;

    [Header("Hands :")]
    public Hands Hands;

    private void Update()
    {
        if (PlayerManager.Instance.UiManager.IsGamePause)
            return;

        if (PlayerManager.Instance.Stats.IsDead)
            return;

        RaycastHit hit;

        if (PlayerManager.Instance.PlayerInputs.Player.Eject.triggered && Hands.GetObjectInHand(0) != null)
        {
            Hands.GetObjectInHand(0).GetComponent<ObjectsComponents>().Grab(null);
            Hands.LoseObjectInHand(0);
        }

        //Origin point of ray
        Vector3 origin = transform.position;
        Debug.DrawLine(origin, origin + transform.TransformDirection(Vector3.forward) * _distance);

        if (!PlayerManager.Instance.Look.IsOnHead)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                _obj.gameObject.SetActive(true);
                //Debug.Log(hit.point);
                _obj.position = hit.point;

                if (PlayerManager.Instance.PlayerInputs.Player.Interact.triggered)
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

            if (_visualFeedBack != null)
                _visualFeedBack.SetActive(false);
        }
        else
        {
            _obj.gameObject.SetActive(false);

            //Ray
            if (Physics.Raycast(origin, transform.forward, out hit, _distance, _interactions) && PlayerManager.Instance.Look.IsOnHead)
            {
                if (_visualFeedBack != null)
                    _visualFeedBack.SetActive(true);

                if (PlayerManager.Instance.PlayerInputs.Player.Interact.triggered)
                {
                    var obj = hit.transform.GetComponent<ObjectsComponents>();
                    var place = hit.transform.GetComponent<ObjectPlacement>();
                    var interact = hit.transform.GetComponent<Interactible>();
                    //Doors/Objects/Objects placement/Furnase/Panel
                    //var door = hit.transform.GetComponent<Doors>();
                    //var furnase = hit.transform.GetComponent<Furnase>();
                    //var panel = hit.transform.GetComponent<ControlPanel>();
                    //var bed = hit.transform.GetComponent<Bed>();

                    if (obj != null)
                        obj.ObjInt.OnAction(obj, Hands);
                    if (place != null)
                        place.PlacementInt.OnAction(place, Hands);
                    if (interact != null && interact.IsActivableOut)
                        interact.ChangeTarget();
                    //if (door != null)
                    //    door.DoorInt.OnAction(door, Hands);
                    //else if (furnase != null)
                    //    furnase.FurnaseInt.OnAction(furnase, Hands);
                    //else if (panel != null)
                    //    panel.PanelInt.OnAction(panel, Hands);
                    //else if (bed != null)
                    //    bed.PanelInt.OnAction(bed, Hands);
                }
            }
            else
            {
                if (_visualFeedBack != null)
                    _visualFeedBack.SetActive(false);
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