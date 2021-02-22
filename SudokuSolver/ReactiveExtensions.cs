using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Reactive.Disposables;

namespace SudokuSolver
{
    public static class ReactiveExtensions
    {
        public static IObservable<T> Pace<T>(this IObservable<T> source, TimeSpan interval)
        {
            return source.Select(i => Observable.Empty<T>()
                                                .Delay(interval)
                                                .StartWith(i)).Concat();

        }

        public static IObservable<T> ToCompleteableEnumerable<T>(this IEnumerable<T> enumerable)
        {
            return Observable.Create<T>(o =>
            {
                foreach (var item in enumerable)
                    o.OnNext(item);
                o.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
