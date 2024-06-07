namespace KallyPoker;

public struct PlayerCollection(PlayerArray playerArray, int count)
{
    public PlayerCollection() : this(new PlayerArray(), PokerConstants.PlayerCount)
    {
    }
    
    public int Count { get; private set; } = count;

    public Player this[int index]
    {
        get => playerArray[index];
        set => playerArray[index] = value;
    }

    public void ResetCards()
    {
        foreach (ref var player in playerArray)
            player.Cards = CardCollection.Empty;
    }

    public Enumerator GetEnumerator() => new(this);

    public ref struct Enumerator(PlayerCollection playerCollection)
    {
        private readonly PlayerCollection _playerCollection = playerCollection;
        private int _index = -1;

        public readonly Player Current => _playerCollection[_index];

        public bool MoveNext()
        {
            var newIndex = _index + 1;
            if (newIndex >= _playerCollection.Count)
                return false;
            
            _index = newIndex;
            return true;

        }
    }
}