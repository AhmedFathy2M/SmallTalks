﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IGenericRepository<T> where T: BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllElementsAsync();
		Task<int> AddElementAsync(T element);
	}
}
