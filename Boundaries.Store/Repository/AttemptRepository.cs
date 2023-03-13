using Core.Contracts;
using Core.Models.Attempts;
using Core.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boundaries.Store.Repository
{
    public sealed class AttemptRepository : IAttemptStore
    {

        private readonly IApplicationDbContext _context;

        public AttemptRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        Task<int> IAttemptStore.AddAttempt(AddAttemptRequest request)
        {
            try
            {
                var exitsCase = _context.Attempt.Include(d => d.AttemptDetails)
                    .FirstOrDefault(x => x.DocumentHandler == request.DocumentHandler);
                var config = _context.Rules.FirstOrDefault(x => x.Name == "TryLimits");
                if (exitsCase != null)
                {
                    _context.AttemptDetail.Add(new AttemptDetail
                    {
                        ErrorDetails = request.Message,
                        RegistryDate = DateTimeOffset.Now,
                        AttemptId =  exitsCase.Id
                    });

                    if (exitsCase.AttemptDetails.Count() + 1 == int.Parse(config.Value))
                    {
                        exitsCase.CaseCaseStatus = AttemptCaseStatus.Lock;
                    }
                }
                else
                {
                    _context.AttemptDetail.Add(new AttemptDetail
                    {
                        ErrorDetails = request.Message,
                        RegistryDate = DateTimeOffset.Now,
                        Attempt = new Attempt
                        {
                            BatchId = request.BatchId,
                            DocumentHandler = request.DocumentHandler,
                            DocumentType = request.DocumentType,
                            RegistryDate = DateTimeOffset.Now,
                            LastUpDate = DateTimeOffset.Now,
                            CaseCaseStatus = AttemptCaseStatus.Open
                        }
                    });
                }

                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }

            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<int> IAttemptStore.DirectLockCase(AddAttemptRequest request, AttemptCaseStatus status)
        {
            try
            {
                _context.AttemptDetail.Add(new AttemptDetail
                {
                    ErrorDetails = request.Message,
                    RegistryDate = DateTimeOffset.Now,
                    Attempt = new Attempt
                    {
                        BatchId = request.BatchId,
                        DocumentHandler = request.DocumentHandler,
                        DocumentType = request.DocumentType,
                        RegistryDate = DateTimeOffset.Now,
                        LastUpDate = DateTimeOffset.Now,
                        CaseCaseStatus = AttemptCaseStatus.Lock
                    }
                });
                return _context.SaveChanges();
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<bool> IAttemptStore.Exists(Expression<Func<Attempt, bool>> expression)
            => _context.Attempt.AnyAsync(expression);

        Task<List<Attempt>> IAttemptStore.GetAllCases()
        {
            try
            {
                var query = _context.Attempt
                    .Where(x => x.CaseCaseStatus == AttemptCaseStatus.Lock)
                    .ToListAsync();
                return query;
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<List<AttemptDetail>> IAttemptStore.GetCaseDetails(long caseId)
        {
            try
            {
                var query = _context.AttemptDetail
                    .Where(x => x.AttemptId == caseId)
                    .ToListAsync();
                return query;
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }

        Task<int> IAttemptStore.UnlockCase(IEnumerable<UpdateCaseAttempt> request)
        {
            try
            {
                var cases = request.Select(x => x.CaseId);
                var query = _context.Attempt.Where(x => cases.Contains(x.Id));
                foreach (Attempt attempt in query)
                {
                    attempt.CaseCaseStatus = AttemptCaseStatus.Open;
                }

                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new StoreException($"Error occurs when update concurrent {e.Message}");
            }
            catch (DbUpdateException e)
            {
                throw new StoreException($"Error occurs when update {e.Message}");
            }
            catch (TimeoutException e)
            {
                throw new StoreException($"Execution time is over {e.Message}");
            }
            catch (ArgumentException e)
            {
                throw new StoreException($"Invalid {e.Message}");
            }
            catch (Exception e)
            {
                throw new StoreException($"UnManage error occurs {e.Message}");
            }
        }
    }
}
