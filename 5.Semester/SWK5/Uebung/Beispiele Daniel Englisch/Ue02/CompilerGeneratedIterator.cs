[CompilerGenerated]
private sealed class CompilerGeneratedEnumerator : IEnumerator<KeyValuePair<K, V>>, IDisposable, IEnumerator
{
    private int state;

    private KeyValuePair<K, V> current;

    public HashDictionary<K, V> _this;

    private int i;

    private HashDictionary<K, V>.Node n;

    KeyValuePair<K, V> IEnumerator<KeyValuePair<K, V>>.Current
    {
        [DebuggerHidden]
        get
        {
            return this.current;
        }
    }

    object IEnumerator.Current
    {
        [DebuggerHidden]
        get
        {
            return this.current;
        }
    }

    [DebuggerHidden]
    public CompilerGeneratedEnumerator(int state)
    {
        this.state = state;
    }

    [DebuggerHidden]
    void IDisposable.Dispose()
    {
    }

    bool IEnumerator.MoveNext()
    {
        int num = this.state;
        if (num == 0)
        {
            this.state = -1;
            this.i = 0;
            goto IL_AC;
        }
        if (num != 1)
        {
            return false;
        }
        this.state = -1;
        this.n = this.n.Next;
        IL_88:
        if (this.n != null)
        {
            this.current = new KeyValuePair<K, V>(this.n.Key, this.n.Value);
            this.state = 1;
            return true;
        }
        this.n = null;
        this.i++;
        IL_AC:
        if (this.i >= this._this.hashTable.Length)
        {
            return false;
        }
        this.n = this._this.hashTable[this.i];
        goto IL_88;
    }

    [DebuggerHidden]
    void IEnumerator.Reset()
    {
        throw new NotSupportedException();
    }
}