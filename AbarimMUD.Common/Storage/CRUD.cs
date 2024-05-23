﻿using AbarimMUD.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbarimMUD.Storage
{
	public abstract class CRUD<T> where T: Entity
	{
		private readonly DataContextSettings _context;
		internal readonly Dictionary<string, T> _cache = new Dictionary<string, T>();

		internal string BaseFolder => _context.Folder;

		public int Count => _cache.Count;

		public T[] All => _cache.Values.ToArray();

		internal CRUD(DataContextSettings context)
		{
			_context = context;
		}

		internal void Log(string message) => _context.Log(message);

		internal void AddToCache(T entity)
		{
			_cache[entity.Id] = entity;
		}

		public T GetById(string id)
		{
			T result;
			if (!_cache.TryGetValue(id, out result))
			{
				return null;
			}

			return result;
		}

		public T EnsureById(string id)
		{
			var result = GetById(id);
			if (result == null)
			{
				throw new Exception($"Could not find item with id {id}");
			}

			return result;
		}


		public void Update(T entity)
		{
			// Save the data
			Save(entity);

			// Add to the cache
			AddToCache(entity);
		}

		internal abstract void Save(T entity);

		internal virtual void SetReferences(DataContext db)
		{
		}
	}
}
