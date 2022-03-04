﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Utils.Extensions
{
    public static class SLLockExtensions
    {
        public static void Locking(this object source, Action action)
        {
            lock (source)
            {
                action();
            }
        }

        public static void Locking<T>(this T source, Action<T> action) where T : class
        {
            lock (source)
            {
                action(source);
            }
        }

        public static TResult Locking<TResult>(this object source, Func<TResult> func)
        {
            lock (source)
            {
                return func();
            }
        }

        public static TResult Locking<T, TResult>(this T source, Func<T, TResult> func) where T : class
        {
            lock (source)
            {
                return func(source);
            }
        }
    }
}
