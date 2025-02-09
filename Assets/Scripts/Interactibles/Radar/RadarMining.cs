using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RadarMining : MonoBehaviour
{
    public enum RessourcesType
    {
        Dirt,
        Rock,
        Coal
    }

    [SerializeField] GameObject _dirtPrefab;
    [SerializeField] GameObject _rockPrefab;
    [SerializeField] GameObject _coalPrefab;
    [SerializeField] Transform _ressourceParent;
    [SerializeField] List<Transform> _ressourcesLocation = new();
    [SerializeField] float _distanceSpawn = 3;

    [SerializeField] float _timerMine = 1;
    List<Ressource> _ressourcesOnZone = new();
    float _timeMine = 0;
    bool _canMineNow = true;

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            var coal = Instantiate(_coalPrefab, _ressourceParent);
            coal.transform.localPosition = Random.insideUnitCircle * _distanceSpawn;
            coal.transform.localScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));
            _ressourcesLocation.Add(coal.transform);
        }

        for (int i = 0; i < 10; i++)
        {
            var dirt = Instantiate(_dirtPrefab, _ressourceParent);
            dirt.transform.localPosition = Random.insideUnitCircle * _distanceSpawn;
            dirt.transform.localScale = new Vector3(Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f), Random.Range(0.1f, 0.5f));
            _ressourcesLocation.Add(dirt.transform);
        }

        var rock = Instantiate(_rockPrefab, _ressourceParent);
        rock.transform.localScale = new Vector3(_distanceSpawn * 2, _distanceSpawn * 2, _distanceSpawn * 2);
        _ressourcesLocation.Add(rock.transform);
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePause && GameManager.Instance.PanelControl.Power.IsActive)
        {
            _timeMine += Time.deltaTime;

            if (_timeMine >= _timerMine)
            {
                _canMineNow = true;
                _timeMine = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!GameManager.Instance.IsGamePause && GameManager.Instance.PanelControl.Power.IsActive && GameManager.Instance.PanelControl.Drill.IsActive)
        {
            Ressource ressource = other.GetComponent<Ressource>();

            if (ressource != null && !_ressourcesOnZone.Contains(ressource))
                _ressourcesOnZone.Add(ressource);

            if (_canMineNow)
            {
                for (int i = 0; i < _ressourcesOnZone.Count; i++)
                {
                    Mine(_ressourcesOnZone[i]);
                }

                _ressourcesOnZone.Clear();
            }
        }
    }

    void Mine(Ressource ressource)
    {
        if (!GameManager.Instance.IsGamePause && ressource != null)
        {
            if (ressource.Type != RessourcesType.Dirt
                && ressource.Type != RessourcesType.Rock
                && ressource.Type != RessourcesType.Coal)
                return;

            if (ressource.Type == RessourcesType.Coal)
            {
                //Debug.Log("Mine coal");
                GameManager.Instance.Belt.SpawnRessource(GameManager.Instance.Belt.CoalPrefab);
                _canMineNow = false;
            }
            else if (ressource.Type == RessourcesType.Dirt)
            {
                //Debug.Log("Mine dirt");
                GameManager.Instance.Belt.SpawnRessource(GameManager.Instance.Belt.DirtPrefab);
                _canMineNow = false;
            }
            else if (ressource.Type == RessourcesType.Rock)
            {
                //Debug.Log("Mine rocks");
                GameManager.Instance.Belt.SpawnRessource(GameManager.Instance.Belt.RockPrefab);
                _canMineNow = false;
            }

            ressource.RessourcesAmount -= Time.deltaTime;

            if (ressource.RessourcesAmount <= 0)
            {
                _ressourcesLocation.Remove(ressource.transform);
                Destroy(ressource.gameObject);
            }
        }
    }

    public void Move(Vector3 move, Quaternion rotate)
    {
        for (int i = 0; i < _ressourcesLocation.Count; i++)
        {
            _ressourcesLocation[i].position -= move;
        }

        _ressourceParent.rotation *= rotate;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_ressourceParent.position, _distanceSpawn);
    }
}
