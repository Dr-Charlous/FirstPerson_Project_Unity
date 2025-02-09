using UnityEngine;

public class Interactions : MonoBehaviour
{
    [SerializeField] float _distance;
    [SerializeField] LayerMask _interactions;
    [SerializeField] GameObject _uiText;
    [SerializeField] Transform _obj;

    public Hands Hands;

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Q) && Hands.GetObjectInHand(0) != null)
        {
            Hands.GetObjectInHand(0).GetComponent<ObjectsComponents>().Grab(null);
            Hands.DestroyObjectInHand(0);
        }

        //Origin point of ray
        Vector3 origin = transform.position;
        Debug.DrawLine(origin, origin + transform.TransformDirection(Vector3.forward) * _distance);

        if (!GameManager.Instance.Player.Look.IsOnHead)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                _obj.gameObject.SetActive(true);
                //Debug.Log(hit.point);
                _obj.position = hit.point;

                if (Input.GetKeyDown(KeyCode.Mouse0))
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

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Doors
                    var door = hit.transform.GetComponent<Doors>();

                    if (door != null)
                    {
                        //var objComp = Hands[0].ObjectInHand.GetComponent<ObjectsComponents>();

                        //if (objComp != null && door.IsLocked)
                        //    door.IsLocked = !objComp.Use();

                        if (!door.IsLocked)
                            door.ChangeTarget();
                        else
                            Debug.Log("Door close");
                    }

                    //Objects
                    var obj = hit.transform.GetComponent<ObjectsComponents>();

                    if (obj != null && Hands.GetObjectInHand(0) == null)
                    {
                        obj.Grab(Hands.GetObjectInHand(0).transform);
                        Hands.SetObjectInHand(0, obj.gameObject);
                    }

                    //Objects placement
                    var placement = hit.transform.GetComponent<ObjectPlacement>();

                    if (placement != null && Hands.GetObjectInHand(0) != null)
                    {
                        var objComp = Hands.GetObjectInHand(0).GetComponent<ObjectsComponents>();

                        if (!placement.IsReplace && objComp.ObjectInfos.Type == ObjectInfos.ObjectType.Change && objComp.ObjectInfos.SubType == placement.SubType)
                        {
                            placement.IsReplace = objComp.Use();
                            Hands.DestroyObjectInHand(0);

                            placement.Repair();
                        }
                        else
                            Debug.Log("Not the right one or not brake yet");
                    }

                    //Furnase
                    var furnase = hit.transform.GetComponent<Furnase>();

                    if (furnase != null && Hands.GetObjectInHand(0) != null)
                    {
                        furnase.Repair(Hands.GetObjectInHand(0).GetComponent<Fuel>());

                        Hands.DestroyObjectInHand(0);
                    }
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
