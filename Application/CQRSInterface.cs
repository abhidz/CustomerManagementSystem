namespace CustomerManagementSystem.Application
{
    // These both are Marker interfaces for CQRS pattern. They don't have any members, but they are used to identify
    // the type of the class (Command or Query) and to enforce a certain structure in the application.
    public interface ICommand
    {

    }

    public interface ICommandHandler<T> //CommandHandler will handle a command
    {
        void Handle(T command); // Handle method will take a command as a parameter and return void
    }
    public interface IQuery<TResult> // Query class will return a result, so we use generic type TResult
    {

    }

    public interface IQueryHandler<Tinput, TResult> // QueryHandler will handle a query and return a result
    {
        List<TResult> Query<T>(Tinput input); // Handle method will take a query as a parameter and return a result
    }
}
