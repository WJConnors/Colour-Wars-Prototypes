using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    [SerializeField] Ammo ammoPrefab;
    List<AmmoMover> ammoMovers = new List<AmmoMover>();
    List<Miner> miners = new List<Miner>();
    List<Mine> mines = new List<Mine>();
    List<Ammo> ammos = new List<Ammo>();
    List<Tower> towers = new List<Tower>();

    public void Awake()
    {
        ammoMovers.AddRange(FindObjectsOfType<AmmoMover>());
        miners.AddRange(FindObjectsOfType<Miner>());
        mines.AddRange(FindObjectsOfType<Mine>());
        ammos.AddRange(FindObjectsOfType<Ammo>());
        towers.AddRange(FindObjectsOfType<Tower>());
    }
    //getter functions for lists
    public List<AmmoMover> AmmoMovers { get => ammoMovers; }
    public List<Miner> Miners { get => miners; }
    public List<Mine> Mines { get => mines; }
    public List<Ammo> Ammos { get => ammos; }
    public List<Tower> Towers { get => towers; }

    private void Update()
    {
        AmmoMovement();
    }

    void AmmoMovement()
    {
        List<Ammo> toRemove = new List<Ammo>();
        foreach (Ammo ammo in ammos)
        {
            if (!ammo.mover)
            {
                foreach(AmmoMover ammoMover in ammoMovers)
                {
                    if (Vector2.Distance(ammo.transform.position, ammoMover.transform.position) < 0.6f && !ammo.movers.Contains(ammoMover))
                    {
                        ammo.movers.Add(ammoMover);
                        ammo.mover = ammoMover;
                        ammo.GetComponent<Rigidbody2D>().velocity = ammoMover.transform.position - ammo.transform.position;
                    }
                }
            }
            else
            {
                if (Vector2.Distance(ammo.transform.position, ammo.mover.transform.position) > 0.6f)
                {
                    ammo.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                    ammo.mover = null;
                }


            }
            foreach (Tower tower in towers)
            {
                if (Vector2.Distance(ammo.transform.position, tower.transform.position) < 0.5f)
                {
                    toRemove.Add(ammo);
                    tower.containedAmmo.Add(ammo);
                    ammo.transform.position = tower.transform.position;
                    ammo.GetComponent<SpriteRenderer>().enabled = false;
                    tower.ColorUpdate();
                }
            }
        }
        foreach (Ammo ammo in toRemove)
        {
            ammos.Remove(ammo);
        }
    }

    public Color MinerOverlap (Miner miner)
    {
        Color color = new Color(0,0,0,0);
        foreach (Mine mine in mines)
        {
            if (mine.transform.position == miner.transform.position)
            {
                color = mine.Color;
                break;
            }
        }
        return color;
    }

    public void SpawnAmmo(AmmoMover mover)
    {
        foreach (Miner miner in miners)
        {
            if (Vector2.Distance(mover.transform.position, miner.transform.position) < 1.5f)
            {
                Vector2 spawnPoint = (mover.transform.position + miner.transform.position)/2;
                Ammo ammo = Instantiate(ammoPrefab, spawnPoint, Quaternion.identity);
                ammo.GetComponent<SpriteRenderer>().color = miner.GetComponent<SpriteRenderer>().color;
                ammos.Add(ammo);
            }
        }

    }

}
