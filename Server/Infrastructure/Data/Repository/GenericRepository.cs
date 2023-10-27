using Core.Entities;
using Core.Interfaces;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repository
{
	public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
	{
		private readonly AppIdentityDbContext _chatContext;
		public GenericRepository(AppIdentityDbContext chatContext)
		{
			_chatContext = chatContext;
		}

		public async Task<int> AddElementAsync(T element)
		{
			await _chatContext.Set<T>().AddAsync(element);
			return await _chatContext.SaveChangesAsync();
		}

		public async Task<IReadOnlyList<T>> GetAllElementsAsync()
		{
			return await _chatContext.Set<T>().ToListAsync();
		}
	}
}
