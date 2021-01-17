using Managment.Core.Domain;
using Managment.Core.DomainServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment.Infrastructure
{
    public class NoteRepository : INoteRepository
    {

        private readonly AnimalDbContext _context;

        public NoteRepository(AnimalDbContext context)
        {
            _context = context;
        }

        public async Task AddNote(Note newnote)
        {
            await _context.Notes.AddAsync(newnote);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return _context.Notes;
        }

        public async Task<Note> GetById(int id)
        {
            return await _context.Notes.SingleOrDefaultAsync(Note => Note.Id == id);
        }

        public IEnumerable<Note> GetByAnimalId(int id)
        {
            return _context.Notes?.Where(Note => Note.AnimalId == id);
        }

        public async Task DeleteNote(Note note)
        {
            if (note == null) throw new ArgumentNullException(nameof(note));

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }
    }
}
