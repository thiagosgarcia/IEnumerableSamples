using System.Text.RegularExpressions;

namespace IEnumerableSamples;

public class FileFinder
{
    //TODO DI - remove static refs
    /// <summary>
    /// LookForFiles method searches for files that match a specified input in the given path. </summary>
    /// <param name="path">The path to search for files.</param> <param name="input">The input to match the files against.</param>
    /// <returns>
    /// An IEnumerable collection of strings representing the matched files. </returns>
    /// /
    public static IEnumerable<string> LookForFiles(string path, string input)
    {
        IEnumerable<string> files = FilterFiles(path, input);
        foreach (var file in files)
        {
            Console.WriteLine(file);
            yield return file;
        }
    }

    /// <summary>
    /// Filters files in a given path and its subfolders based on the specified input pattern.
    /// </summary>
    /// <param name="path">The path to search for files.</param>
    /// <param name="input">The input pattern to filter files.</param>
    /// <returns>An enumerable collection of file paths that match the input pattern.</returns>
    static IEnumerable<string> FilterFiles(string path, string input)
    {
        var entries = Directory.EnumerateFileSystemEntries(path, "*", new EnumerationOptions()
        {
            IgnoreInaccessible = true
        });
        foreach (var entry in entries)
        {
            if (Regex.IsMatch(entry, $"{Path.PathSeparator}\\.{{1,2}}$"))
                continue;

            if (Directory.Exists(entry))
            {
                var innerFolderFiles = FilterFiles(entry, input);
                foreach (var innerFolderFile in innerFolderFiles)
                    yield return innerFolderFile;
            }

            if (!File.Exists(entry))
                continue;

            if (Regex.IsMatch(entry, $"{input}$"))
                yield return entry;
        }
    }

    /// <summary>
    /// Searches for files in the specified path that match the input string asynchronously.
    /// </summary>
    /// <param name="path">The directory path to search for files in.</param>
    /// <param name="input">The string pattern used to match file names.</param>
    /// <returns>An asynchronous enumerable of file paths that match the input string.</returns>
    public static async IAsyncEnumerable<string> LookForFilesAsync(string path, string input)
    {
        IAsyncEnumerable<string> files = FilterFilesAsync(path, input);
        await foreach (var file in files)
        {
            Console.WriteLine(file);
            yield return file;
        }
    }

    /// <summary>
    /// Filters files in a specified path and its subdirectories based on a given input pattern asynchronously.
    /// </summary>
    /// <param name="path">The root path to start filtering files.</param>
    /// <param name="input">The regular expression pattern to match files against.</param>
    /// <returns>An asynchronous enumerable of strings representing the filtered file paths.</returns>
    static async IAsyncEnumerable<string> FilterFilesAsync(string path, string input)
    {
        var entries = Directory.EnumerateFileSystemEntries(path, "*", new EnumerationOptions()
        {
            IgnoreInaccessible = true
        });
        foreach (var entry in entries)
        {
            if (Regex.IsMatch(entry, $"{Path.PathSeparator}\\.{{1,2}}$"))
                continue;

            if (Directory.Exists(entry))
            {
                var innerFolderFiles = FilterFilesAsync(entry, input);
                await foreach (var innerFolderFile in innerFolderFiles)
                    yield return innerFolderFile;
            }

            if (!File.Exists(entry))
                continue;

            if (Regex.IsMatch(entry, $"{input}$"))
            {
                await Task.Delay(10); //Do some work
                yield return entry;
            }
        }
    }
}