using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityAsyncAwaitUtil;

// We could just add a generic GetAwaiter to YieldInstruction and CustomYieldInstruction
// but instead we add specific methods to each derived class to allow for return values
// that make the most sense for the specific instruction type
public static class IEnumeratorAwaitExtensions
{
    public static SimpleCoroutineAwaiter GetAwaiter(this WaitForSeconds instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter GetAwaiter(this WaitForUpdate instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter GetAwaiter(this WaitForEndOfFrame instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter GetAwaiter(this WaitForFixedUpdate instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter GetAwaiter(this WaitForSecondsRealtime instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter GetAwaiter(this WaitUntil instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter GetAwaiter(this WaitWhile instruction)
    {
        return GetAwaiterReturnVoid(instruction);
    }

    public static SimpleCoroutineAwaiter<AsyncOperation> GetAwaiter(this AsyncOperation instruction)
    {
        return GetAwaiterReturnSelf(instruction);
    }

    public static SimpleCoroutineAwaiter<UnityEngine.Object> GetAwaiter(this ResourceRequest instruction)
    {
        var er = new SimpleCoroutineAwaiter<UnityEngine.Object>();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            InstructionWrappers.ResourceRequest(er, instruction)));
        return er;
    }

    // Return itself so you can do things like ( new WWW(url)).bytes
    public static SimpleCoroutineAwaiter<WWW> GetAwaiter(this WWW instruction)
    {
        return GetAwaiterReturnSelf(instruction);
    }

    public static SimpleCoroutineAwaiter<AssetBundle> GetAwaiter(this AssetBundleCreateRequest instruction)
    {
        var er = new SimpleCoroutineAwaiter<AssetBundle>();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            InstructionWrappers.AssetBundleCreateRequest(er, instruction)));
        return er;
    }

    public static SimpleCoroutineAwaiter<UnityEngine.Object> GetAwaiter(this AssetBundleRequest instruction)
    {
        var er = new SimpleCoroutineAwaiter<UnityEngine.Object>();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            InstructionWrappers.AssetBundleRequest(er, instruction)));
        return er;
    }

