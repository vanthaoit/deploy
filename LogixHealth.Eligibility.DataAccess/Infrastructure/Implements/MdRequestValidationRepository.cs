using LogixHealth.Eligibility.DataAccess.Infrastructure.Interfaces;
using LogixHealth.Eligibility.DataAccess.TORM.CoreRepositories.Interfaces;
using LogixHealth.Eligibility.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LogixHealth.Eligibility.DataAccess.Infrastructure.Implements
{
    public interface IMdRequestValidationRepository : IRepository<MdRequestValidation, int>
    {
    }

    public class MdRequestValidationRepository : IMdRequestValidationRepository
    {
        private IRepositoryFactory _repositoryFactory;
        private IRepositoryBase<MdRequestValidation> _registerRepository;

        public MdRequestValidationRepository(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            _registerRepository = _repositoryFactory.Create<MdRequestValidation>();
        }

        public MdRequestValidation Add(MdRequestValidation entity)
        {
            var result = _registerRepository.Insert(entity);
            return result;
        }


        public IQueryable<MdRequestValidation> FindAll(params Expression<Func<MdRequestValidation, object>>[] includeProperties)
        {
            //IRepositoryBase<MdRequestValidation> registerRepository = _repositoryFactory.Create<MdRequestValidation>();

            var validationResult = _registerRepository.Query().Go();

            return validationResult.AsQueryable();
        }

        public IQueryable<MdRequestValidation> FindAll(Expression<Func<MdRequestValidation, bool>> predicate, params Expression<Func<MdRequestValidation, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public MdRequestValidation FindById(int id, params Expression<Func<MdRequestValidation, object>>[] includeProperties)
        {

            var validationResult = _registerRepository.Query().Where(x => x.Id == id).Go();

            return validationResult.FirstOrDefault();
        }

        public MdRequestValidation FindSingle(Expression<Func<MdRequestValidation, bool>> predicate, params Expression<Func<MdRequestValidation, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public MdRequestValidation Remove(MdRequestValidation entity)
        {
            var res = _registerRepository.Delete(entity);
            if (res == 1) return entity;
            return null ;
        }

        public void Remove(int id)
        {
            var entity = FindById(id);
            if(entity != null)
            _registerRepository.Delete(entity);
        }

        public void RemoveMultiple(IEnumerable<MdRequestValidation> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(MdRequestValidation entity)
        {
            
            var statusResult = _registerRepository.Update(entity);
        }
    }
}
