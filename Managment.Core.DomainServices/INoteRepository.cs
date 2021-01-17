using Managment.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Core.DomainServices
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAllNotes();
        Task<Note> GetById(int id);
        Task AddNote(Note newnote);
        IEnumerable<Note> GetByAnimalId(int id);
        Task DeleteNote(Note note);
    }
}
