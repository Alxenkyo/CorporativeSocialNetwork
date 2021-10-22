using CorporativeSN.Data;
using CorporativeSN.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Interfaces
{
    public interface IDepartmentManager
    {
        Task<PagedResult<DepartmentDTO>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
        Task<DepartmentDTO> CreateDepartmentAsync(DepartmentDTO dep, CancellationToken cancellationToken = default);
        Task<DepartmentDTO> UpdateDepartmentAsync(DepartmentDTO dep, CancellationToken cancellationToken = default);
        Task DeleteDepartmentAsync(int depId, CancellationToken cancellationToken = default);
    }
}