    public static SimpleCoroutineAwaiter<T> GetAwaiter<T>(this IEnumerator<T> coroutine)
    {
        var er = new SimpleCoroutineAwaiter<T>();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            new CoroutineWrapper<T>(coroutine, er).Run()));
        return er;
    }

    public static SimpleCoroutineAwaiter<object> GetAwaiter(this IEnumerator coroutine)
    {
        var er = new SimpleCoroutineAwaiter<object>();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            new CoroutineWrapper<object>(coroutine, er).Run()));
        return er;
    }

    static SimpleCoroutineAwaiter GetAwaiterReturnVoid(object instruction)
    {
        var er = new SimpleCoroutineAwaiter();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            InstructionWrappers.ReturnVoid(er, instruction)));
        return er;
    }

    static SimpleCoroutineAwaiter<T> GetAwaiterReturnSelf<T>(T instruction)
    {
        var er = new SimpleCoroutineAwaiter<T>();
        RunOnUnityScheduler(() => AsyncCoroutineRunner.Instance.StartCoroutine(
            InstructionWrappers.ReturnSelf(er, instruction)));
        return er;
    }

    static void RunOnUnityScheduler(Action action)
    {
        if (SynchronizationContext.Current == SyncContextUtil.UnitySynchronizationContext)
        {
            action();
        }
        else
        {
            SyncContextUtil.UnitySynchronizationContext.Post(_ => action(), null);
        }
    }

    static void Assert(bool condition)
    {
        if (!condition)
        {
            throw new Exception("Assert hit in UnityAsyncUtil package!");
        }
    }

    public class SimpleCoroutineAwaiter<T> : INotifyCompletion
    {
        bool _isDone;
        Exception _exception;
        Action _continuation;
        T _result;

        public bool IsCompleted
        {
            get { return _isDone; }
        }

        public T GetResult()
        {
            Assert(_isDone);

            if (_exception != null)
            {
                ExceptionDispatchInfo.Capture(_exception).Throw();
            }

            return _result;
        }

        public void Complete(T result, Exception e)
        {
            Assert(!_isDone);

            _isDone = true;
            _exception = e;
            _result = result;

            // Always trigger the continuation on the unity thread when ing on unity yield
            // instructions
            if (_continuation != null)
            {
                RunOnUnityScheduler(_continuation);
            }
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            Assert(_continuation == null);
            Assert(!_isDone);

            _continuation = continuation;
        }
    }

    public class SimpleCoroutineAwaiter : INotifyCompletion
    {
        bool _isDone;
        Exception _exception;
        Action _continuation;

        public bool IsCompleted
        {
            get { return _isDone; }
        }

        public void GetResult()
        {
            Assert(_isDone);

            if (_exception != null)
            {
                ExceptionDispatchInfo.Capture(_exception).Throw();
            }
        }

        public void Complete(Exception e)
        {
            Assert(!_isDone);

            _isDone = true;
            _exception = e;

            // Always trigger the continuation on the unity thread when ing on unity yield
            // instructions
            if (_continuation != null)
            {
                RunOnUnityScheduler(_continuation);
            }
        }

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            Assert(_continuation == null);
            Assert(!_isDone);

            _continuation = continuation;
        }
    }

    class CoroutineWrapper<T>
    {
        readonly SimpleCoroutineAwaiter<T> _er;
        readonly Stack<IEnumerator> _processStack;

        public CoroutineWrapper(
            IEnumerator coroutine, SimpleCoroutineAwaiter<T> er)
        {
            _processStack = new Stack<IEnumerator>();
            _processStack.Push(coroutine);
            _er = er;
        }

        public IEnumerator Run()
        {
            while (true)
            {
                var topWorker = _processStack.Peek();

                bool isDone;

                try
                {
                    isDone = !topWorker.MoveNext();
                }
                catch (Exception e)
                {
                    // The IEnumerators we have in the process stack do not tell us the
                    // actual names of the coroutine methods but it does tell us the objects
                    // that the IEnumerators are associated with, so we can at least try
                    // adding that to the exception output
                    var objectTrace = GenerateObjectTrace(_processStack);

                    if (objectTrace.Any())
                    {
                        _er.Complete(
                            default(T), new Exception(
                                GenerateObjectTraceMessage(objectTrace), e));
                    }
                    else
                    {
                        _er.Complete(default(T), e);
                    }

                    yield break;
                }

                if (isDone)
                {
                    _processStack.Pop();

                    if (_processStack.Count == 0)
                    {
                        _er.Complete((T)topWorker.Current, null);
                        yield break;
                    }
                }

                // We could just yield return nested IEnumerator's here but we choose to do
                // our own handling here so that we can catch exceptions in nested coroutines
                // instead of just top level coroutine
                if (topWorker.Current is IEnumerator)
                {
                    _processStack.Push((IEnumerator)topWorker.Current);
                }
                else
                {
                    // Return the current value to the unity engine so it can handle things like
                    // WaitForSeconds, WaitToEndOfFrame, etc.
                    yield return topWorker.Current;
                }
            }
        }

        string GenerateObjectTraceMessage(List<Type> objTrace)
        {
            var result = new StringBuilder();

            foreach (var objType in objTrace)
            {
                if (result.Length != 0)
                {
                    result.Append(" -> ");
                }

                result.Append(objType.ToString());
            }

            result.AppendLine();
            return "Unity Coroutine Object Trace: " + result.ToString();
        }

        static List<Type> GenerateObjectTrace(IEnumerable<IEnumerator> enumerators)
        {
            var objTrace = new List<Type>();

            foreach (var enumerator in enumerators)
            {
                // NOTE: This only works with scripting engine 4.6
                // And could easily stop working with unity updates
                var field = enumerator.GetType().GetField("$this", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

                if (field == null)
                {
                    continue;
                }

                var obj = field.GetValue(enumerator);

                if (obj == null)
                {
                    continue;
                }

                var objType = obj.GetType();

                if (!objTrace.Any() || objType != objTrace.Last())
                {
                    objTrace.Add(objType);
                }
            }

            objTrace.Reverse();
            return objTrace;
        }
    }

    static class InstructionWrappers
    {
        public static IEnumerator ReturnVoid(
            SimpleCoroutineAwaiter er, object instruction)
        {
            // For simple instructions we assume that they don't throw exceptions
            yield return instruction;
            er.Complete(null);
        }

        public static IEnumerator AssetBundleCreateRequest(
            SimpleCoroutineAwaiter<AssetBundle> er, AssetBundleCreateRequest instruction)
        {
            yield return instruction;
            er.Complete(instruction.assetBundle, null);
        }

        public static IEnumerator ReturnSelf<T>(
            SimpleCoroutineAwaiter<T> er, T instruction)
        {
            yield return instruction;
            er.Complete(instruction, null);
        }

        public static IEnumerator AssetBundleRequest(
            SimpleCoroutineAwaiter<UnityEngine.Object> er, AssetBundleRequest instruction)
        {
            yield return instruction;
            er.Complete(instruction.asset, null);
        }

        public static IEnumerator ResourceRequest(
            SimpleCoroutineAwaiter<UnityEngine.Object> er, ResourceRequest instruction)
        {
            yield return instruction;
            er.Complete(instruction.asset, null);
        }
    }
}
