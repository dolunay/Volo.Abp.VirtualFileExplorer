using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Models;

public class FileInfoViewModel: ExtensibleEntityDto<string>
{
    public string FilePath { get; set; }

    public string Icon { get; set; }

    public string FileType { get; set; }

    public string Length { get; set; }

    public string FileName { get; set; }

    public DateTime LastUpdateTime { get; set; }

    public bool IsDirectory { get; set; }

    public Func<string, Task> PathChanged { get; set; }
}
