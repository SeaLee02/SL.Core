using SL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL.Utils.Extensions
{
	public static class SLCollectionExtensions
	{
		public static bool IsNull<T>(this ICollection<T> source)
		{
			return source == null || source.Count <= 0;
		}
		public static bool NotNull<T>(this ICollection<T> source)
		{
			return source != null || source.Count > 0;
		}


		public static bool AddIfNotContains<T>(this ICollection<T> source, T item)
		{
			Check.NotNull(source, nameof(source));

			if (source.Contains(item))
			{
				return false;
			}

			source.Add(item);
			return true;
		}

		public static IEnumerable<T> AddIfNotContains<T>(this ICollection<T> source, IEnumerable<T> items)
		{
			Check.NotNull(source, nameof(source));

			var addedItems = new List<T>();

			foreach (var item in items)
			{
				if (source.Contains(item))
				{
					continue;
				}

				source.Add(item);
				addedItems.Add(item);
			}

			return addedItems;
		}

		public static bool AddIfNotContains<T>(this ICollection<T> source, Func<T, bool> predicate, Func<T> itemFactory)
		{
			Check.NotNull(source, nameof(source));
			Check.NotNull(predicate, nameof(predicate));
			Check.NotNull(itemFactory, nameof(itemFactory));

			if (source.Any(predicate))
			{
				return false;
			}

			source.Add(itemFactory());
			return true;
		}

		public static IList<T> RemoveAll<T>(this ICollection<T> source, Func<T, bool> predicate)
		{
			var items = source.Where(predicate).ToList();

			foreach (var item in items)
			{
				source.Remove(item);
			}

			return items;
		}

		public static void RemoveAll<T>(this ICollection<T> source, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				source.Remove(item);
			}
		}
	}
}
