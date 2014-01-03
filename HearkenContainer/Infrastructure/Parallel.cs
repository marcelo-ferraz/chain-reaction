using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace HearkenContainer.Infrastructure
{
    public static class Parallel
    {
        private class Handler : IComparable
        {
            public ManualResetEvent Event { get; set; }
            public bool Available { get; set; }
            public Guid Id { get; set; }
            public int CompareTo(object obj)
            {
                return obj is Handler ?
                    this.Available.CompareTo(((Handler)obj).Available) :
                    Available.CompareTo((bool)obj);
            }
        }

        public class Pair<T>
        {
            public Action<T> Act { get; set; }
            public T Param { get; set; }
        }

        private static Handler[] _handlers = new Handler[64];

        static Parallel()
        {
            for (int i = 0; i < 64; i++)
            {
                _handlers[i] = new Handler
                {
                    Event = new ManualResetEvent(false),
                    Available = true
                };
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        private static Handler GetResetEvent(Guid id)
        {
            int index = -1;
            try
            {
                if (Monitor.TryEnter(_handlers, 500))
                {
                    Array.Sort(_handlers);

                    index =
                        Array.BinarySearch(_handlers, true);
                }
            }
            finally { Monitor.Exit(_handlers); }

            if (index < 0)
            {
                Thread.Sleep(5);
                return GetResetEvent(id);
            }

            try
            {
                if (Monitor.TryEnter(_handlers, 500))
                {
                    _handlers[index].Available = false;
                    _handlers[index].Id = id;
                }
            }
            finally { Monitor.Exit(_handlers); }

            return _handlers[index];
        }

        private static IEnumerable<ManualResetEvent> GetEvents(Guid id)
        {
            for (int i = 0; i < 64; i++)
            {
                if (object.Equals(_handlers[i].Id, id))
                {
                    yield return _handlers[i].Event;
                }
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static void Foreach<T>(IList<T> list, Action<int, T> needsToBeDone)
        {
            var id = Guid.NewGuid();
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];

                var ev = GetResetEvent(id);

                ThreadPool.QueueUserWorkItem(
                    p =>
                    {
                        var @params = (object[])p;
                        needsToBeDone(i, (T)@params[0]);

                        var handler = (Handler)@params[1];

                        try
                        {
                            if (Monitor.TryEnter(handler, 500))
                            {
                                handler.Event.Set();
                                handler.Available = true;
                            }
                        }
                        finally { Monitor.Exit(handler); }


                    }, new object[] { list[i], ev });
            }
            
            var events = GetEvents(id).ToArray();

            if (events == null || events.Length < 1) { return; }

            WaitHandle.WaitAll(events);
        }

        /// <summary>
        /// Executa em chunks de no maximo 64 acoes em "paralelo", por vez. 
        /// <remarks>Um mesmo parametro, passado para varias acoes</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter">Um mesmo parametro para todas as acoes</param>
        /// <param name="actions">acoes a serem executadas</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static void Run<T>(T parameter, params Action<T>[] actions)
        {
            Parallel.Foreach(actions,
                (i, act) => act(parameter));
        }

        /// <summary>
        /// Executa em chunks de no maximo 64 acoes em "paralelo", por vez. 
        /// <remarks>Uma mesma acao, passando varias parametros</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">Uma acao</param>
        /// <param name="parameters">parametros que serao passados para a acao, em paralelo</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static void Run<T>(Action<T> action, params T[] parameters)
        {
            Parallel.Foreach(parameters,
                (i, p) => action(p));
        }

        /// <summary>
        /// Executa em chunks de no maximo 64 acoes em "paralelo", por vez. 
        /// <remarks>Varias acoes a serem executadas em paralelo</remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pairs">Par com a acao e seu parametro</param>
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static void Run<T>(params Pair<T>[] pairs)
        {
            Parallel.Foreach(pairs,
                (i, item) => item.Act(item.Param));
        }
    }
}