using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.VirtualFileExplorer.Blazor;

public class GetVirtualFileExplorerInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    public string Path { get; set; }

    public Func<string, Task> PathChanged { get; set; }
}
