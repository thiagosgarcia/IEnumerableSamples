namespace IEnumerableSamples;

public class IteratorSamples
{
    public static async Task IEnumerableIterator(IAsyncEnumerable<object> enumerable)
    {
        await foreach (var item in enumerable)
        {
            //Iterating over the Enumerable, I don't have control over the cursor, so I get to iterate over the `Current` object
            Console.WriteLine(item.ToString());
        }
    }
    public static async Task IEnumeratorIterator(IAsyncEnumerator<object> enumerator)
    {
        while (await enumerator.MoveNextAsync())
        {
            //Iterating over the Enumerator, I have to call `Current` for the object that the cursor is pointing at
            Console.WriteLine(enumerator.Current.ToString());
        }
    }

    public static void SyncIEnumerableIterator(IEnumerable<object> enumerable)
    {
        foreach (var item in enumerable)
        {
            Console.WriteLine(item.ToString());
        }
    }
    public static void SyncIEnumeratorIterator(IEnumerator<object> enumerator)
    {
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current.ToString());
        }
        //For this one, I can even reset to the initial position if needed.
        enumerator.Reset();
    }
}