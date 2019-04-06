using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using LogixHealth.Eligibility.Models.Infrastructure.SharedKernel;
using LogixHealth.EnterpriseLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogixHealth.Eligibility.DataAccess.Infrastructure.Interfaces
{
    public interface IMdRequestValidationRepository<TEntity,K>:IRepository<TEntity,K> where TEntity: DomainEntity<int>
    {
       
    }
}
