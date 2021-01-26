#region Using derectives

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace CyberMath.Helpers
{
	/// <summary>
	///     Just a class to make out life and programing faster and more productive
	/// </summary>
	public static class GenericTypesExtensions
	{
		/// <summary>
		///     Makes a deep copy of <paramref name="item" />.
		///     <c>Type of <paramref name="item" /> should be marked as [Serializable]; otherwise -> EXCEPTION</c>
		/// </summary>
		/// <param name="item">Object to copy</param>
		/// <typeparam name="T">[Serializable] and NOT null</typeparam>
		/// <returns>New copied object</returns>
		/// <exception cref="ArgumentNullException">When <paramref name="item" /> is null</exception>
		/// <exception cref="ArgumentException">When <paramref name="item" /> is not [Serializable]</exception>
		public static T SerializableDeepCopy<T>(this T item)
		{
			if (ReferenceEquals(item, null)) throw new ArgumentNullException(nameof(item));
			if (!typeof(T).IsSerializable)
				throw new ArgumentException("Inserted type should be with [Serializable] attribute");

			var formatter = new BinaryFormatter();
			using var stream = new MemoryStream();
			formatter.Serialize(stream, item);
			stream.Seek(0, SeekOrigin.Begin);
			return (T)formatter.Deserialize(stream);
		}
	}
}