using System;
using System.Collections.Generic;
using System.Linq;
using Classes;
using Interfaces;
using Wintellect.PowerCollections;

public class PitFortressCollection : IPitFortress
{
    private readonly Dictionary<string, Player> playerByName;
    private readonly SortedSet<Player> players;
    private readonly OrderedDictionary<int, SortedSet<Minion>> minionByX;
    private readonly SortedSet<Mine> mines;
    private int mineId;
    private int minionId;

    public PitFortressCollection()
    {
        this.mineId = 1;
        this.minionId = 1;
        this.playerByName = new Dictionary<string, Player>();
        this.players = new SortedSet<Player>();
        this.minionByX = new OrderedDictionary<int, SortedSet<Minion>>();
        this.mines = new SortedSet<Mine>();
    }

    public int PlayersCount => this.players.Count;

    public int MinionsCount => this.minionByX.Sum(m => m.Value.Count);

    public int MinesCount => this.mines.Count;

    public void AddPlayer(string name, int mineRadius)
    {
        if (this.playerByName.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        var newPlayer = new Player(name, mineRadius);
        this.playerByName.Add(name, newPlayer);
        this.players.Add(newPlayer);
    }

    public void AddMinion(int xCoordinate)
    {
        var newMinion = new Minion(this.minionId++, xCoordinate);
        if (!this.minionByX.ContainsKey(xCoordinate))
        {
            this.minionByX.Add(xCoordinate, new SortedSet<Minion>());
        }

        this.minionByX[xCoordinate].Add(newMinion);
    }

    public void SetMine(string playerName, int xCoordinate, int delay, int damage)
    {
        if (!this.playerByName.ContainsKey(playerName))
        {
            throw new ArgumentException();
        }

        var currentPlayer = this.playerByName[playerName];
        var newMine = new Mine(currentPlayer, this.mineId++, xCoordinate, delay, damage);
        this.mines.Add(newMine);
    }

    public IEnumerable<Minion> ReportMinions()
    {
        var minions = this.minionByX.Values;
        foreach (var set in minions)
        {
            foreach (var minion in set)
            {
                yield return minion;
            }
        }
    }

    public IEnumerable<Player> Top3PlayersByScore()
    {
        if (this.players.Count < 3)
        {
            throw new ArgumentException();
        }

        return this.players.Reverse().Take(3);
    }

    public IEnumerable<Player> Min3PlayersByScore()
    {
        if (this.players.Count < 3)
        {
            throw new ArgumentException();
        }

        return this.players.Take(3);
    }

    public IEnumerable<Mine> GetMines()
    {
        return this.mines;
    }

    public void PlayTurn()
    {
        List<Mine> detonatedMines = this.GetDetonatedMinesAfterEffect();
        foreach (var detonatedMine in detonatedMines)
        {
            var minionsInRange = this.GetMinionsInRange(detonatedMine);
            this.InteractMinionsInRange(detonatedMine, minionsInRange);
        }

        this.RemoveDetonatedMines(detonatedMines);
    }

    private List<Mine> GetDetonatedMinesAfterEffect()
    {
        var detonatedMines = new List<Mine>();
        foreach (var mine in this.mines)
        {
            mine.Delay--;
            if (mine.Delay == 0)
            {
                detonatedMines.Add(mine);
            }
        }

        return detonatedMines;
    }

    private void UpdatePlayerScore(Player player)
    {
        this.players.Remove(player);
        player.Score++;
        this.players.Add(player);
    }

    private List<Minion> GetMinionsInRange(Mine detonatedMine)
    {
        var lowBoundRange = detonatedMine.XCoordinate - detonatedMine.Player.Radius;
        var highBoundRange = detonatedMine.XCoordinate + detonatedMine.Player.Radius;
        var minionsInRange = this.minionByX
            .Range(lowBoundRange, true, highBoundRange, true)
            .SelectMany(m => m.Value)
            .ToList();
        return minionsInRange;
    }

    private void RemoveDetonatedMines(List<Mine> detonatedMines)
    {
        foreach (var detonatedMine in detonatedMines)
        {
            this.mines.Remove(detonatedMine);
        }
    }

    private void InteractMinionsInRange(Mine detonatedMine, List<Minion> minionsInRange)
    {
        var playerByMine = detonatedMine.Player;
        foreach (var minion in minionsInRange)
        {
            minion.Health -= detonatedMine.Damage;
            if (minion.Health <= 0)
            {
                this.UpdatePlayerScore(playerByMine);
                this.minionByX[minion.XCoordinate].Remove(minion);
            }
        }
    }
}
