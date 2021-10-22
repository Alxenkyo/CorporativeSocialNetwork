using AutoMapper;
using CorporativeSN.Data;
using CorporativeSN.Logic.Interfaces;
using CorporativeSN.Logic.Models;
using CorporativeSN.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CorporativeSN.Logic.Managers
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly ICorpSNContext _corpSNContext;
        private readonly IMapper _mapper;

        public DepartmentManager(ICorpSNContext corpSNContext, IMapper mapper)
        {
            _corpSNContext = corpSNContext;
            _mapper = mapper;
        }

        public async Task<DepartmentDTO> CreateDepartmentAsync(DepartmentDTO dep, CancellationToken cancellationToken = default)
        {
            var add = _mapper.Map<Departments>(dep);
            _corpSNContext.Departments.Add(add);
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<DepartmentDTO>(add);
        }

        public async Task DeleteDepartmentAsync(int depId, CancellationToken cancellationToken = default)
        {
            var dep = await _corpSNContext.Departments.FirstOrDefaultAsync(x => x.Id == depId, cancellationToken);
            if (dep != null)
            {
                _corpSNContext.Departments.Remove(dep);
                await _corpSNContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<PagedResult<DepartmentDTO>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            var query = _corpSNContext.Departments.AsNoTracking();
            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    query = query.Where(x =>
            //        x.Name.ToLower().Contains(search.ToLower()));
            //}
            //if (fromIndex.HasValue)
            //{
            //    query = query.Skip(fromIndex.Value);
            //}
            //query = query.OrderBy(x => x.Name);
            var total = await query.CountAsync(cancellationToken);
            //if (fromIndex.HasValue && toIndex.HasValue)
            //{
            //    query = query.Skip(fromIndex.Value).Take(toIndex.Value - fromIndex.Value + 1);
            //}
            //var items = _mapper.ProjectTo<ChatDTO>(query).ToArrayAsync(cancellationToken);
            var items = _mapper.Map<IEnumerable<DepartmentDTO>>(query);
            return new PagedResult<DepartmentDTO> { Items = (IEnumerable<DepartmentDTO>)items, Total = total };
        }

        public async Task<DepartmentDTO> UpdateDepartmentAsync(DepartmentDTO dep, CancellationToken cancellationToken = default)
        {
            var update = await _corpSNContext.Departments.FirstOrDefaultAsync(x => x.Id == dep.Id, cancellationToken);
            if (update != null)
            {
                _mapper.Map(dep, update);
            }
            await _corpSNContext.SaveChangesAsync(cancellationToken);
            return dep;
        }
    }
}
