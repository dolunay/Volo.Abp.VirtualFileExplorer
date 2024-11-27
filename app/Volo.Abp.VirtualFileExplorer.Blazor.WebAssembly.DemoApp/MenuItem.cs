namespace Volo.Abp.VirtualFileExplorer.Blazor.WebAssembly.DemoApp;

public class MenuItem
{
    public string Text { get; set; }

    public string Url { get; set; }

    public string Icon { get; set; }

    public bool Expanded { get; set; }

    public int Level { get; set; }

    public IEnumerable<MenuItem> Items { get; set; }
}
