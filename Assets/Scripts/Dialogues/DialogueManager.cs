using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI DialogText;

    [SerializeField] GameObject _DialogParent;
    [SerializeField] Dialogs _actualDialogue;
    [SerializeField] float _timerValue;

    Dialogs _dialogue;
    float _timer;

    private void Start()
    {
        _DialogParent.SetActive(false);
        //_actualDialogue.UpdateDialogue(NameText, DialogText);
        _dialogue = _actualDialogue;
    }

    private void Update()
    {
        if (_actualDialogue != null)
        {
            if (_timer < _timerValue)
                _timer += Time.deltaTime;

            if (_timer >= _timerValue/* && Input.GetMouseButtonDown(0)*/)      //Input next line
            {
                NextDialogue();
            }
        }
        
        //if (Input.GetMouseButtonDown(1))     //Input exemple call dialog
        //{
        //    UpdateDialogue(_dialogue);
        //}
    }

    //Next line
    void NextDialogue()
    {
        if (_actualDialogue.NextDialogue != null)
        {
            _actualDialogue = _actualDialogue.NextDialogue;
            _actualDialogue.UpdateDialog(NameText, DialogText);
        }
        else
        {
            _DialogParent.SetActive(false);
            _actualDialogue = null;
        }

        _timer = 0;
    }

    //Activate dialog box
    public void UpdateDialogue(Dialogs dialogue)
    {
        if (dialogue != null)
        {
            _DialogParent.SetActive(true);
            _actualDialogue = dialogue;
            _actualDialogue.UpdateDialog(NameText, DialogText);
            _timer = 0;
        }
    }
}
