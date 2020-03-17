using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class IAsyncOperationExtensions
{
    public static TaskAwaiter<T> GetAwaiter<T>(this AsyncOperationHandle<T> ap)
    {
        var tcs = new TaskCompletionSource<T>();
        ap.Completed += operation => tcs.TrySetResult(operation.Result);
        return tcs.Task.GetAwaiter();
    }
}