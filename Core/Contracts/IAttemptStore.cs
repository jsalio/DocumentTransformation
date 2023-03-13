using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Attempts;
using Core.Models.Exceptions;

namespace Core.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAttemptStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="StoreException"></exception>
        Task<int> AddAttempt(AddAttemptRequest request);
        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="StoreException"></exception>
        Task<int> UnlockCase(IEnumerable<UpdateCaseAttempt> request);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        /// <exception cref="StoreException"></exception>
        Task<int> DirectLockCase(AddAttemptRequest request, AttemptCaseStatus status);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="StoreException"></exception>
        Task<List<Attempt>> GetAllCases();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="caseId"></param>
        /// <returns></returns>
        /// <exception cref="StoreException"></exception>
        Task<List<AttemptDetail>> GetCaseDetails(long caseId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        /// <exception cref="StoreException"></exception>
        Task<bool> Exists(Expression<Func<Attempt, bool>> expression);
    }
}
