using System;
using System.Collections.Generic;
using System.Linq;

namespace LightStore.Application.Utils
{
    public static class TreeUtils
    {
        public static bool AnyParent<T>(
            T node,
            Func<T, T> parentGetter,
            Predicate<T> predicate)
        {
            while (true)
            {
                var parent = parentGetter(node);
                if (parent is null)    return false;
                if (predicate(parent)) return true;
            }
        }

        public static TDest Transform<TSrc, TDest>(
            TSrc src,
            Func<TSrc, IEnumerable<TSrc>> srcChildrenGetter,
            Func<TSrc, TDest> visitor,
            Action<TDest, List<TDest>> destChildrenSetter)
        {
            if (src is null)
                throw new ArgumentNullException(nameof(src));

            var dest = visitor(src);
            var srcChildren = srcChildrenGetter(src);

            if (srcChildren is null)
                return dest;

            var destChildren = new List<TDest>(srcChildren.Count());
            destChildrenSetter(dest, destChildren);

            foreach (var child in srcChildren)
            {
                destChildren.Add(
                    Transform(child, srcChildrenGetter, visitor, destChildrenSetter)
                );
            }

            return dest;
        }

        public static List<T> Enumerate<T>(T src, Func<T, IEnumerable<T>> srcChildrenGetter)
        {
            var dest = new List<T>();
            if (src is not null)
                EnumerateRecursive(src, srcChildrenGetter, ref dest);
            return dest;
        }

        public static List<TDest> Enumerate<TSrc, TDest>(
            TSrc src,
            Func<TSrc, IEnumerable<TSrc>> srcChildrenGetter,
            Func<TSrc, TDest> visitor)
        {
            var dest = new List<TDest>();
            if (src is not null)
                EnumerateRecursive(src, srcChildrenGetter, visitor, ref dest);
            return dest;
        }

        private static void EnumerateRecursive<T>(
            T src,
            Func<T, IEnumerable<T>> srcChildrenGetter,
            ref List<T> dest)
        {
            dest.Add(src);
            var srcChildren = srcChildrenGetter(src);

            if (srcChildren is null)
                return;

            foreach (var child in srcChildren)
                EnumerateRecursive(child, srcChildrenGetter, ref dest);
        }

        private static void EnumerateRecursive<TSrc, TDest>(
            TSrc src,
            Func<TSrc, IEnumerable<TSrc>> srcChildrenGetter,
            Func<TSrc, TDest> visitor,
            ref List<TDest> dest)
        {
            dest.Add(visitor(src));
            var srcChildren = srcChildrenGetter(src);

            if (srcChildren is null)
                return;

            foreach (var child in srcChildren)
                EnumerateRecursive(child, srcChildrenGetter, visitor, ref dest);
        }
    }
}
