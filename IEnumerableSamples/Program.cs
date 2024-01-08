// See https://aka.ms/new-console-template for more information

using Dasync.Collections;
using IEnumerableSamples;

string? input, path;
ConsoleKeyInfo method;
do
{
    Console.WriteLine("[A]sync / [S]ync?");
    method = Console.ReadKey();
    if (method.Key == ConsoleKey.Enter)
        break;
    Console.WriteLine();

    Console.WriteLine("Choose Path");
    path = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(path))
        break;

    Console.WriteLine("Choose file type");
    input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        break;

    var asyncFiles = AsyncEnumerable.Empty<string>();
    var syncFiles = Enumerable.Empty<string>();

    if((method.KeyChar) is 'a' or 'A')
        asyncFiles = FileFinder.LookForFilesAsync(path, input);
    else if((method.KeyChar) is 's' or 'S')
        syncFiles = FileFinder.LookForFiles(path, input);

    await IteratorSamples.IEnumerableIterator(asyncFiles);
    await IteratorSamples.IEnumeratorIterator(asyncFiles.GetAsyncEnumerator());

    IteratorSamples.SyncIEnumerableIterator(syncFiles);
    IteratorSamples.SyncIEnumeratorIterator(syncFiles.GetEnumerator());

} while (true);


