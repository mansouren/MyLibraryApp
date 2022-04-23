using Microsoft.AspNetCore.Mvc;
using MyLibraryApp.Controllers;
using MyLibraryApp.Data.Models;
using MyLibraryApp.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyLibraryApp.Test
{
    public class BooksControllerTest
    {
        IBookService service;
        BooksController controller;
        public BooksControllerTest()
        {
            service = new BookService();
            controller = new BooksController(service);
        }

        [Fact]
        public void GetAllTest()
        {
            //arrange
            //act
            var result = controller.Get();
            //assert
            Assert.IsType<OkObjectResult>(result.Result);

            var list = result.Result as OkObjectResult;
            Assert.IsType<List<Book>>(list.Value);

            var listBooks = list.Value as List<Book>;
            Assert.Equal(5, listBooks.Count);
        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void GetByIdTest(string guid1,string guid2)
        {
            //arrange
            var validGuid =new Guid(guid1);
           
            //act
            var okObjectResult = controller.Get(validGuid);

            //assert
            Assert.IsType<OkObjectResult>(okObjectResult.Result);
            var item = okObjectResult.Result as OkObjectResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(validGuid,bookItem.Id);
            Assert.Equal("Managing Oneself", bookItem.Title);

            //arrange
            var invalidGuid = new Guid(guid2);
            //act
            var notFoundResult = controller.Get(invalidGuid);
            //assert
            Assert.IsType<NotFoundResult>(notFoundResult.Result);
           
        }

        [Fact]
        public void AddBookTest()
        {
            //arrange
            var compeletedBook = new Book
            {
                Title = "Title",
                Author = "Author",
                Description = "Description"
            };
            //act
            var createdActionResult = controller.Post(compeletedBook);
            //assert
            Assert.IsType<CreatedAtActionResult>(createdActionResult);
            var item = createdActionResult as CreatedAtActionResult;
            Assert.IsType<Book>(item.Value);

            var bookItem = item.Value as Book;
            Assert.Equal(compeletedBook.Author,bookItem.Author);
            Assert.Equal(compeletedBook.Title, bookItem.Title);
            Assert.Equal(compeletedBook.Description, bookItem.Description);


            //arrange
            var notCompeletedBook = new Book
            {
                Title = "Title"
            };
            //act
            controller.ModelState.AddModelError("Author", "Author Is Required!");
            var badRequestResult = controller.Post(notCompeletedBook);
            //assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);

        }

        [Theory]
        [InlineData("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200", "ab2bd817-98cd-4cf3-a80a-53ea0cd9c111")]
        public void RemoveBookByIdTest(string guid1, string guid2)
        {
            //arrange
            var validGuid = new Guid(guid1);
            var invalidGuid = new Guid(guid2);

            //act
            var notFoundResult = controller.Remove(invalidGuid);
            //assert
            Assert.IsType<NotFoundResult>(notFoundResult);
            //Assert.False(notFoundResult is NotFoundResult);
            Assert.Equal(5, service.GetAll().Count());

            //act
            var okResult = controller.Remove(validGuid);
            //assert
            Assert.IsType<OkResult>(okResult);
            Assert.Equal(4, service.GetAll().Count());
            
        }
    }
}
