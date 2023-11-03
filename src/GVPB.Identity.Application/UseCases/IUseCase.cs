namespace GVPB.Identity.Application;

public interface IUseCase<T>
{
    void Execute(T request);
}
