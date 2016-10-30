using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using GenericList;
using TodoItemException;

namespace Repositories
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly IGenericList<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(IGenericList<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new GenericList<TodoItem>();
            }
        }


        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            if (!_inMemoryTodoDatabase.Contains(todoItem))
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                throw new DuplicateTodoItemException("Duplicate id: " + todoItem.Id);
            }
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
        }

        public List<TodoItem> GetActive()
        {
            return _inMemoryTodoDatabase.Where(i => !i.IsCompleted).ToList();
        }

        public List<TodoItem> GetAll()
        {
            return _inMemoryTodoDatabase.OrderByDescending(i => i.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return _inMemoryTodoDatabase.Where(i => i.IsCompleted).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return _inMemoryTodoDatabase.Where(filterFunction).ToList();
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId && !i.IsCompleted).FirstOrDefault();
            if (item != null)
            {
                item.MarkAsCompleted();
                return true;
            }
            return false;
        }

        public bool Remove(Guid todoId)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoId).FirstOrDefault();
            return item == null ? false : _inMemoryTodoDatabase.Remove(item);
        }

        public void Update(TodoItem todoItem)
        {
            TodoItem item = _inMemoryTodoDatabase.Where(i => i.Id == todoItem.Id).FirstOrDefault();
            if (item != null)
            {
                _inMemoryTodoDatabase.Remove(item);
            }
            _inMemoryTodoDatabase.Add(todoItem);
        }
    }
}
