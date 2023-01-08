namespace Core.Contracts
{
    public interface IRequest<T>
    {
        T BuildRequest();
    }

}
