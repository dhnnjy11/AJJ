
using Ajj.Core.Interface;
using Ajj.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ajj.Infrastructure.Repository
{
    public class UnityOfWork : IUnityOfWork
    {
        protected readonly ApplicationDbContext _context;
        private IPostalCodeRepository _postalcodeRepository;
        private IClientRepository _clientRepository;
        private IBusinessStreamRepository _businessStreamRepository;
        public UnityOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IClientRepository ClientRepository
        {
            get
            {
                return _clientRepository = _clientRepository ?? new ClientRepository(_context);
            }
        }

        public IPostalCodeRepository PostalcodeRepository
        {
            get
            {
                return _postalcodeRepository = _postalcodeRepository ?? new PostalCodeRepository(_context);
            }
        }

       
        public IBusinessStreamRepository BusinessStreamRepository
        {
            get
            {
                return _businessStreamRepository = _businessStreamRepository ?? new BusinessStreamRepository(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
