using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interfaces;
using Repositories;
using Models;
using TodoItemException;
using GenericList;

namespace TodoRepositoryTests
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }
        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }
        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }
        [TestMethod]
        public void CreatingDatabaseWithExistingIGenericList()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            list.Add(todoItem);
            ITodoRepository repository = new TodoRepository(list);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        public void GettingElementWillGetElementFromDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            list.Add(todoItem);
            ITodoRepository repository = new TodoRepository(list);
            Assert.AreEqual(todoItem, repository.Get(todoItem.Id));
        }

        [TestMethod]
        public void GettingActiveElementWillGetActiveElementFromDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            list.Add(todoItem);
            ITodoRepository repository = new TodoRepository(list);
            Assert.AreEqual(1, repository.GetActive().Count);
        }

        [TestMethod]
        public void GettingAllElementsWillGetAllElementsFromDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Cars ");
            list.Add(todoItem);
            list.Add(todoItem2);
            ITodoRepository repository = new TodoRepository(list);
            Assert.AreEqual(2, repository.GetAll().Count);
        }

        [TestMethod]
        public void GettingCompletedWillGetAllCompletedElementsFromDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Cars ");
            list.Add(todoItem);
            list.Add(todoItem2);
            ITodoRepository repository = new TodoRepository(list);
            repository.MarkAsCompleted(todoItem.Id);
            Assert.AreEqual(1, repository.GetCompleted().Count);
        }

        [TestMethod]
        public void RemoveWillRemoveElementFromDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Cars ");
            list.Add(todoItem);
            list.Add(todoItem2);
            ITodoRepository repository = new TodoRepository(list);
            Assert.IsTrue(repository.Remove(todoItem2.Id));
            Assert.AreEqual(1, repository.GetAll().Count);
        }

        [TestMethod]
        public void RemoveNullWillNotRemoveElementFromDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            var todoItem2 = new TodoItem(" Cars ");
            list.Add(todoItem);
            list.Add(todoItem2);
            ITodoRepository repository = new TodoRepository(list);
            Assert.IsFalse(repository.Remove(Guid.NewGuid()));
            Assert.AreEqual(2, repository.GetAll().Count);
        }

        [TestMethod]
        public void UpdateExistingElementWillUpdateElementInDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            list.Add(todoItem);
            ITodoRepository repository = new TodoRepository(list);
            todoItem.Text = "Ananas";
            repository.Update(todoItem);
            Assert.AreEqual("Ananas", repository.Get(todoItem.Id).Text);
            Assert.AreNotEqual(" Groceries ", repository.Get(todoItem.Id).Text);
        }

        [TestMethod]
        public void UpdateNonExistingElementWillAddElementInDatabase()
        {
            IGenericList<TodoItem> list = new GenericList<TodoItem>(); ;
            var todoItem = new TodoItem(" Groceries ");
            ITodoRepository repository = new TodoRepository(list);
            todoItem.Text = "Ananas";
            Assert.AreEqual(0, repository.GetAll().Count);
            repository.Update(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.AreEqual("Ananas", repository.Get(todoItem.Id).Text);
            Assert.AreNotEqual(" Groceries ", repository.Get(todoItem.Id).Text);
        }
    }
}
