using LogixHealth.Eligibility.DataAccess.Infrastructure.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using LogixHealth.Eligibility.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LogixHealth.Eligibility.DataAccess.Infrastructure.Implements
{
    public interface IAuditTrailRepository : IRepository<AuditTrails, int>
    {
    }

    public class AuditTrailRepository : IAuditTrailRepository
    {
        private IRepositoryFactory _repositoryFactory;
        private IRepositoryBase<AuditTrails> _registerRepository;

        public AuditTrailRepository(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _registerRepository = _repositoryFactory.Create<AuditTrails>();
        }

        public AuditTrails Add(AuditTrails entity)
        {
            var result = _registerRepository.Insert(entity);
            return result;
        }


        public IQueryable<AuditTrails> FindAll(params Expression<Func<AuditTrails, object>>[] includeProperties)
        {
           
            var result = _registerRepository.Query().Go();

            return result.AsQueryable();
        }

        public IQueryable<AuditTrails> FindAll(Expression<Func<AuditTrails, bool>> predicate, params Expression<Func<AuditTrails, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public AuditTrails FindById(int id, params Expression<Func<AuditTrails, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public AuditTrails FindSingle(Expression<Func<AuditTrails, bool>> predicate, params Expression<Func<AuditTrails, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public AuditTrails Remove(AuditTrails entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiple(IEnumerable<AuditTrails> entities)
        {
            var fieldId = entities.FirstOrDefault().Field;
            //_registerRepository.Query().GroupBy(x => x.Field).Go();
            var res = _registerRepository.Delete().Where(x=>x.Field == fieldId).Go();
        }

        public void Update(AuditTrails entity)
        {
            throw new NotImplementedException();
        }
    }
}