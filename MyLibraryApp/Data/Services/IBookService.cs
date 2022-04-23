using MyLibraryApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibraryApp.Data.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Add(Book newBook);
        Book GetById(Guid id);
        void Remove(Guid id);
    }
}
