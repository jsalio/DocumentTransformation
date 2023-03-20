namespace Core.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRequest<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        T BuildRequest();
    }

}
