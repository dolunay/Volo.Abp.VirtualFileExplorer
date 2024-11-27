using AutoMapper.Internal.Mappers;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.VirtualFileExplorer.Blazor.Models;

namespace Volo.Abp.VirtualFileExplorer.Blazor.Services;

public interface IVirtualFileExplorerAppService :
    IReadOnlyService<FileInfoViewModel, FileInfoViewModel, string, GetVirtualFileExplorerInput>
{

}
public interface IReadOnlyService<TGetOutputDto, TGetListOutputDto, in TKey, in TGetListInput>
{
    Task<TGetOutputDto> GetAsync(TKey id);

    Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input);
}